using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PierresSweets.Models;

namespace PierresSweets.Controllers
{
  public class HomeController : Controller
  {
    private readonly PierresSweetsContext _db;

    public HomeController(PierresSweetsContext db)
    {
      _db = db;
    }
    
    [Route("/")]
    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Treat> treatList = _db.Treats.ToList();
      ViewBag.TreatList = treatList;
      List<Flavor> flavorList = _db.Flavors.ToList();
      return View(flavorList);
    }
  }
}