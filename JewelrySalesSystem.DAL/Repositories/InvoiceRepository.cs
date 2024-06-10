using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Net.Http.Headers;

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
                                                            .ThenInclude(i => i.Product)
                                                      .Include(i => i.Customer)
                                                      .Include(i => i.User)
                                                      .Include(i => i.Warranty);

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
            var result = await _dbSet
                                .Include(i => i.InvoiceDetails)
                                    .ThenInclude(i => i.Product)
                                .Include(i => i.Customer)
                                .Include(i => i.User)
                                .Include(i => i.Warranty)
                                .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (result == null) return null;

            return result;
        }

        public async Task DeleteById(int id)
        {
            var found = await _dbSet.FindAsync(id);
            if (found == null)
            {
                throw new Exception($"{id} is not found!");
            }
            found.Status = false;
            _dbSet.Update(found);
        }
    }
}
