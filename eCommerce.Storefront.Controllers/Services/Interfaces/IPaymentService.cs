using System.Threading.Tasks;
using eCommerce.Storefront.Controllers.Models;
using eCommerce.Storefront.Model.Orders;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Storefront.Controllers.Services.Interfaces
{
    public interface IPaymentService
    {
        PaymentPostData GeneratePostDataFor(OrderPaymentRequest orderRequest);
        Task<TransactionResult> HandleCallBack(OrderPaymentRequest orderRequest, IFormCollection collection);
        int GetOrderIdFor(IFormCollection collection);
    }
}