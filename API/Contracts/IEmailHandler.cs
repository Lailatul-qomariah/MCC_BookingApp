namespace API.Contracts
{
    public interface IEmailHandler
    {
       //method untuk custom isi email service
        void Send(string subject, string body, string toEmail);
    }
}
