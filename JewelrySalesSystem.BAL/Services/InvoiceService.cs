using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoiceService(
            IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<GetInvoiceResponse>> PaginationAsync(
            string? invoiceStatus,
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => _mapper.Map<PaginatedList<GetInvoiceResponse>>(await _unitOfWork.Invoices.PaginationAsync(invoiceStatus, searchTerm, sortColumn, sortOrder, page, pageSize));

        public async Task<CreateInvoiceRequest> AddAsync(CreateInvoiceRequest createInvoiceRequest)
        {
            var invoiceDetails = new List<InvoiceDetail>();

            float total = 0;

            if (createInvoiceRequest.InvoiceDetails.Count > 0)
            {
                foreach (var item in createInvoiceRequest.InvoiceDetails)
                {
                    invoiceDetails.Add(new InvoiceDetail
                    {
                        ProductId = item,
                        ProductPrice = await CalculateProductPrice(item)
                    });
                }
            }

            foreach (var item in invoiceDetails)
            {
                total += item.ProductPrice;
            }

            var invoice = new Invoice
            {
                OrderDate = DateTime.Now,
                CustomerId = await _unitOfWork.Customers.GetCustomerByNameAsync(createInvoiceRequest.CustomerName),
                UserId = createInvoiceRequest.UserId,
                WarrantyId = createInvoiceRequest.WarrantyId,
                Total = total,
                PerDiscount = createInvoiceRequest.PerDiscount,
                TotalWithDiscount = total - (total * createInvoiceRequest.PerDiscount) / 100,
                InvoiceDetails = invoiceDetails
            };

            var result = _unitOfWork.Invoices.AddEntity(invoice);

            await _unitOfWork.CompleteAsync();

            return createInvoiceRequest;
        }

        public async Task<UpdateInvoiceRequest> UpdateAsync(UpdateInvoiceRequest updateInvoiceRequest)
        {
            var invoiceDetails = new List<InvoiceDetail>();

            float total = 0;

            if (updateInvoiceRequest.InvoiceDetails.Count > 0)
            {
                foreach (var item in updateInvoiceRequest.InvoiceDetails)
                {
                    invoiceDetails.Add(new InvoiceDetail
                    {
                        ProductId = item,
                        ProductPrice = await CalculateProductPrice(item)
                    });
                }
            }

            foreach (var item in invoiceDetails)
            {
                total += item.ProductPrice;
            }

            var invoice = new Invoice
            {
                InvoiceId = updateInvoiceRequest.InvoiceId,
                OrderDate = DateTime.Now,
                CustomerId = await _unitOfWork.Customers.GetCustomerByNameAsync(updateInvoiceRequest.CustomerName),
                UserId = updateInvoiceRequest.UserId,
                WarrantyId = updateInvoiceRequest.WarrantyId,
                InvoiceDetails = invoiceDetails,
                InvoiceStatus = updateInvoiceRequest.InvoiceStatus,
                Total = total,
                PerDiscount = updateInvoiceRequest.PerDiscount,
                TotalWithDiscount = total - (total * updateInvoiceRequest.PerDiscount) / 100
            };

            await _unitOfWork.Invoices.UpdateInvoice(invoice);

            await _unitOfWork.CompleteAsync();

            return updateInvoiceRequest;
        }



        public async Task<GetInvoiceResponse?> GetByIdWithIncludeAsync(int id) => _mapper.Map<GetInvoiceResponse>(await _unitOfWork.Invoices.GetByIdWithIncludeAsync(id));

        private async Task<float> CalculateProductPrice(int productId)
        {
            var product = await _unitOfWork.Products.GetByIdWithIncludeAsync(productId);

            float productPrice = 0;

            if (product != null)
            {
                productPrice += product.ProductionCost;
                if (product.ProductGems.Count > 0)
                {
                    foreach (var gem in product.ProductGems)
                    {
                        var temp = await _unitOfWork.Gems.GetByIdWithIncludeAsync(gem.GemId);

                        if (temp != null)
                        {
                            var gemPrice = temp.GemPrice;

                            if (gemPrice != null)
                            {
                                productPrice += gemPrice.CutPrice + gemPrice.CaratWeightPrice + gemPrice.ClarityPrice + gemPrice.ColourPrice;
                            }
                        }
                    }
                }

                if (product.ProductMaterials.Count > 0)
                {
                    foreach (var material in product.ProductMaterials)
                    {
                        var temp = await _unitOfWork.Materials.GetByIdWithIncludeAsync(material.MaterialId);

                        if (temp != null)
                        {
                            var materialPrice = temp.MaterialPrices.SingleOrDefault();

                            if (materialPrice != null) productPrice += (product.Weight * materialPrice.SellPrice);
                        }
                    }
                }
                productPrice += (productPrice * (product.PercentPriceRate) / 100);
            }
            return productPrice;
        }

        public async Task DeleteInvoice(int id)
        {
            await _unitOfWork.Invoices.DeleteById(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetInvoiceResponse?> GetByIdAsync(int id) => _mapper.Map<GetInvoiceResponse>(await _unitOfWork.Invoices.GetEntityByIdAsync(id));

        public async Task ChangeInvoiceStatus(int id)
        {
            await _unitOfWork.Invoices.ChangeInvoiceStatus(id);

            await _unitOfWork.CompleteAsync();
        }

        public async Task CancelInvoice(int id)
        {
            await _unitOfWork.Invoices.CancelInvoice(id);

            await _unitOfWork.CompleteAsync();
        }
    }
}
