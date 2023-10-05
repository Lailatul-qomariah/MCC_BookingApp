using API.Contracts;
using System.Net.Mail;

namespace API.Utilities.Handlers
{
    public class EmailHandler : IEmailHandler
        //implement interface IEmail Handler
    {
        //properti untuk email service
        private string _server; //properti untuk menyimpan nama server email
        private int _port; //properti untuk menyimpan port email
        private string _fromEmailAddress; ///properti untuk menyimpan pengirm email

        public EmailHandler(string server, int port, string fromEmailAddress)
        {// inisiasi semua properti kedalam constructor
            _server = server;
            _port = port;
            _fromEmailAddress = fromEmailAddress;
        }

        public void Send(string subject, string body, string toEmail)
        {//implementasi method dari interface

            //bikin instance untuk custom isi emailnya
            var message = new MailMessage()
            {
                From = new MailAddress(_fromEmailAddress), //alamat email pengirim
                Subject = subject, //setting subject email
                Body = body, //setting mengatur isi pesan email
                IsBodyHtml = true //setting isi pesan bisa gambar dll
            };

            message.To.Add(new MailAddress(toEmail)); // Menambahkan alamat email penerima
            //instance SmptpClient
            using var smtpClient = new SmtpClient(_server, _port);
            smtpClient.Send(message); //send message
        }
    }
}
