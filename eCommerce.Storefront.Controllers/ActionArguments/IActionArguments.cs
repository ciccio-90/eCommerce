namespace eCommerce.Storefront.Controllers.ActionArguments
{
    public interface IActionArguments
    {
        string GetValueForArgument(ActionArgumentKey key);
    }
}