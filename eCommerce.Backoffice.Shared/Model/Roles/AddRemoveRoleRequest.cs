namespace eCommerce.Backoffice.Shared.Model.Roles
{
    public class AddRemoveRoleRequest
    {
        public string RoleId { get; set; }
        public string UserId { get; set; }
        public bool Add { get; set; }
    }
}