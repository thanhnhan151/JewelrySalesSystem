using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<Invoice>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => await _unitOfWork.Invoices.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            var result = _unitOfWork.Invoices.AddEntity(invoice);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            _unitOfWork.Invoices.UpdateEntity(invoice);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<Invoice?> GetByIdAsync(int id) => await _unitOfWork.Invoices.GetByIdAsync(id);
    }
}
