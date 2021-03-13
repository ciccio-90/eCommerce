namespace eCommerce.Backoffice.Shared.Model.Accounts
{
    public class EmailConfirmationRequest
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}