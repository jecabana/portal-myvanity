using System.Web.Mvc;

namespace MyVanity.Web.Controllers
{
    public class CommonController : Controller
    {
        public PartialViewResult GetPartialViewForAddingFile()
        {
            return PartialView("_FileEditModelPartial");
        }
    }
}