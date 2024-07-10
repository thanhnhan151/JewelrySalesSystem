using JewelrySalesSystem.BAL.Models.VnPays;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IVnPayService
    {
        public string CreateUrl(CreatePaymentRequest request);
    }
}
