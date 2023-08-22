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
    
    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Treat> treat = _db.Treats.ToList();

      List<Flavor> flavor = _db.Flavors.ToList();
      return View();
    }
  }
}