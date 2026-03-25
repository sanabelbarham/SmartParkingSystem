using BLL.Service.QR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace SmartParkingSystem.Area.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRController : ControllerBase
    {
     

        [HttpGet("Generate")]
        public IActionResult GenerateQR()
        {
            //this is my own ip address to let my phone read the QR
            string swaggerUrl = "http://192.168.56.1:5192/swagger/index.html";
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(swaggerUrl, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(data);

            byte[] qrImage = qrCode.GetGraphic(20);

                return File(qrImage, "image/png");
            
        
    }



    }
}
