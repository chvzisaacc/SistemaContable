using System.Net;
using System.Net.Mail;

namespace Capa_de_acceso_de_datos.CORREO
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.Clsconexion" />
    public abstract class ClsEmailService : Clsconexion
    {
        /// <summary>
        /// The SMTP client
        /// </summary>
        private SmtpClient smtpClient = new();
        /// <summary>
        /// Gets or sets the sender mail.
        /// </summary>
        /// <value>
        /// The sender mail.
        /// </value>
        protected string senderMail { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        protected string password { get; set; }
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        protected string host { get; set; }
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        protected int port { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ClsEmailService"/> is SSL.
        /// </summary>
        /// <value>
        ///   <c>true</c> if SSL; otherwise, <c>false</c>.
        /// </value>
        protected bool ssl { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ClsEmailService"/> class.
        /// </summary>
        /// <param name="send">The send.</param>
        /// <param name="pass">The pass.</param>
        /// <param name="hos">The hos.</param>
        /// <param name="por">The por.</param>
        /// <param name="SSL">if set to <c>true</c> [SSL].</param>
        public ClsEmailService(string send, string pass, string hos, int por, bool SSL)
        {
            senderMail = send;
            password = pass;
            host = hos;
            port = por;
            ssl = SSL;
            initializeSmtpCliente();

        }
        /// <summary>
        /// Initializes the SMTP cliente.
        /// </summary>
        protected void initializeSmtpCliente()
        {
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderMail, password);
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="recipienteMail">The recipiente mail.</param>
        /// <exception cref="System.Exception">Error al enviar correo: " + ex.Message</exception>
        public void sendMail(String subject, string body, List<string> recipienteMail)
        {
            var mailMessage = new MailMessage();

            try
            {
                mailMessage.From = new MailAddress(senderMail);
                foreach (string mail in recipienteMail)
                {
                    mailMessage.To.Add(mail);
                }
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;
                smtpClient.Send(mailMessage);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar correo: " + ex.Message);
            }
            finally
            {
                mailMessage.Dispose();
                smtpClient.Dispose();
            }
        }
        /// <summary>
        /// Generars the codigo.
        /// </summary>
        /// <returns></returns>
        public string GenerarCodigo()
        {
            Random random = new Random();
            int codigo = random.Next(10000000, 99999999);
            return codigo.ToString();
        }
    }
}
