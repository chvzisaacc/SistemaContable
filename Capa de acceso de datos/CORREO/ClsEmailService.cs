using System.Net;
using System.Net.Mail;

namespace Capa_de_acceso_de_datos.CORREO
{
    public abstract class ClsEmailService : Clsconexion
    {
        private SmtpClient smtpClient = new();
        protected string senderMail { get; set; }
        protected string password { get; set; }
        protected string host { get; set; }
        protected int port { get; set; }
        protected bool ssl { get; set; }


        public ClsEmailService(string send, string pass, string hos, int por, bool SSL)
        {
            senderMail = send;
            password = pass;
            host = hos;
            port = por;
            ssl = SSL;
            initializeSmtpCliente();

        }
        protected void initializeSmtpCliente()
        {
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderMail, password);
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
        }

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
        public string GenerarCodigo()
        {
            Random random = new Random();
            int codigo = random.Next(10000000, 99999999);
            return codigo.ToString();
        }
    }
}
