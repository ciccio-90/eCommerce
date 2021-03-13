namespace eCommerce.Storefront.Controllers.ViewModels.Account
{
    public class AccountView
    {
        public AccountView()
        {
            CallBackSettings = new CallBackSettings();
        }
        
        public CallBackSettings CallBackSettings { get; set; }
        public bool HasIssue { get; set; }
        public string Message { get; set; }
    }
}