using BLL.Service.QR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartParkingSystem.Area.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QRController : ControllerBase
    {
        [HttpGet("Generate")]
        public IActionResult GenerateQR()
        {
            //this is my own ip address to let my phone read the QR
            string swaggerUrl = "http://192.168.56.1:5192/swagger/index.html";

            var qrService = new QRCode();
            byte[] qrImage = qrService.GenerateQR(swaggerUrl);

            return File(qrImage, "image/png");
        }
    }
}
