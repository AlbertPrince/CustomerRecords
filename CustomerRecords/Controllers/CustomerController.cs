using Microsoft.AspNetCore.Mvc;

namespace CustomerRecords.Api.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
