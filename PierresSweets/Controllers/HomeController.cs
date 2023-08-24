using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PierresSweets.Models;
using System.Collections.Generic;
using System.Linq;

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
    public ActionResult Index()
    {
      List<Treat> treatList = _db.Treats.ToList();
      ViewBag.TreatList = treatList;
      List<Flavor> flavorList = _db.Flavors.ToList();
      return View(flavorList);
    }
  }
}