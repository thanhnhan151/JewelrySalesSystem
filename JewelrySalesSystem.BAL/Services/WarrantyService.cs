using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Roles;
using JewelrySalesSystem.BAL.Models.Users;
using JewelrySalesSystem.BAL.Models.Warranties;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.Rendering;
using System.Data;

namespace JewelrySalesSystem.BAL.Services
{
    public class WarrantyService : IWarrantyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WarrantyService(IUnitOfWork unitOfWork,
                IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GetWarrantyResponse>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => _mapper.Map<PaginatedList<GetWarrantyResponse>>(await _unitOfWork.Warranties.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize));


        //Update information of warranty in the database
        //Use AutoMapper
        public async Task UpdateAsync(UpdateWarrantyRequest updateWarrantyRequest)
        {
            _unitOfWork.Warranties.UpdateEntity(_mapper.Map<Warranty>(updateWarrantyRequest));
            await _unitOfWork.CompleteAsync();
        }
        public async Task<GetWarrantyResponse?> GetWarrantyById(int id) => _mapper.Map<GetWarrantyResponse>(await _unitOfWork.Warranties.GetEntityByIdAsync(id));
        

        //change here
        public async Task<CreateWarrantyRequest> AddNewWarranty(CreateWarrantyRequest createWarrantyRequest)
        {
            var warranty = _unitOfWork.Warranties.AddEntity(_mapper.Map<Warranty>(createWarrantyRequest));

            await _unitOfWork.CompleteAsync();

            var newWarranty = _mapper.Map<CreateWarrantyRequest>(warranty);

            return newWarranty;
        }

        public Task<byte[]> GenerateReturnPolicyPdf()
        {
            // Create a new document
            MigraDocCore.DocumentObjectModel.Document document = new();
            document.Info.Title = "Return policy";

            // Add a section to the document
            var section = document.AddSection();

            // Add a title
            var title = section.AddParagraph("RETURN POLICY");
            title.Format.Font.Size = 18;
            title.Format.Font.Bold = true;
            title.Format.SpaceAfter = "1cm";
            title.Format.Alignment = ParagraphAlignment.Center;
            title.Format.Font.Color = Colors.Blue;
            //title.Format.Shading.Color = Colors.Blue;

            // Add introduction text
            //    var intro = section.AddParagraph("In order to bring better after-sales services to customer shopping at Jewelry store, we has a Return policy as follows:");
            //    intro.Format.Font.Size = 12;
            //    intro.Format.SpaceAfter = "1cm";

            //    // Add a subsection
            //    var subsectionTitle = section.AddParagraph("Article 1: Exchange Policy");
            //    subsectionTitle.Format.Font.Size = 14;
            //    subsectionTitle.Format.Font.Bold = true;
            //    subsectionTitle.Format.SpaceAfter = "0.75cm";
            //    subsectionTitle.Format.Shading.Color = Colors.Black;
            //    subsectionTitle.Format.Font.Color = Colors.Yellow;

            //    var subSectionSmailTitle = section.AddParagraph("Applicable for one exchange per order");
            //    subSectionSmailTitle.Format.Font.Size = 12;
            //    subSectionSmailTitle.Format.Font.Bold = true;
            //    subSectionSmailTitle.Format.LeftIndent = "0.5cm";
            //    subSectionSmailTitle.Format.SpaceAfter = "0.5cm";
            //    // Add bullet points
            //    var bulletPoints = new List<string>
            //{
            //    "Products can be returned within 2 days from the date of purchase on the invoice",
            //    "The product still has the original stamp and sales invoice. The product is not damaged by factors outside the store after purchase."
            //};

            //    foreach (var point in bulletPoints)
            //    {
            //        var p = section.AddParagraph("• " + point);
            //        p.Format.Font.Size = 12;
            //        p.Format.LeftIndent = "1cm";
            //        p.Format.SpaceAfter = "0.2cm";
            //    }

            //    var subSectionSmailTitle2 = section.AddParagraph("Can be exchanged for order with any product");
            //    subSectionSmailTitle2.Format.SpaceBefore = "0.3cm";
            //    subSectionSmailTitle2.Format.Font.Size = 12;
            //    subSectionSmailTitle2.Format.Font.Bold = true;
            //    subSectionSmailTitle2.Format.LeftIndent = "0.5cm";
            //    subSectionSmailTitle2.Format.SpaceAfter = "0.5cm";
            //    // Add bullet points
            //    var bulletPoints1_2 = new List<string>
            //{
            //    "If the order has a LOWER value than the previous order, we will not refund the difference.",
            //    "If the order has a HIGHER value than the previous order, please pay the difference in the order."
            //};

            //    foreach (var point in bulletPoints1_2)
            //    {
            //        var p = section.AddParagraph("• " + point);
            //        p.Format.Font.Size = 12;
            //        p.Format.LeftIndent = "1cm";
            //        p.Format.SpaceAfter = "0.2cm";
            //    }


            //    var subsectionTitle2 = section.AddParagraph("Article 2: Exchange policy due to technical errors and refund");
            //    subsectionTitle2.Format.SpaceBefore = "0.8cm";
            //    subsectionTitle2.Format.Font.Size = 14;
            //    subsectionTitle2.Format.Font.Bold = true;
            //    subsectionTitle2.Format.SpaceAfter = "0.75cm";
            //    subsectionTitle2.Format.Shading.Color = Colors.Black;
            //    subsectionTitle2.Format.Font.Color = Colors.Yellow;

            //    var subSectionSmailTitle2_1 = section.AddParagraph("Conditions apply");
            //    subSectionSmailTitle2_1.Format.Font.Size = 12;
            //    subSectionSmailTitle2_1.Format.Font.Bold = true;
            //    subSectionSmailTitle2_1.Format.LeftIndent = "0.5cm";
            //    subSectionSmailTitle2_1.Format.SpaceAfter = "0.5cm";
            //    // Add bullet points
            //    var bulletPoints2 = new List<string>
            //{
            //    "Technically defective products: Discolored, faded, distorted, not true to actual size...",
            //    "NOTE: The product will be replaced completely free of charge in case of technical errors within 2 days from the date of purchase on the sales invoice. After 2 days, warranty policy support will be provided."
            //};

            //    foreach (var point in bulletPoints2)
            //    {
            //        var p = section.AddParagraph("• " + point);
            //        p.Format.Font.Size = 12;
            //        p.Format.LeftIndent = "1cm";
            //        p.Format.SpaceAfter = "0.2cm";
            //    }

            //    var subSectionSmailTitle2_2 = section.AddParagraph("Case not resolved");
            //    subSectionSmailTitle2_2.Format.SpaceBefore = "0.3cm";
            //    subSectionSmailTitle2_2.Format.Font.Size = 12;
            //    subSectionSmailTitle2_2.Format.Font.Bold = true;
            //    subSectionSmailTitle2_2.Format.LeftIndent = "0.5cm";
            //    subSectionSmailTitle2_2.Format.SpaceAfter = "0.5cm";
            //    // Add bullet points
            //    var bulletPoints2_2 = new List<string>
            //{
            //    "Used product",
            //};

            //    foreach (var point in bulletPoints2_2)
            //    {
            //        var p = section.AddParagraph(point);
            //        p.Format.Font.Size = 12;
            //        p.Format.LeftIndent = "1cm";
            //        p.Format.SpaceAfter = "0.2cm";
            //    }

            var table = section.AddTable();
            table.Borders.Width = 0.75;

            // Define columns
            var column1 = table.AddColumn(MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(4));
            var column2 = table.AddColumn(MigraDocCore.DocumentObjectModel.Unit.FromCentimeter(12.2));

            // Add header row
            var row = table.AddRow();
            row.Height = 23;
            row.Shading.Color = Colors.LightGray;
            var cell = row.Cells[0];
            cell.AddParagraph("Exchange 48H");
            cell.MergeRight = 1;
            cell.Format.Font.Bold = true;
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = MigraDocCore.DocumentObjectModel.Tables.VerticalAlignment.Center;

            // Add condition rows
            row = table.AddRow();
            row.Height = 20;
            row.Cells[0].AddParagraph("I/ Time and conditions for 48H exchange:");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].MergeRight = 1;
            row.Cells[0].VerticalAlignment = MigraDocCore.DocumentObjectModel.Tables.VerticalAlignment.Center;

