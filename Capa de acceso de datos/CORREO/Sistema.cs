namespace Capa_de_acceso_de_datos.CORREO
{
    public class Sistema : ClsEmailService
    {
        public static Dictionary<string, string> CodigosVerificacion = new Dictionary<string, string>();

        public Sistema(string correoRemitente, string contrasenaRemitente, string v, int v1, bool v2)
            : base(correoRemitente, contrasenaRemitente, "smtp.gmail.com", 587, true)
        {
        }

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

        public bool ValidarCodigo(string correo, string codigoIngresado)
        {
            return CodigosVerificacion.ContainsKey(correo) && CodigosVerificacion[correo] == codigoIngresado;
        }
    }
}


