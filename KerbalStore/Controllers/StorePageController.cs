using KerbalStore.Data;
using KerbalStore.Models;
using KerbalStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace KerbalStore.Controllers
{
    public class StorePageController : Controller
    {
        private readonly ITicketService ticketService;
        private readonly IKerbalStoreRepository kerbalStoreRepository;

        public StorePageController(ITicketService ticketService, IKerbalStoreRepository kerbalStoreRepository)
        {
            this.ticketService = ticketService;
            this.kerbalStoreRepository = kerbalStoreRepository;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("AboutUs")]
        public IActionResult AboutUs()
        {
            ViewBag.Title = "About the store";
            return View();
        }

        [HttpGet("TestErrorPage")]
        public IActionResult TestErrorPageUsingRazorPage()
        {
            throw new InvalidOperationException("Oops!");
        }

        [HttpGet]
        public IActionResult RequestPart()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RequestPart(RequestPartModel model)
        {
            if (ModelState.IsValid)
            {
                ticketService.SendTicket(model.Name, model.RocketPart);
                ViewBag.UserFeedback = "Request received, please wait for it to be reviewed by KSP staff";
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult Shop()
        {
            //var results = kerbalStoreRepository.RocketParts.OrderBy(rp => rp.PartName).ToList();
            //var linqResults = from rp in kerbalStoreRepository.RocketParts
            //                  orderby rp.PartName ascending
            //                  select rp;
            var fromRepository = kerbalStoreRepository.GetAllRocketParts();

            return View(fromRepository);
        }
    }
}