using Microsoft.AspNetCore.Mvc;
using qrcodegenerator.Models;
using QRCoder;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace qrcodegenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Qrcode(string QRCodeText)
        {
            var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chs=500x500&chl={0}", QRCodeText);
            WebResponse response = default(WebResponse);
            Stream remoteStream = default(Stream);
            StreamReader readStream = default(StreamReader);
            WebRequest request = WebRequest.Create(url);
            response = request.GetResponse();
            remoteStream = response.GetResponseStream();
            readStream = new StreamReader(remoteStream);
            Image img = Image.FromStream(remoteStream);
            img.Save("C:\\Users\\jose1\\source\\repos\\qrcodegenerator\\qrcodegenerator\\wwwroot\\img\\" + "generatedqrcode.png");
            response.Close();
            remoteStream.Close();
            readStream.Close();
            ViewBag.Message = "QR Code Gerado:";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}