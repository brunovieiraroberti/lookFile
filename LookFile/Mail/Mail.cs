using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Mail;

namespace Mail
{
    public class Mail
    {
        public string _smtp = string.Empty;
        public string _mailDe = string.Empty;
        public List<string> _mailPara = new List<string>();
        public string _usuario = string.Empty;
        public string _senha = string.Empty;
        public string _login = string.Empty;
        public int _Porta = 0;

        public Mail()
        { 
            
        }

        public Mail(string smtp, string mailDe, List<string> mailPara, string usuario, string senha)
        {
            _smtp = smtp;
            _mailDe = mailDe;
            _mailPara = mailPara;
            _usuario = usuario;
            _senha = senha;
        }

        public void Enviar(string texto, string assunto, string caminhoArquivoCompleto)
        {
            MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("srsvr034.seniorsolution.com.br");
            SmtpClient SmtpServer = new SmtpClient(_smtp);

            SmtpServer.Credentials = new System.Net.NetworkCredential();
            SmtpServer.Port = _Porta;
            SmtpServer.EnableSsl = true;
            

            //mail.From = new MailAddress("bruno.roberti@seniorsolution.com.br");
            mail.From = new MailAddress(_mailDe);
            //mail.To.Add("bruno.roberti@seniorsolution.com.br");
            foreach (string item in _mailPara)
            {
                mail.To.Add(item); 
            }
            
            mail.Subject = "Aviso de alteração: " + assunto;
            mail.Body = texto;

            Attachment anexo = new Attachment(caminhoArquivoCompleto + @"\" + assunto);

            mail.Attachments.Add(anexo);

            //SmtpServer.Credentials = new System.Net.NetworkCredential("bruno.roberti", "Cna_058919");
            SmtpServer.Credentials = new System.Net.NetworkCredential(_login, _senha);

            SmtpServer.Send(mail);

        }
    }
}
