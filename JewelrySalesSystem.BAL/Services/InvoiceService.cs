using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using Microsoft.EntityFrameworkCore;

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
                    var existedProduct = await _unitOfWork.Products.GetEntityByIdAsync(item);

                    if (existedProduct != null)
                    {
                        switch (existedProduct.ProductTypeId)
                        {
                            case 2:
                                var material = await _unitOfWork.Materials.GetByNameWithIncludeAsync(existedProduct.ProductName);

                                if (material != null)
                                {
                                    invoiceDetails.Add(new InvoiceDetail
                                    {
                                        ProductId = item,
                                        ProductPrice = existedProduct.ProductPrice
                                    });
                                }

                                break;
                            case 3:
                                invoiceDetails.Add(new InvoiceDetail
                                {
                                    ProductId = item,
                                    ProductPrice = existedProduct.ProductPrice
                                });

                                break;
                            default:
                                var gem = await _unitOfWork.Gems.GetByNameWithIncludeAsync(existedProduct.ProductName);

                                if (gem != null)
                                {
                                    invoiceDetails.Add(new InvoiceDetail
                                    {
                                        ProductId = item,
                                        ProductPrice = existedProduct.ProductPrice
                                    });
                                }

                                break;
                        }
                    }
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
                    var existedProduct = await _unitOfWork.Products.GetEntityByIdAsync(item);

                    if (existedProduct != null)
                    {
                        switch (existedProduct.ProductTypeId)
                        {
                            case 2:
                                var material = await _unitOfWork.Materials.GetByNameWithIncludeAsync(existedProduct.ProductName);

                                if (material != null)
                                {
                                    invoiceDetails.Add(new InvoiceDetail
                                    {
                                        ProductId = item,
                                        ProductPrice = existedProduct.ProductPrice
                                    });
                                }

                                break;
                            case 3:
                                invoiceDetails.Add(new InvoiceDetail
                                {
                                    ProductId = item,
                                    ProductPrice = existedProduct.ProductPrice
                                });

                                break;
                            default:
                                var gem = await _unitOfWork.Gems.GetByNameWithIncludeAsync(existedProduct.ProductName);

                                if (gem != null)
                                {
                                    invoiceDetails.Add(new InvoiceDetail
                                    {
                                        ProductId = item,
                                        ProductPrice = existedProduct.ProductPrice * 70 / 100
                                    });
                                }

                                break;
                        }
                    }
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

        //private async Task<float> CalculateProductPrice(int productId)
        //{
        //    var product = await _unitOfWork.Products.GetByIdWithIncludeAsync(productId);

        //    float productPrice = 0;

        //    if (product != null)
        //    {
        //        productPrice += product.ProductionCost;
        //        if (product.ProductGems.Count > 0)
        //        {
        //            foreach (var gem in product.ProductGems)
        //            {
        //                var temp = await _unitOfWork.Gems.GetByIdWithIncludeAsync(gem.GemId);

        //                if (temp != null)
        //                {
        //                    var gemPrice = temp.GemPrice;

        //                    if (gemPrice != null)
        //                    {
        //                        productPrice += gemPrice.CutPrice + gemPrice.CaratWeightPrice + gemPrice.ClarityPrice + gemPrice.ColourPrice;
        //                    }
        //                }
        //            }
        //        }

        //        if (product.ProductMaterials.Count > 0)
        //        {
        //            foreach (var material in product.ProductMaterials)
        //            {
        //                var temp = await _unitOfWork.Materials.GetByIdWithIncludeAsync(material.MaterialId);

        //                if (temp != null)
        //                {
        //                    var materialPrice = temp.MaterialPrices.SingleOrDefault();

        //                    if (materialPrice != null) productPrice += (product.Weight * materialPrice.SellPrice);
        //                }
        //            }
        //        }
        //        productPrice += (productPrice * (product.PercentPriceRate) / 100);
        //    }
        //    return productPrice;
        //}

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

        public async Task<CreatePurchaseInvoiceRequest> AddPurchaseInvoiceAsync(CreatePurchaseInvoiceRequest createPurchaseInvoiceRequest)
        {
            var invoiceDetails = new List<InvoiceDetail>();

            float total = 0;

            int countProductHasMaterial = 0;

            if (createPurchaseInvoiceRequest.InvoiceDetails.Count > 0)
            {
                
                foreach (var item in createPurchaseInvoiceRequest.InvoiceDetails)
                {
                    var existedProduct = await _unitOfWork.Products.GetEntityByIdAsync(item);

                    if (existedProduct != null)
                    {
                        switch (existedProduct.ProductTypeId)
                        {
                            case 2:
                                var material = await _unitOfWork.Materials.GetByNameWithIncludeAsync(existedProduct.ProductName);

                                if (material != null)
                                {
                                    var buyMaterialPrice = await _unitOfWork.MaterialPrices.GetNewestMaterialPriceByMaterialIdAsync(material.MaterialId);

                                    //if(createPurchaseInvoiceRequest.InvoiceType.Equals("in"))

                                    invoiceDetails.Add(new InvoiceDetail
                                    {
                                        ProductId = item,
                                        //ProductPrice = existedProduct.ProductPrice
                                        ProductPrice = buyMaterialPrice.BuyPrice * createPurchaseInvoiceRequest.Weights.ElementAt(countProductHasMaterial) * 375 / 100,
                                    });
                                    countProductHasMaterial++;
                                }

                                break;
                            case 3:
                                var materialInProduct = await _unitOfWork.ProductMaterials.GetProductMaterialByProductIdAsync(existedProduct.ProductId);
                                var materialPrice = await _unitOfWork.MaterialPrices.GetNewestMaterialPriceByMaterialIdAsync(materialInProduct.MaterialId);
                                invoiceDetails.Add(new InvoiceDetail
                                {

                                    ProductId = item,
                                    //ProductPrice = existedProduct.ProductPrice,
                                    ProductPrice = (materialPrice.BuyPrice * materialInProduct.Weight) * 375 / 100,
                                });

                                break;
                            default:
                                var gem = await _unitOfWork.Gems.GetByNameWithIncludeAsync(existedProduct.ProductName);

                                if (gem != null)
                                {
                                    invoiceDetails.Add(new InvoiceDetail
                                    {
                                        ProductId = item,
                                        ProductPrice = existedProduct.ProductPrice * 70 / 100,

                                    });
                                }

                                break;
                        }
                    }
                }
                //if (createPurchaseInvoiceRequest.InvoiceType.Equals("in"))
                //{
                //    foreach(var item in createPurchaseInvoiceRequest.InvoiceDetails)
                //    {
                //        var existedProduct = await _unitOfWork.Products.GetEntityByIdAsync(item);
                //        if (existedProduct != null)
                //        {
                //            var existingInvoiceDetails = await _unitOfWork.
                //        }
                //    }
                //}
            }

            foreach (var item in invoiceDetails)
            {
                total += item.ProductPrice;
            }

            var invoice = new Invoice
            {
                //OrderDate = DateTime.Now,
                CustomerId = await _unitOfWork.Customers.GetCustomerByNameAsync(createPurchaseInvoiceRequest.CustomerName),
                InvoiceType = createPurchaseInvoiceRequest.InvoiceType,
                UserId = createPurchaseInvoiceRequest.UserId,
                WarrantyId = 1,
                Total = total,
                PerDiscount = 0,
                TotalWithDiscount = 0,
                InvoiceDetails = invoiceDetails
            };
            var allAreGoldProduct = true;
            //var allAreExistingProduct = true;


            //if (invoice.InvoiceType.Equals("in"))
            //{
            //    foreach (var item in invoice.InvoiceDetails)
            //    {
            //        var existingProduct = await _unitOfWork.Products.GetEntityByIdAsync(item.ProductId);
            //        if (existingProduct == null)
            //        {
            //            allAreExistingProduct = false;
            //            break;
            //        }

            //    }
            //}
            //else
            //{
            //    //var allAreGoldProduct = true;
            //    foreach (var item in invoice.InvoiceDetails)
            //    {
            //        var existingProduct = await _unitOfWork.Products.GetEntityByIdAsync(item.ProductId);
            //        if (existingProduct != null)
            //        {
            //            if (existingProduct.ProductTypeId != 2)
            //            {
            //                allAreGoldProduct = false;
            //                break;
            //            }
            //        }
            //    }
               
            //}
            if (invoice.InvoiceType.Equals("out"))
            {
                foreach (var item in invoice.InvoiceDetails)
                {
                    var existingProduct = await _unitOfWork.Products.GetEntityByIdAsync(item.ProductId);
                    if (existingProduct != null)
                    {
                        if (existingProduct.ProductTypeId != 2)
                        {
                            allAreGoldProduct = false;
                            break;
                        }
                    }
                }
            }

            //if (allAreGoldProduct == true  && countProductHasMaterial==createPurchaseInvoiceRequest.Weights.Count)  //&& allAreExistingProduct == true
            //{
            //    var result = _unitOfWork.Invoices.AddEntity(invoice);
            //}
            try
            {
                if (allAreGoldProduct == true && countProductHasMaterial == createPurchaseInvoiceRequest.Weights.Count) 
                {
                    var result = _unitOfWork.Invoices.AddEntity(invoice);
                    await _unitOfWork.CompleteAsync();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not add new purchase invoice.", ex);
            }
            
            

            return createPurchaseInvoiceRequest;
        }


    }
}

