using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class InvoiceController : ControllerBase
    {
        private readonly IServiceRecordRepository _serviceRecord;
        private readonly ICustomerRepository _customerService;
        private readonly IVehicleRepository _vehicleService;
        private readonly IServiceRecordItemRepository _serviceRecordItem;
        private readonly IMaterialRepository _materialService;
        private readonly IServiceRepresentativeRepository _serviceRepresentative;

        public InvoiceController
            (
                IServiceRecordRepository serviceRecord,
                ICustomerRepository customerService,
                IVehicleRepository vehicleService,
                IServiceRecordItemRepository serviceRecordItem,
                IMaterialRepository materialService,
                IServiceRepresentativeRepository serviceRepresentative
            )
        {
            _serviceRecord = serviceRecord;
            _customerService = customerService;
            _vehicleService = vehicleService;
            _serviceRecordItem = serviceRecordItem;
            _materialService = materialService;
            _serviceRepresentative = serviceRepresentative;
        }

        [HttpGet("{vehicleId}")]
        public async Task<ActionResult> GeneratePdf(int vehicleId)
        {
            try
            {
                // Service Record
               var serviceRecord = await _serviceRecord.GetServiceRecordByVehicleIdAsync(vehicleId);

                // Get Customer Details by id

                var customer = await _customerService.GetCustomerAsync(serviceRecord.CustomerId ?? 0);

                // Get Vehicle Details By id

                var vehicle = await _vehicleService.GetVehicleAsync(vehicleId);

                // Get Vehicle Service Record by Id

                var serviceRecordItems = await _serviceRecordItem.GetAllServiceRecordItemByServiceRecordAsync(serviceRecord.ServiceRecordID);


                // Get Service Representative by id

                var serviceRepresentative = await _serviceRepresentative.GetServiceRepresentativeAsync(serviceRecord.RepresentativeID);




                var document = new PdfDocument();
                string htmlcontent = $"<small> Invoice No: INV_{serviceRecord.ServiceRecordID} | Service Date : {serviceRecord.ServiceDate} </small>";
                 htmlcontent += "<div style='width:100%; text-align:center'>";
                

                htmlcontent += "<h1>********** Prime Automobile PVT. LTD. **********</h1>";

                //htmlcontent += $"<h2> Invoice No. INV_{serviceRecord.ServiceRecordID} & Invoice Date : {serviceRecord.ServiceDate} </h2>";

                htmlcontent += "<div style='border:1px solid black;padding:5px;text-align:center;margin-bottom:10px;'>";

                htmlcontent += $"<h3> Customer : {customer.FirstName + " "+ customer.LastName} </h3>";
                htmlcontent += $"<h3> Service Advisor : {serviceRepresentative.FirstName + " " + serviceRepresentative.LastName} </h3>";
                htmlcontent += $"<h3> Vehicle Number : {vehicle.VehicleNumber} | Vehicle Brand : {vehicle.VehicleBrand}</h3>";
                htmlcontent += $"<p> Address : Prime Automobile PVT. LTD.  </p>";
                htmlcontent += $"<p> Pune - 411041 </p>";
                htmlcontent += $"<h3> Contact : {customer.Mobile} & Email : {customer.Email} </h3>";

                htmlcontent += "</div>";

                htmlcontent += "<div>";

                ///// -------------- HEAD ------------------




                htmlcontent += "<table style ='width:100%; border: 1px solid #000'>";
                htmlcontent += "<thead style='font-weight:bold'>";
                htmlcontent += "<tr>";
                htmlcontent += "<th style='border:1px solid #000'> Material Code </th>";
                htmlcontent += "<th style='border:1px solid #000'> Material Name </th>";
                htmlcontent += "<th style='border:1px solid #000'>Quantity</th>";
                htmlcontent += "<th style='border:1px solid #000'>Price</th >";
                htmlcontent += "<th style='border:1px solid #000'>Total</th>";
                htmlcontent += "</tr>";
                htmlcontent += "</thead >";


                //// ---------- BODY ----------

                htmlcontent += "<tbody>";

                double totalCount = 0;

                foreach (var item in serviceRecordItems)
                {

                    var material = await _materialService.GetMaterialAsync(item.ItemID);
                    totalCount += item.Total;

                    htmlcontent += "<tr>";
                    htmlcontent += $"<td> {material.ItemID} </td>";
                    htmlcontent += $"<td> {material.ItemName} </td>";
                    htmlcontent += $"<td>{item.Quantity}</td>";
                    htmlcontent += $"<td>{item.Price}</td >";
                    htmlcontent += $"<td>{item.Total}</td>";
                    htmlcontent += "</tr>";
                }


                htmlcontent += "</tbody>";

                htmlcontent += "</table>";

                htmlcontent += $"<h2 style ='border: 1px solid black;padding:3px;color:green;'>  Summary Total = {totalCount}/- </h2>";

                htmlcontent += "</div>";
                htmlcontent += "</div>";


                PdfGenerator.AddPdfPages(document, htmlcontent, PageSize.A4);
                byte[]? response = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    document.Save(ms);
                    response = ms.ToArray();
                }
                string Filename = "Invoice_" + vehicleId + ".pdf";
                return File(response, "application/pdf", Filename);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal Server Error");
            }
        }

       


    }
}
