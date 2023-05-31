using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace DiplomaSite3.Controllers
{
    public class DiplomaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string name, int nTimes=1)
        {
            ViewData["Message"] = "Welcome " + name;
            ViewData["numtimes"] = nTimes;
            return View();
        }
    }
}
