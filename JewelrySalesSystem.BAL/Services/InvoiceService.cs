using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

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

            var invoice = new Invoice
            {
                OrderDate = DateTime.Now,
                UserId = createInvoiceRequest.UserId,
                Total = createInvoiceRequest.Total,
                WarrantyId = 1,
                PerDiscount = createInvoiceRequest.PerDiscount,
                TotalWithDiscount = createInvoiceRequest.Total - (createInvoiceRequest.Total * createInvoiceRequest.PerDiscount) / 100,
                InvoiceDetails = invoiceDetails
            };

            var customer = await _unitOfWork.Customers.GetCustomerByNameAsync(createInvoiceRequest.CustomerName);

            if (customer != null)
            {
                invoice.CustomerId = customer.CustomerId;
            }
            else
            {
                var newestCustomer = new Customer { FullName = createInvoiceRequest.CustomerName };

                var addedCustomer = _unitOfWork.Customers.AddEntity(newestCustomer);

                invoice.Customer = addedCustomer;
            }

            var result = _unitOfWork.Invoices.AddEntity(invoice);

            await _unitOfWork.CompleteAsync();

            return createInvoiceRequest;
        }

        public async Task<UpdateInvoiceRequest> UpdateAsync(UpdateInvoiceRequest updateInvoiceRequest)
        {
            var invoiceDetails = new List<InvoiceDetail>();

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

            var invoice = new Invoice
            {
                InvoiceId = updateInvoiceRequest.InvoiceId,
                OrderDate = DateTime.Now,
                UserId = updateInvoiceRequest.UserId,
                WarrantyId = 1,
                InvoiceDetails = invoiceDetails,
                InvoiceStatus = updateInvoiceRequest.InvoiceStatus,
                Total = updateInvoiceRequest.Total,
                PerDiscount = updateInvoiceRequest.PerDiscount,
                TotalWithDiscount = updateInvoiceRequest.Total - (updateInvoiceRequest.Total * updateInvoiceRequest.PerDiscount) / 100
            };

            var customer = await _unitOfWork.Customers.GetCustomerByNameAsync(updateInvoiceRequest.CustomerName);

            if (customer != null)
            {
                invoice.CustomerId = customer.CustomerId;
            }

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
                                    invoiceDetails.Add(new InvoiceDetail
                                    {
                                        ProductId = item,
                                        //ProductPrice = existedProduct.ProductPrice
                                        ProductPrice = buyMaterialPrice.BuyPrice,
                                    });
                                }

                                break;
                            case 3:
                                var materialInProduct = await _unitOfWork.ProductMaterials.GetProductMaterialByProductIdAsync(existedProduct.ProductId);
                                var materialPrice = await _unitOfWork.MaterialPrices.GetNewestMaterialPriceByMaterialIdAsync(materialInProduct.MaterialId);
                                invoiceDetails.Add(new InvoiceDetail
                                {

                                    ProductId = item,
                                    //ProductPrice = existedProduct.ProductPrice,
                                    ProductPrice = materialPrice.BuyPrice * materialInProduct.Weight,
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

            var customer = await _unitOfWork.Customers.GetCustomerByNameAsync(createPurchaseInvoiceRequest.CustomerName);

            var invoice = new Invoice
            {
                OrderDate = DateTime.Now,
                InvoiceType = "Purchase",
                UserId = createPurchaseInvoiceRequest.UserId,
                WarrantyId = 1,
                Total = createPurchaseInvoiceRequest.Total,
                PerDiscount = 0,
                TotalWithDiscount = createPurchaseInvoiceRequest.Total,
                InvoiceDetails = invoiceDetails
            };

            if (customer != null)
            {
                invoice.CustomerId = customer.CustomerId;
            }

            var allAreGoldProduct = true;
            var allAreExistingProduct = true;
            if (invoice.InvoiceType.Equals("Purchase"))
            {
                foreach (var item in invoice.InvoiceDetails)
                {
                    var existingProduct = await _unitOfWork.Products.GetEntityByIdAsync(item.ProductId);
                    if (existingProduct == null)
                    {
                        allAreExistingProduct = false;
                        break;
                    }

                }
            }
            else
            {
                //var allAreGoldProduct = true;
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
            if (allAreGoldProduct == true && allAreExistingProduct == true)
            {
                var result = _unitOfWork.Invoices.AddEntity(invoice);
            }

            await _unitOfWork.CompleteAsync();

            return createPurchaseInvoiceRequest;
        }

        public async Task<byte[]> GenerateInvoicePdf(int invoiceId)
        {
            var invoice = await _unitOfWork.Invoices.GetByIdWithIncludeAsync(invoiceId);
            if (invoice == null)
            {
                throw new Exception($"Invoice with id {invoiceId} not found.");
            }

            //tạo file pdf
            PdfDocument pdf = new PdfDocument();

            // thêm trang
            PdfPage page = pdf.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);


            // Khai báo font chữ cho các phần riêng biệt
            XFont boldFont = new XFont("Verdana", 15, XFontStyle.Bold);
            XFont regularFont = new XFont("Verdana", 15, XFontStyle.Regular);
            XFont largerFont = new XFont("Verdana", 20, XFontStyle.Bold);

            void DrawLabelValue(XGraphics gfx, XFont boldFont, XFont regularFont, string label, string value, double x, double y)
            {
                // In ra label in đậm
                gfx.DrawString($"{label}: ", boldFont, XBrushes.Black,
                    new XRect(x, y, gfx.PageSize.Width, gfx.PageSize.Height), XStringFormats.TopLeft);

                // Lấy chiều rộng của chuỗi label in đậm để tính toán vị trí tiếp theo
                double boldTextWidth = gfx.MeasureString($"{label}: ", boldFont).Width;

                // In ra giá trị value in nhạt, tính toán vị trí dựa trên chiều rộng của label in đậm
                gfx.DrawString($"{value}", regularFont, XBrushes.Gray,
                    new XRect(x + boldTextWidth, y, gfx.PageSize.Width, gfx.PageSize.Height), XStringFormats.TopLeft);

            }

            //title
            gfx.DrawString("Jewelry Sales System", largerFont, XBrushes.Black,
                new XRect(200, 20, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            // In ra InvoiceId
            DrawLabelValue(gfx, boldFont, regularFont, "InvoiceId", invoice.InvoiceId.ToString(), 30, 70);

            // In ra CustomerName
            DrawLabelValue(gfx, boldFont, regularFont, "CustomerName", invoice.Customer.FullName, 30, 100);

            // In ra PhoneNumber 

            DrawLabelValue(gfx, boldFont, regularFont, "PhoneNumber", invoice.Customer.PhoneNumber, 30, 130);

            // In ra OrderDate
            DrawLabelValue(gfx, boldFont, regularFont, "OrderDate", invoice.OrderDate.ToString("yyyy-MM-dd "), 30, 160);



            float tableTop = 190; // Điểm bắt đầu của bảng
            float column1Left = 30; // Cột đầu tiên
            float column2Left = 150; // Cột thứ hai
            float column3Left = 300; // Cột thứ ba

            // Tiêu đề của các cột
            gfx.DrawString("Product ID", boldFont, XBrushes.Black,
                new XRect(column1Left, tableTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            gfx.DrawString("Product Name", boldFont, XBrushes.Black,
                new XRect(column2Left, tableTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            gfx.DrawString("Price", boldFont, XBrushes.Black,
                new XRect(column3Left, tableTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);

            float dataTop = tableTop + 20;

            // Lặp qua từng sản phẩm trong danh sách và in ra thông tin của từng sản phẩm
            foreach (var item in invoice.InvoiceDetails)
            {
                // Dữ liệu của từng sản phẩm
                string productId = $"{item.ProductId}";
                string productName = $"{item.Product.ProductName}";
                string productPrice = $" {item.ProductPrice}";

                // In ra các dòng dữ liệu của từng sản phẩm
                gfx.DrawString(productId, regularFont, XBrushes.Black,
                    new XRect(column1Left, dataTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                gfx.DrawString(productName, regularFont, XBrushes.Black,
                    new XRect(column2Left, dataTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                gfx.DrawString(productPrice, regularFont, XBrushes.Black,
                    new XRect(column3Left, dataTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);

                // Di chuyển vị trí dòng dữ liệu xuống cho sản phẩm tiếp theo
                dataTop += 30;
            }


            float additionalInfoTop = dataTop + 30; // Vị trí bắt đầu in các thông tin khác

            // In ra các thông tin khác của hóa đơn trên từng dòng riêng biệt
            gfx.DrawString($"Discount: {invoice.PerDiscount}", boldFont, XBrushes.Black,
                new XRect(column3Left - 105, dataTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            //dataTop += 30;

            //gfx.DrawString($"TotalWithDiscount: {invoice.TotalWithDiscount}", boldFont, XBrushes.Black,
            //    new XRect(column3Left-160, dataTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            dataTop += 30;

            gfx.DrawString($"Total: {invoice.Total}", boldFont, XBrushes.Black,
                new XRect(column3Left - 45, dataTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);


            // Save the document to a byte array
            using (MemoryStream stream = new MemoryStream())
            {
                pdf.Save(stream, false);
                return stream.ToArray();
            }

        }

    }

}

