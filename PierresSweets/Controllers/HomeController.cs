using Microsoft.AspNetCore.Mvc;

namespace PierresSweets.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}