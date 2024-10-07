using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
