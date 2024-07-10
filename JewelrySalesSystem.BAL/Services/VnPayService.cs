using JewelrySalesSystem.BAL.Helpers;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.VnPays;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace JewelrySalesSystem.BAL.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly VnPayConfig _vnPayConfig;

        public VnPayService(
            IHttpContextAccessor httpContextAccessor
            , IOptions<VnPayConfig> vnPayConfigOptions)
        {
            _httpContextAccessor = httpContextAccessor;
            _vnPayConfig = vnPayConfigOptions.Value;
        }

        public string CreateUrl(CreatePaymentRequest request)
        {
            var ipAddress = _httpContextAccessor?.HttpContext?.Connection?.LocalIpAddress?.ToString();

            var paymentUrl = string.Empty;

            var vnPayRequest = new VnPayRequest(
                _vnPayConfig.Version,
                _vnPayConfig.TmnCode, 
                DateTime.Now,
                ipAddress ?? string.Empty, 
                request.RequiredAmount ?? 0, 
                "other", 
                request.PaymentContent ?? string.Empty, 
                _vnPayConfig.ReturnUrl, 
                "Hello World");

            paymentUrl = vnPayRequest.GetLink(_vnPayConfig.PaymentUrl, _vnPayConfig.HashSecret);

            return paymentUrl;
        }
    }
}
