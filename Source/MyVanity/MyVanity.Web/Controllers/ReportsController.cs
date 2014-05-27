using System.Web.Mvc;

namespace MyVanity.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        public ActionResult Conversations()
        {

            return View();
        }
    }
}