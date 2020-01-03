using Dispatch_system.Data;
using Dispatch_system.Services;
using Dispatch_system.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Controllers
{
    public class ClientParcelController : Controller
    {
        private readonly IClientParcelService clientParcelService;
        // to mi będzie potrzebne później dla ograniczenia dostępu dla zalogowanych
        // użytkowników z określoną rolą
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;

        public ClientParcelController(IClientParcelService clientParcelService,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            this.clientParcelService = clientParcelService;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet]
        public IActionResult PostParcel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostParcel(ClientParcelViewModel clientParcelViewModel)
        {
            if (ModelState.IsValid)
            {
                clientParcelService.PostOnline(clientParcelViewModel);
                int senderBranchCode = int.Parse(new string(clientParcelViewModel.SenderPostalCode.Take(2).ToArray()));

                var queryModel = (from branches in context.Branches
                                  join parcels in context.Parcels on branches.BranchId equals parcels.LastBranchId
                                  where branches.BranchCode == senderBranchCode
                                  select new ParcelSummaryViewModel
                                  {
                                      ParcelId = parcels.ParcelId,
                                      BranchName = branches.BranchName,
                                      BranchAddress = branches.BranchAddress,
                                      BranchCity = branches.City
                                  }
                    );

                ParcelSummaryViewModel model = new ParcelSummaryViewModel
                {
                    ParcelId = queryModel.First().ParcelId,
                    BranchName = queryModel.First().BranchName,
                    BranchAddress = queryModel.First().BranchAddress,
                    BranchCity = queryModel.First().BranchCity
                };

                return RedirectToAction("ThanksPage", model);
            }

            return View();
        }

        [HttpGet]
        public IActionResult ThanksPage(ParcelSummaryViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult CheckStatus(string parcelId)
        {
            var model = clientParcelService.CheckStatus(int.Parse(parcelId));

            if (model != null) return View(model);
            return RedirectToAction("ParcelNotFound", "Home");
        }

        [HttpGet]
        public IActionResult EnterCode()
        {
            return View();
        }
    }
}