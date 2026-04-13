using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Clase base para gestionar conexiones a SQL Server y ejecutar procedimientos almacenados.
    /// Proporciona métodos para ejecutar comandos con sincronización remota automática.
    /// La sincronización notifica al servidor remoto sobre cambios realizados en la BD local.
    /// </summary>
    public class Clsconexion
    {
        static string conexion = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
        public SqlConnection sc = new SqlConnection(conexion);
        public Clsconexion() => sc.ConnectionString = conexion;

        /// <summary>
        /// Evento que se dispara cuando un procedimiento almacenado se ejecuta con sincronización.
        /// Parámetros: nombreSp (nombre del procedimiento), json (datos serializados incluyendo parámetros y parroquia).
        /// La capa de presentación se suscribe a este evento para notificar al servidor remoto.
        /// Se ejecuta de forma asincrónica sin bloquear la operación actual.
        /// </summary>
        public static event Action<string, string>? OnSpEjecutado;

        /// <summary>
        /// Ejecuta comando SQL sin retorno de datos (INSERT, UPDATE, DELETE).
        /// Parámetro cmd: comando con procedimiento y parámetros ya configurados.
        /// Parámetro sincronizar: true dispara NotificarSP para replicar cambio en servidor remoto.
        /// Abre conexión antes de ejecutar, cierra después en bloque finally.
        /// </summary>
        public void EjecutarYEnviar(SqlCommand cmd, bool sincronizar = false)
        {
            try
            {
                Abrir();
                cmd.Connection = sc;
                cmd.ExecuteNonQuery();
                if (sincronizar) NotificarSP(cmd);
            }
            finally { Cerrar(); }
        }

        /// <summary>
        /// Ejecuta comando SQL que retorna conjunto de datos (SELECT en procedimiento).
        /// Parámetro cmd: comando con procedimiento y parámetros ya configurados.
        /// Parámetro sincronizar: true dispara NotificarSP para sincronizar con servidor remoto.
        /// Retorna SqlDataReader con CommandBehavior.CloseConnection para cerrar conexión automáticamente.
        /// No cierra conexión explícitamente: SqlDataReader la cierra al cerrarse el reader.
        /// </summary>
        public SqlDataReader EjecutarReaderYEnviar(SqlCommand cmd, bool sincronizar = false)
        {
            Abrir();
            cmd.Connection = sc;
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (sincronizar) NotificarSP(cmd);
            return reader;
        }

        /// <summary>
        /// Ejecuta comando SQL que retorna un valor escalar (ID generado, count, suma, etc).
        /// Parámetro cmd: comando con procedimiento y parámetros ya configurados.
        /// Parámetro sincronizar: true dispara NotificarSP para replicar cambio en servidor remoto.
        /// Retorna valor int: convierte resultado a entero, o 0 si resultado es NULL o DBNull.
        /// Se usa para obtener IDs autogenerados de inserciones.
        /// Abre conexión antes de ejecutar, cierra después en bloque finally.
        /// </summary>
        public int EjecutarScalarYEnviar(SqlCommand cmd, bool sincronizar = false)
        {
            try
            {
                Abrir();
                cmd.Connection = sc;
                object res = cmd.ExecuteScalar();
                if (sincronizar) NotificarSP(cmd);
                return (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
            }
            finally { Cerrar(); }
        }

        /// <summary>
        /// Ejecuta comando SQL usando SqlDataAdapter para llenar DataTable.
        /// Parámetro cmd: comando con procedimiento y parámetros ya configurados.
        /// Parámetro sincronizar: true dispara NotificarSP para sincronizar con servidor remoto.
        /// Retorna DataTable poblado con todos los registros del resultado.
        /// Se usa cuando procedimiento retorna múltiples filas (listados, reportes).
        /// Abre conexión antes de ejecutar, cierra después en bloque finally.
        /// </summary>
        public DataTable EjecutarAdapterYEnviar(SqlCommand cmd, bool sincronizar = false)
        {
            try
            {
                Abrir();
                cmd.Connection = sc;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (sincronizar) NotificarSP(cmd);
                return dt;
            }
            finally { Cerrar(); }
        }

        /// <summary>
        /// Método privado que construye evento de sincronización con datos del procedimiento.
        /// Extrae nombre del SP y parámetros del comando, serializa a JSON.
        /// Incluye nombre del SP, diccionario de parámetros e ID de parroquia del usuario actual.
        /// Dispara evento OnSpEjecutado de forma asincrónica (sin esperar respuesta).
        /// La capa de presentación suscrita captura evento y envía datos a servidor remoto vía AccesoRemoto.
        /// </summary>
        private void NotificarSP(SqlCommand cmd)
        {
            if (OnSpEjecutado == null) return;
            string nombreSp = cmd.CommandText;

            var parametros = new Dictionary<string, object>();
            foreach (SqlParameter p in cmd.Parameters)
            {
                string key = p.ParameterName.Replace("@", "");
                object valor = (p.Value == null || p.Value == DBNull.Value) ? null : p.Value;
                parametros[key] = valor;
            }

            var root = new System.Text.Json.Nodes.JsonObject
            {
                ["SpName"] = nombreSp,
                ["Parametros"] = System.Text.Json.JsonSerializer.SerializeToNode(parametros),
                ["_ParroquiaId"] = Sesion1.id_parroquia
            };

            string json = root.ToJsonString();
            _ = Task.Run(() => OnSpEjecutado?.Invoke(nombreSp, json));
        }

        /// <summary>
        /// Abre conexión a SQL Server si está cerrada.
        /// Valida estado actual: solo abre si ConnectionState es Closed.
        /// Evita intentar abrir una conexión ya abierta.
        /// </summary>
        public void Abrir() { if (sc.State == ConnectionState.Closed) sc.Open(); }

        /// <summary>
        /// Cierra conexión a SQL Server si está abierta.
        /// Valida estado actual: solo cierra si ConnectionState es Open.
        /// Libera recursos de conexión a la BD.
        /// </summary>
        public void Cerrar() { if (sc.State == ConnectionState.Open) sc.Close(); }
    }
}