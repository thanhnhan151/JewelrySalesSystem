using System.Reflection.Metadata;
using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using MigraDoc.DocumentObjectModel.Tables;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.BarCodes;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.Advanced;
using ZXing;
using ZXing.Common;
using ZXing.OneD;
using ZXing.Windows.Compatibility;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Numerics;
using System.Text;

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
            string? invoiceType,
            string? inOrOut,
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            bool isActive,
            int page,
            int pageSize)
        => _mapper.Map<PaginatedList<GetInvoiceResponse>>(await _unitOfWork.Invoices.PaginationAsync(invoiceStatus, invoiceType, inOrOut, searchTerm, sortColumn, sortOrder, isActive, page, pageSize));

        public async Task<CreateInvoiceRequest> AddAsync(CreateInvoiceRequest createInvoiceRequest)
        {
            var invoiceDetails = new List<InvoiceDetail>();

            if (createInvoiceRequest.InvoiceDetails.Count > 0)
            {
                foreach (var item in createInvoiceRequest.InvoiceDetails)
                {
                    var existedProduct = await _unitOfWork.Products.GetEntityByIdAsync(item.ProductId);

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
                                        ProductId = item.ProductId,
                                        ProductPrice = existedProduct.ProductPrice * item.Quantity,
                                        Quantity = item.Quantity
                                    });
                                }

                                break;
                            case 3:
                                invoiceDetails.Add(new InvoiceDetail
                                {
                                    ProductId = item.ProductId,
                                    ProductPrice = existedProduct.ProductPrice * item.Quantity,
                                    Quantity = item.Quantity
                                });

                                break;
                            default:
                                var gem = await _unitOfWork.Gems.GetByNameWithIncludeAsync(existedProduct.ProductName);

                                if (gem != null)
                                {
                                    invoiceDetails.Add(new InvoiceDetail
                                    {
                                        ProductId = item.ProductId,
                                        ProductPrice = existedProduct.ProductPrice * item.Quantity,
                                        Quantity = item.Quantity
                                    });
                                }

                                break;
                        }
                        existedProduct.Quantity -= item.Quantity;
                        _unitOfWork.Products.UpdateEntity(existedProduct);
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
                var newestCustomer = new Customer { FullName = createInvoiceRequest.CustomerName, PhoneNumber = createInvoiceRequest.PhoneNumber };

                invoice.PerDiscount = 0;

                invoice.Customer = newestCustomer;
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
                    var existedProduct = await _unitOfWork.Products.GetEntityByIdAsync(item.ProductId);

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
                                        ProductId = item.ProductId,
                                        ProductPrice = existedProduct.ProductPrice * item.Quantity,
                                        Quantity = item.Quantity
                                    });
                                }

                                break;
                            case 3:
                                invoiceDetails.Add(new InvoiceDetail
                                {
                                    ProductId = item.ProductId,
                                    ProductPrice = existedProduct.ProductPrice * item.Quantity,
                                    Quantity = item.Quantity
                                });

                                break;
                            default:
                                var gem = await _unitOfWork.Gems.GetByNameWithIncludeAsync(existedProduct.ProductName);

                                if (gem != null)
                                {
                                    invoiceDetails.Add(new InvoiceDetail
                                    {
                                        ProductId = item.ProductId,
                                        ProductPrice = existedProduct.ProductPrice * item.Quantity,
                                        Quantity = item.Quantity
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

            var customer = await _unitOfWork.Customers.GetCustomerByNameAsync(createPurchaseInvoiceRequest.CustomerName);

            var invoice = new Invoice
            {
                //OrderDate = DateTime.Now,
                //CustomerId = await _unitOfWork.Customers.GetCustomerByNameAsync(createPurchaseInvoiceRequest.CustomerName),
                InvoiceType = "Purchase",
                InOrOut = createPurchaseInvoiceRequest.InOrOut,
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
            else
            {
                var newestCustomer = new Customer { FullName = createPurchaseInvoiceRequest.CustomerName, PhoneNumber = createPurchaseInvoiceRequest.PhoneNumber };

                invoice.Customer = newestCustomer;
            }

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

        //public async Task<byte[]> GenerateInvoicePdf(int invoiceId)
        //{
        //    var invoice = await _unitOfWork.Invoices.GetByIdWithIncludeAsync(invoiceId);
        //    if (invoice == null)
        //    {
        //        throw new Exception($"Invoice with id {invoiceId} not found.");
        //    }

        //    //tạo file pdf
        //    PdfDocument pdf = new PdfDocument();

        //    // thêm trang
        //    PdfPage page = pdf.AddPage();

        //    XGraphics gfx = XGraphics.FromPdfPage(page);


        //    // Khai báo font chữ cho các phần riêng biệt
        //    XFont boldFont = new XFont("Verdana", 15, XFontStyle.Bold);
        //    XFont regularFont = new XFont("Verdana", 15, XFontStyle.Regular);
        //    XFont largerFont = new XFont("Verdana", 20, XFontStyle.Bold);

        //    void DrawLabelValue(XGraphics gfx, XFont boldFont, XFont regularFont, string label, string value, double x, double y)
        //    {
        //        // In ra label in đậm
        //        gfx.DrawString($"{label}: ", boldFont, XBrushes.Black,
        //            new XRect(x, y, gfx.PageSize.Width, gfx.PageSize.Height), XStringFormats.TopLeft);

        //        // Lấy chiều rộng của chuỗi label in đậm để tính toán vị trí tiếp theo
        //        double boldTextWidth = gfx.MeasureString($"{label}: ", boldFont).Width;

        //        // In ra giá trị value in nhạt, tính toán vị trí dựa trên chiều rộng của label in đậm
        //        gfx.DrawString($"{value}", regularFont, XBrushes.Gray,
        //            new XRect(x + boldTextWidth, y, gfx.PageSize.Width, gfx.PageSize.Height), XStringFormats.TopLeft);

        //    }

        //    //title
        //    gfx.DrawString("Jewelry Sales System", largerFont, XBrushes.Black,
        //        new XRect(200, 20, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
        //    // In ra InvoiceId
        //    DrawLabelValue(gfx, boldFont, regularFont, "InvoiceId", invoice.InvoiceId.ToString(), 30, 70);

        //    // In ra CustomerName
        //    DrawLabelValue(gfx, boldFont, regularFont, "CustomerName", invoice.Customer.FullName, 30, 100);

        //    // In ra PhoneNumber 

        //    DrawLabelValue(gfx, boldFont, regularFont, "PhoneNumber", invoice.Customer.PhoneNumber, 30, 130);

        //    // In ra OrderDate
        //    DrawLabelValue(gfx, boldFont, regularFont, "OrderDate", invoice.OrderDate.ToString("yyyy-MM-dd "), 30, 160);



        //    float tableTop = 190; // Điểm bắt đầu của bảng
        //    float column1Left = 30; // Cột đầu tiên
        //    float column2Left = 150; // Cột thứ hai
        //    float column3Left = 300; // Cột thứ ba

        //    // Tiêu đề của các cột
        //    gfx.DrawString("Product ID", boldFont, XBrushes.Black,
        //        new XRect(column1Left, tableTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
        //    gfx.DrawString("Product Name", boldFont, XBrushes.Black,
        //        new XRect(column2Left, tableTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
        //    gfx.DrawString("Price", boldFont, XBrushes.Black,
        //        new XRect(column3Left, tableTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);

        //    float dataTop = tableTop + 20;

        //    // Lặp qua từng sản phẩm trong danh sách và in ra thông tin của từng sản phẩm
        //    foreach (var item in invoice.InvoiceDetails)
        //    {
        //        // Dữ liệu của từng sản phẩm
        //        string productId = $"{item.ProductId}";
        //        string productName = $"{item.Product.ProductName}";
        //        string productPrice = $" {item.ProductPrice}";

        //        // In ra các dòng dữ liệu của từng sản phẩm
        //        gfx.DrawString(productId, regularFont, XBrushes.Black,
        //            new XRect(column1Left, dataTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
        //        gfx.DrawString(productName, regularFont, XBrushes.Black,
        //            new XRect(column2Left, dataTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
        //        gfx.DrawString(productPrice, regularFont, XBrushes.Black,
        //            new XRect(column3Left, dataTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);

        //        // Di chuyển vị trí dòng dữ liệu xuống cho sản phẩm tiếp theo
        //        dataTop += 30;
        //    }


        //    float additionalInfoTop = dataTop + 30; // Vị trí bắt đầu in các thông tin khác

        //    // In ra các thông tin khác của hóa đơn trên từng dòng riêng biệt
        //    gfx.DrawString($"Discount: {invoice.PerDiscount}", boldFont, XBrushes.Black,
        //        new XRect(column3Left - 105, dataTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
        //    //dataTop += 30;

        //    //gfx.DrawString($"TotalWithDiscount: {invoice.TotalWithDiscount}", boldFont, XBrushes.Black,
        //    //    new XRect(column3Left-160, dataTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
        //    dataTop += 30;

        //    gfx.DrawString($"Total: {invoice.Total}", boldFont, XBrushes.Black,
        //        new XRect(column3Left - 45, dataTop, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);


        //    byte[] barcodeBytes = await GenerateBarCode(invoiceId);
        //    //XImage barcodeImage = XImage.FromStream(new MemoryStream(barcodeBytes));
        //    XImage barcodeImage = XImage.FromStream(() => new MemoryStream(barcodeBytes));
        //    // Tính toán vị trí để đặt barcode trên trang PDF
        //    float barcodeX = 50; // Khoảng cách từ lề trái
        //    float barcodeY = (float)(page.Height - 150); // Khoảng cách từ lề trên
        //    //gfx = XGraphics.FromPdfPage(page);
        //    gfx.DrawImage(barcodeImage, barcodeX, barcodeY);
        //    // Save the document to a byte array

        //    using (MemoryStream stream = new MemoryStream())
        //    {

        //        pdf.Save(stream, false);
        //        return stream.ToArray();
        //    }



        //}

        public async Task<byte[]> GenerateInvoicePdf(int invoiceId)
        {
            var invoice = await _unitOfWork.Invoices.GetByIdWithIncludeAsync(invoiceId);
            if (invoice == null)
            {
                throw new Exception($"Invoice with id {invoiceId} not found.");
            }



            MigraDoc.DocumentObjectModel.Document doc = new();
            Section sec = doc.AddSection();

            var titleStyle = doc.AddStyle("TitleStyle", "Normal");
            titleStyle.Font.Name = "Times New Roman";
            titleStyle.Font.Size = 13;
            titleStyle.Font.Bold = true;

            var arialStyle = doc.AddStyle("ArialStyle", "Normal");
            arialStyle.Font.Name = "Arial";
            arialStyle.Font.Size = 15;



            Paragraph titleParagraph = sec.AddParagraph("Jewelry Sales System", "TitleStyle");
            titleParagraph.Format.Alignment = ParagraphAlignment.Center;
            titleParagraph.Format.Font.Size = 24;
            sec.AddParagraph();

            Paragraph titleType;
            if (invoice.InvoiceType.Equals("Sale"))
            {
                titleType = sec.AddParagraph("Sale Invoice", "TitleStyle");

            }
            else
            {
                titleType = sec.AddParagraph("Purchase Invoice", "TitleStyle");
            }
            titleType.Format.Alignment = ParagraphAlignment.Center;
            titleType.Format.Font.Size = 16;
            titleParagraph.Format.Alignment = ParagraphAlignment.Center;
            sec.AddParagraph();

            sec.AddParagraph($"Invoice ID: {invoice.InvoiceId}", "TitleStyle");
            sec.AddParagraph($"Customer Name: {invoice.Customer.FullName}", "TitleStyle");
            sec.AddParagraph($"Phone: {invoice.Customer.PhoneNumber}", "TitleStyle");
            sec.AddParagraph($"Order Date: {invoice.OrderDate.ToString("yyyy-MM-dd")}", "TitleStyle");

            sec.AddParagraph();
            Table table = new();
            table.Borders.Width = 0.5;
            Column column = table.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromCentimeter(5));
            column = table.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromCentimeter(10));
            column = table.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromCentimeter(3));

            Row row = table.AddRow();
            Cell cell = row.Cells[0];
            cell = row.Cells[0];
            Paragraph itemParagraph = cell.AddParagraph("Item");
            itemParagraph.Format.Font.Bold = true;
            itemParagraph.Format.Alignment = ParagraphAlignment.Center;
            cell = row.Cells[1];
            Paragraph productNameParagraph = cell.AddParagraph("Product Name");
            productNameParagraph.Format.Font.Bold = true;
            productNameParagraph.Format.Alignment = ParagraphAlignment.Center;
            cell = row.Cells[2];
            Paragraph priceParagraph = cell.AddParagraph("Price");
            priceParagraph.Format.Font.Bold = true;
            priceParagraph.Format.Alignment = ParagraphAlignment.Center;
            var count = 1;
            foreach (var item in invoice.InvoiceDetails)
            {
                row = table.AddRow();
                row.Cells[0].AddParagraph($"{count}");
                row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[1].AddParagraph(item.Product.ProductName);
                row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[2].AddParagraph(item.ProductPrice.ToString());
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                count++;
            }
            row = table.AddRow();
            row.Cells[0].AddParagraph("Total");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[2].AddParagraph($"{invoice.Total}");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            sec.AddParagraph();



            doc.LastSection.Add(table);
            for (int i = 0; i < 3; i++)
            {
                sec.AddParagraph();
            }
            var saleTitle = sec.AddParagraph("Salesman", "TitleStyle");
            saleTitle.Format.Alignment = ParagraphAlignment.Right;

            var barcodeBytes = await GenerateBarCode(invoiceId);
            if (barcodeBytes != null)
            {
                string tempFilePath = Path.Combine(Path.GetTempPath(), "barcode.png");
                File.WriteAllBytes(tempFilePath, barcodeBytes);

                var image = sec.AddImage(tempFilePath);
                image.Width = "4cm";
                image.Height = "2cm";
                image.Left = "2cm";

                // Xóa tệp tạm thời sau khi sử dụng xong
                //File.Delete(tempFilePath);
            }
#pragma warning disable
            MigraDoc.Rendering.PdfDocumentRenderer docRend = new MigraDoc.Rendering.PdfDocumentRenderer(true);
            docRend.Document = doc;


            docRend.RenderDocument();


            using (MemoryStream ms = new MemoryStream())
            {

                //pdf.Save(ms);
                docRend.PdfDocument.Save(ms);
                return ms.ToArray();
            }



        }

        public async Task ChangePendingToDraft(int id)
        {
            await _unitOfWork.Invoices.ChangePendingToDraft(id);

            await _unitOfWork.CompleteAsync();
        }

        public async Task<byte[]> GenerateBarCode(int id)
        {
            var invoice = await _unitOfWork.Invoices.GetByIdWithIncludeAsync(id);

            if (invoice != null)
            {
#pragma warning disable
                var barcodeWriter = new BarcodeWriter
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Height = 100,
                        Width = 100,

                    }
                };
                var count = 0;
                var barcodeContent = $"{invoice.InvoiceId}|{invoice.OrderDate.ToString("yyyy-MM-dd")}|";
                foreach (var detail in invoice.InvoiceDetails)
                {
                    barcodeContent += $"{detail.ProductId}";
                    if (invoice.InvoiceDetails.Count > 1 && count < invoice.InvoiceDetails.Count)
                    {
                        barcodeContent += $",";
                    }
                }
                var barcodeBitmap = barcodeWriter.Write(barcodeContent);

                byte[] barcodeBytes;
                using (var memoryStream = new MemoryStream())
                {
                    barcodeBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    barcodeBytes = memoryStream.ToArray();
                }
                return barcodeBytes;
            }
            return null;
        }

        public async Task<byte[]> GenerateInvoiceExcel(int month, int year)
        {
            var isValidYear = await _unitOfWork.Invoices.CheckValidYear(year);
            if (!IsValidMonth(month) || !isValidYear)
            {
                return null;
            }
            var invoices = await _unitOfWork.Invoices.GetInvoicesForMonthAsync(month, year);

            var csvBuilder = new StringBuilder();

            csvBuilder.AppendLine($"Invoice ID\tOrder Date\tCustomer ID\tUser ID\tInvoice Type\tPer Discount\tTotal");

            foreach (var invoice in invoices)
            {
                csvBuilder.AppendLine($"{invoice.InvoiceId}\t{invoice.OrderDate}\t{invoice.CustomerId}\t{invoice.UserId}\t{invoice.InvoiceType}\t{invoice.PerDiscount}\t{invoice.Total}");
            }

            var totalInvoices = invoices.Count;

            var totalSum = invoices.Sum(i => i.Total);

            csvBuilder.AppendLine($"Total Invoices: {totalInvoices}");
            csvBuilder.AppendLine($"Total: {totalSum}");

            var byteArray = Encoding.UTF8.GetBytes(csvBuilder.ToString());

            return byteArray;

            bool IsValidMonth(int month)
            {
                return month >= 1 && month <= 12;
            }
        }

        public async Task<float> GetMonthlyRevenueAsync(int id, int month , int year)
        {
            var invoices = await _unitOfWork.Invoices.GetInvoicesByEmployeeAndMonthAsync(id, month, year);
            return invoices.Sum(invoice => invoice.Total);
        }

        public async Task<int> GetTransactionCountAsync(int id, int month, int year)
        {
            var invoices = await _unitOfWork.Invoices.GetInvoicesByEmployeeAndMonthAsync(id, month, year);
            return invoices.Count();
        }

    }
}


