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

namespace PierresSweets.Collections
{
  [Authorize]
  public class FlavorController : Controller
  {
    private readonly PierresSweetsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorController(UserManager<ApplicationUser> userManager, PierresSweetsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Flavor> model = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Flavor flavor, int treatId)
    {
      string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      flavor.User = currentUser;

      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      if (treatId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = flavor.FlavorId, TreatId = treatId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View (thisFlavor);
    }

    public ActionResult Details(int id)
    {
      Flavor thisFlavor = _db.Flavors
        .Include(flavor => flavor.JoinEntities)
          .ThenInclude(join => join.Treat)
        .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTreat(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int treatId)
    {
      bool duplicate = _db.FlavorTreats.Any(join => join.TreatId == treatId && join.FlavorId == flavor.FlavorId);

      if (treatId != 0 && !duplicate)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { TreatId = treatId, FlavorId = flavor.FlavorId });
      }

      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}