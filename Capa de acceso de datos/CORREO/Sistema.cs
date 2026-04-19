using System.Configuration;
namespace Capa_de_acceso_de_datos.CORREO
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.CORREO.ClsEmailService" />
    public class Sistema : ClsEmailService
    {
        /// <summary>
        /// The codigos verificacion
        /// </summary>
        public static Dictionary<string, string> CodigosVerificacion = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Sistema"/> class.
        /// </summary>
        /// <param name="correoRemitente"></param>
        /// <param name="contrasenaRemitente"></param>
        /// <param name="v"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public Sistema(string correoRemitente, string contrasenaRemitente, string v, int v1, bool v2)
            : base(correoRemitente, contrasenaRemitente, "smtp.gmail.com", 587, true)
        {
        }

        /// <summary>
        /// Enviars the codigo verificacion.
        /// </summary>
        /// <param name="correoDestino">The correo destino.</param>
        /// <param name="codigo">The codigo.</param>
        public void EnviarCodigoVerificacion(string correoDestino, string codigo)
        {
            string asunto = "Solicitud de Restablecimiento de Contraseña - Sistema Parroquia";

            string cuerpo = $@"
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #ddd; border-radius: 8px; padding: 20px;'>
                <h2 style='color: #2c3e50; text-align: center;'>Restablecimiento de Contraseña</h2>
                <p style='color: #555; font-size: 16px;'>Hola,</p>
                <p style='color: #555; font-size: 16px;'>Hemos recibido una solicitud para acceder a su cuenta. Utilice el siguiente código de seguridad para continuar con el proceso:</p>
        
                <div style='background-color: #f8f9fa; border: 1px dashed #2c3e50; padding: 20px; text-align: center; margin: 20px 0;'>
                    <span style='font-size: 32px; font-weight: bold; letter-spacing: 5px; color: #0046ad;'>{codigo}</span>
                </div>

                <div style='background-color: #fff3f3; border-left: 4px solid #d9534f; padding: 10px; margin-bottom: 20px;'>
                    <p style='color: #a94442; font-size: 14px; margin: 0;'>
                        <strong>⚠️ Información importante:</strong>
                    </p>
                    <ul style='color: #a94442; font-size: 13px; margin: 5px 0;'>
                        <li>Este código tiene una validez de <strong>15 minutos</strong>.</li>
                        <li>Dispone de un máximo de <strong>3 intentos</strong> para ingresar el código.</li>
                        <li>Si supera los intentos fallidos, su cuenta será <strong>inhabilitada</strong> por seguridad.</li>
                    </ul>
                </div>

                <p style='color: #888; font-size: 13px;'>Este código es confidencial. Si usted no realizó esta solicitud, puede ignorar este mensaje de forma segura.</p>
                <hr style='border: 0; border-top: 1px solid #eee; margin-top: 30px;'>
                <p style='text-align: center; color: #aaa; font-size: 12px;'>Este es un mensaje automático, por favor no responda a este correo.</p>
            </div>";

            List<string> destinatarios = new List<string> { correoDestino };
            sendMail(asunto, cuerpo, destinatarios);

            // Lógica del Diccionario (Nota: Tu lógica de base de datos ahora maneja esto mejor que el diccionario)
            if (CodigosVerificacion.ContainsKey(correoDestino))
                CodigosVerificacion[correoDestino] = codigo;
            else
                CodigosVerificacion.Add(correoDestino, codigo);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sistema"/> class.
        /// </summary>
        public Sistema()
        : base(
            ConfigurationManager.AppSettings["Correo.Remitente"],
            ConfigurationManager.AppSettings["Correo.Contrasena"],
            ConfigurationManager.AppSettings["Correo.Smtp"],
            int.Parse(ConfigurationManager.AppSettings["Correo.Puerto"]),
            true
        )
        {
        }

        /// <summary>
        /// Validars the codigo.
        /// </summary>
        /// <param name="correo">The correo.</param>
        /// <param name="codigoIngresado">The codigo ingresado.</param>
        /// <returns></returns>
        public bool ValidarCodigo(string correo, string codigoIngresado)
        {
            return CodigosVerificacion.ContainsKey(correo) && CodigosVerificacion[correo] == codigoIngresado;
        }
    }
}


