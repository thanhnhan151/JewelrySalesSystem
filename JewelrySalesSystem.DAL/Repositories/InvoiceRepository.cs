using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(
            JewelryDbContext context,
            ILogger logger) : base(context, logger)
        {
        }

        public async Task<PaginatedList<Invoice>> PaginationAsync(
            string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize)
        {
            IQueryable<Invoice> invoicesQuery = _dbSet.Include(i => i.InvoiceDetails)
                                                            .ThenInclude(i => i.Product);

            //if (!string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    invoicesQuery = invoicesQuery.Where(c =>
            //        c.FullName.Contains(searchTerm) ||
            //        c.PhoneNumber.Contains(searchTerm) ||
            //        c.Email.Contains(searchTerm));
            //}

            if (sortOrder?.ToLower() == "desc")
            {
                invoicesQuery = invoicesQuery.OrderByDescending(GetSortProperty(sortColumn));
            }
            else
            {
                invoicesQuery = invoicesQuery.OrderBy(GetSortProperty(sortColumn));
            }

            var invoices = await PaginatedList<Invoice>.CreateAsync(invoicesQuery, page, pageSize);

            return invoices;
        }

        private static Expression<Func<Invoice, object>> GetSortProperty(string? sortColumn)
        => sortColumn?.ToLower() switch
        {
            "date" => invoice => invoice.OrderDate,
            //"dob" => invoice => invoice.DoB,
            _ => invoice => invoice.InvoiceId
        };

        public async Task<Invoice?> GetByIdWithIncludeAsync(int id)
        {
            var result = await _dbSet.Include(i => i.InvoiceDetails)
                                    .ThenInclude(i => i.Product)
                               .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (result == null) return null;
            return result;
        }
    }
}
