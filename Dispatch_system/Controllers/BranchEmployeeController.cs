using Dispatch_system.Data;
using Dispatch_system.Services;
using Dispatch_system.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dispatch_system.Controllers
{
    public class BranchEmployeeController : Controller
    {
        // to mi będzie potrzebne później dla ograniczenia dostępu dla zalogowanych
        // użytkowników z określoną rolą
        private readonly IClientParcelService parcelService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;

        public BranchEmployeeController(IClientParcelService parcelService,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            this.parcelService = parcelService;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> NotSentParcels()
        {
            var user = await userManager.GetUserAsync(User);

            var branchIdModel = (from person in context.People
                                 join employee in context.Employees on person.EmployeeId equals employee.EmployeeId
                                 where person.UserId == user.Id
                                 select new
                                 {
                                     employee.BranchId
                                 });

            int branchId = branchIdModel.First().BranchId;

            var notSentParcels = parcelService.NotSentParcels(branchId);

            return View(notSentParcels);
        }

        [HttpGet]
        public IActionResult CompleteData(int id)
        {
            var parcel = parcelService.GetParcel(id);
            return View(parcel);
        }

        [HttpPost]
        public IActionResult CompleteData(ClientParcelViewModel parcelModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ParcelSummary", "BranchEmployee", parcelModel);
            }

            return View(parcelModel);
        }

        [HttpGet]
        public IActionResult ParcelSummary(ClientParcelViewModel parcelModel)
        {
            return View(parcelModel);
        }
    }
}
