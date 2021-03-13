using System.Collections.Generic;

namespace eCommerce.Backoffice.Shared.Model.Roles
{
    public class EditRoleResponse
    {
        public string Id { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}