            row = table.AddRow();
            row.Shading.Color = Colors.LightSkyBlue;
            row.Height = 17;
            row.Cells[0].AddParagraph("Condition").Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[1].AddParagraph("Applicable to products of store").Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].Format.Font.Bold = true;
            row.Cells[0].VerticalAlignment = MigraDocCore.DocumentObjectModel.Tables.VerticalAlignment.Center;

            row = table.AddRow();
            row.Cells[0].AddParagraph("Time").Format.Alignment = ParagraphAlignment.Center;
            var paragraph = row.Cells[1].AddParagraph();
            var formattedText = paragraph.AddFormattedText("Purchase at the store: ", TextFormat.Bold);
            paragraph.AddText("calculated from the time the store issues an invoice to the customer.");
            row.Cells[0].VerticalAlignment = MigraDocCore.DocumentObjectModel.Tables.VerticalAlignment.Center;

            row = table.AddRow();
            row.Height = 20;
            row.Shading.Color = Colors.LightSkyBlue;
            row.Cells[0].AddParagraph("II/ Exchangeable product lines:");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].MergeRight = 1;
            row.Cells[0].VerticalAlignment = MigraDocCore.DocumentObjectModel.Tables.VerticalAlignment.Center;

            // Add product rows with merged cells
            row = table.AddRow();
            row.Height = 110;
            cell = row.Cells[0];
            cell.AddParagraph("Gold").Format.Alignment = ParagraphAlignment.Center;
            //cell.MergeDown = 1;
            cell.Format.Font.Bold = true;
            cell.VerticalAlignment = MigraDocCore.DocumentObjectModel.Tables.VerticalAlignment.Center;

            //row.Cells[1].AddParagraph("Thu đổi 48H");

            //row = table.AddRow();
            var paragraph2 = row.Cells[1].AddParagraph();
            var formattedText2 = paragraph2.AddFormattedText("New item with HIGHER value or equal to old item: ", TextFormat.Bold);
            paragraph2.AddText("We will refund 100% of the original invoice value.\n\n");
            paragraph2.AddFormattedText("New item with LOWER value than the old item: ", TextFormat.Bold);
            paragraph2.AddText("We will refund 90% of the original invoice value.\n\nNote: Exchanges are not applicable at some stores in shopping malls that share the same payment gateway with the center.");
            row.Cells[1].VerticalAlignment = MigraDocCore.DocumentObjectModel.Tables.VerticalAlignment.Center;


            row = table.AddRow();
            row.Height = 130;
            row.Cells[0].AddParagraph("For jewelries which have production cost").Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].VerticalAlignment = MigraDocCore.DocumentObjectModel.Tables.VerticalAlignment.Center;

            //    var nonApplicableProducts2 = new List<string>
            //{
            //    "Jewelry exchanged for EQUAL or HIGHER price (calculated on total value of purchase invoice): return 100% of the value of wages and jewelry from the old invoice, collect the difference between the new invoice and the old invoice.\n\n",
            //    "Jewelry exchanged for a LOWER price, return with 70% of the value of the product cost (customer loses), the material weight is calculated according to the gold price sold at the time of exchange.",

            //};

            //    foreach (var product in nonApplicableProducts2)
            //    {
            //        row.Cells[1].Format.Font.Bold = false;
            //        row.Cells[1].AddParagraph("• " + product);
            //        //row.Cells[1].MergeRight = 1;
            //    }
            var paragraph3 = row.Cells[1].AddParagraph();
            var formattedText3 = paragraph3.AddFormattedText("Jewelry exchanged for EQUAL or HIGHER price (calculated on total value of purchase invoice): ", TextFormat.Bold);
            paragraph3.AddText("return 100% of the value of wages and jewelry from the old invoice, collect the difference between the new invoice and the old invoice.\n\n");
            paragraph3.AddFormattedText("Jewelry exchanged for a LOWER price: ", TextFormat.Bold);
            paragraph3.AddText("Jewelry exchanged for a LOWER price: return with 70% of the value of the product cost (customer loses), the material weight is calculated according to the gold price sold at the time of exchange.");
            row.Cells[1].VerticalAlignment = MigraDocCore.DocumentObjectModel.Tables.VerticalAlignment.Center;
            


            row = table.AddRow();
            row.Height = 20;
            row.Shading.Color = Colors.LightSkyBlue;
            row.Cells[0].AddParagraph("III/ Products that are not eligible for 48H exchange:");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].MergeRight = 1;
            row.Cells[0].VerticalAlignment = MigraDocCore.DocumentObjectModel.Tables.VerticalAlignment.Center;

            row = table.AddRow();
            row.Height = 70;
            var nonApplicableProducts = new List<string>
        {
            "Loose Gems such as diamonds, colored stones, pearls.\n\n",
            "The product has been loosened, stones added, shape changed, plated, etc"
        };

            foreach (var product in nonApplicableProducts)
            {
                row.Cells[0].Format.Font.Bold = false;
                row.Cells[0].AddParagraph("• " + product).Format.LeftIndent = "0.45cm";
                row.Cells[0].MergeRight = 1;
            }
            row.Cells[0].VerticalAlignment = MigraDocCore.DocumentObjectModel.Tables.VerticalAlignment.Center;

            row = table.AddRow();
            row.Height = 20;
            row.Shading.Color = Colors.LightSkyBlue;
            row.Cells[0].AddParagraph("General Note: ").Format.Font.Bold = true;
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].MergeRight = 1;
            row.Cells[0].VerticalAlignment = MigraDocCore.DocumentObjectModel.Tables.VerticalAlignment.Center;

            


            row = table.AddRow();
            row.Height = 180;
            var nonApplicableProducts3 = new List<string>
        {
            "Gold is exchanged at the current purchase price of gold.\n\n",
            "Please keep the product intact, unused, with complete packaging and related documents, and bring sale invoice\n\n",
            "Only applicable for 48H product exchange at the store system (not applicable for online exchange)\n\n",
            "For invoices issued in the name of the business: when there is a 48H return, the business/company customer does not have to issue the invoice back to store. The store shall issue a replacement invoice and at the same time prepare a Return Record signed and stamped by the two Companies.\n\n"
        };

            foreach (var product in nonApplicableProducts3)
            {
                row.Cells[0].Format.Font.Bold = false;
                row.Cells[0].AddParagraph("• " + product).Format.LeftIndent = "0.45cm";
                row.Cells[0].MergeRight = 1;
            }
            row.Cells[0].VerticalAlignment = MigraDocCore.DocumentObjectModel.Tables.VerticalAlignment.Center;


            MigraDocCore.Rendering.PdfDocumentRenderer docRend = new MigraDocCore.Rendering.PdfDocumentRenderer(true);
            docRend.Document = document;


            docRend.RenderDocument();


            using (MemoryStream ms = new MemoryStream())
            {

                //pdf.Save(ms);
                docRend.PdfDocument.Save(ms);
                return Task.FromResult(ms.ToArray());
            }
        }
        
        }
}
