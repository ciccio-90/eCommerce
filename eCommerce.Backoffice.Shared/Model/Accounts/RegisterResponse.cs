using System.Collections.Generic;

namespace eCommerce.Backoffice.Shared.Model.Accounts
{
    public class RegisterResponse
    {
        public bool IsSuccess { get; set; }
        public bool EmailExist { get; set; }        
        public IEnumerable<string> Errors { get; set; }
        public bool EmailConfirmed { get; set; }     
    }
}