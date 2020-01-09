using Dispatch_system.Data;
using Dispatch_system.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Controllers
{
    public class WarehousemanController : Controller
    {
        private readonly IWarehouseService warehouseService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public WarehousemanController(IWarehouseService warehouseService,
            ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager)
        {
            this.warehouseService = warehouseService;
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult AcceptParcels(int branchId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AcceptSentParcels(int branchId)
        {
            AcceptSentParcels(branchId);
            return RedirectToAction("ParcelsAccepted"); // zaimplementuj widok
        }
    }
}
