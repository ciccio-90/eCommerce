namespace eCommerce.Storefront.Services.Interfaces
{
    public interface IEmailService
    {
        void SendMail(string from, string to, string subject, string body);
    }
}