using System.Threading.Tasks;
using eCommerce.Storefront.Model.Payments;
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