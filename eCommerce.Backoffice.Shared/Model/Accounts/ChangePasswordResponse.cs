using System.Collections.Generic;

namespace eCommerce.Backoffice.Shared.Model.Accounts
{
    public class ChangePasswordResponse
    {        
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}