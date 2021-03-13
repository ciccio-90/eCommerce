using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Customers
{
    public static class CustomerBusinessRules
    {
        public static readonly BusinessRule FirstNameRequired = new BusinessRule() { Property = "FirstName", Rule = "A customer must have a first name." };
        public static readonly BusinessRule SecondNameRequired = new BusinessRule() { Property = "SecondName", Rule = "A customer must have a second name." };
        public static readonly BusinessRule EmailRequired = new BusinessRule() { Property = "Email", Rule = "A customer must have a valid email address." };
        public static readonly BusinessRule IdentityTokenRequired = new BusinessRule() { Property = "IdentityToken", Rule = "A customer must have an identity token." };
    }
}