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
            string? invoiceStatus,
            string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize)
        {
            IQueryable<Invoice> invoicesQuery = _dbSet.OrderByDescending(i => i.OrderDate)
                                                      .Include(i => i.InvoiceDetails)
                                                            .ThenInclude(i => i.Product)
                                                      .Include(i => i.Customer)
                                                      .Include(i => i.User)
                                                      .Include(i => i.Warranty);

            if (invoiceStatus != null)
            {
                if (invoiceStatus.Equals("Pending"))
                {
                    invoicesQuery = invoicesQuery.Where(i => i.InvoiceStatus.Equals(invoiceStatus));
                }
                else if (invoiceStatus.Equals("Processing"))
                {
                    invoicesQuery = invoicesQuery.Where(i => i.InvoiceStatus.Equals(invoiceStatus));
                }
                else if (invoiceStatus.Equals("Delivered"))
                {
                    invoicesQuery = invoicesQuery.Where(i => i.InvoiceStatus.Equals(invoiceStatus));
                }
                else if (invoiceStatus.Equals("Cancelled"))
                {
                    invoicesQuery = invoicesQuery.Where(i => i.InvoiceStatus.Equals(invoiceStatus));
                }
            }

            //if (!string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    invoicesQuery = invoicesQuery.Where(c =>
            //        c.FullName.Contains(searchTerm) ||
            //        c.PhoneNumber.Contains(searchTerm) ||
            //        c.Email.Contains(searchTerm));
            //}

            //if (sortOrder?.ToLower() == "desc")
            //{
            //    invoicesQuery = invoicesQuery.OrderByDescending(GetSortProperty(sortColumn));
            //}
            //else
            //{
            //    invoicesQuery = invoicesQuery.OrderBy(GetSortProperty(sortColumn));
            //}

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
            var found = await _dbSet.FindAsync(id) ?? throw new Exception($"Invoice with {id} is not found!");
            found.Status = false;
            _dbSet.Update(found);
        }

        public async Task UpdateInvoice(Invoice invoice)
        {
            var existingInvoice = await _dbSet
                .Include(i => i.InvoiceDetails)
                .FirstOrDefaultAsync(i => i.InvoiceId == invoice.InvoiceId) ?? throw new Exception($"Invoice with id {invoice.InvoiceId} not found.");
            existingInvoice.OrderDate = invoice.OrderDate;
            existingInvoice.CustomerId = invoice.CustomerId;
            existingInvoice.UserId = invoice.UserId;
            existingInvoice.WarrantyId = invoice.WarrantyId;
            existingInvoice.Status = invoice.Status;
            existingInvoice.InvoiceType = invoice.InvoiceType;


            var detailsToRemove = existingInvoice.InvoiceDetails
                .Where(d => !invoice.InvoiceDetails.Any(nd => nd.ProductId == d.ProductId))
                .ToList();

            foreach (var detail in detailsToRemove)
            {
                _context.InvoiceDetails.Remove(detail);
            }

            foreach (var newDetail in invoice.InvoiceDetails)
            {
                var existingDetail = existingInvoice.InvoiceDetails
                    .FirstOrDefault(d => d.ProductId == newDetail.ProductId);

                if (existingDetail != null)
                {
                    existingDetail.ProductPrice = newDetail.ProductPrice;
                }
                else
                {
                    existingInvoice.InvoiceDetails.Add(new InvoiceDetail
                    {
                        ProductId = newDetail.ProductId,
                        ProductPrice = newDetail.ProductPrice,
                        InvoiceId = existingInvoice.InvoiceId,
                    });
                }
            }

            _dbSet.Update(existingInvoice);
            await _context.SaveChangesAsync();
        }

    }
}
