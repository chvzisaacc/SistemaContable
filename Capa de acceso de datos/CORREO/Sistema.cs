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
            string asunto = "Código de verificación - Recuperar contraseña";
            string cuerpo = $"<h2>Su código de verificación es:</h2>" +
                            $"<h1 style='color:blue;'>{codigo}</h1>" +
                            "<p>Ingrese este código en el sistema para restablecer su contraseña.</p>";

            List<string> destinatarios = new List<string> { correoDestino };
            sendMail(asunto, cuerpo, destinatarios);

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
                "ic7014508@gmail.com",
                "jcjwzarwwtnjoyyp",
                "smtp.gmail.com",
                587,
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


