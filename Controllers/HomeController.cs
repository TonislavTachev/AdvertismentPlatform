﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdvertismentPlatform.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdvertismentPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdvertismentRepository advertismentRepository;
        private readonly IitemRepository iitemRepository;

        public HomeController(ILogger<HomeController> logger, 
            IAdvertismentRepository advertismentRepository,
            IitemRepository iitemRepository)
        {
            _logger = logger;
            this.advertismentRepository = advertismentRepository;
            this.iitemRepository = iitemRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //  var it = await itemRepository.GetItem(1);
            var ad = await advertismentRepository.GetAdvertisment(1);

            var advertise = new Advertisment()
            {
                Title = "NewAD",
                Picture = "NewPic",
                PostDate = DateTime.UtcNow,
                Item = new AutoItem()
                {
                    Brand = "BewBrandd",
                    Description = "NEW description",
                    Mileage = 99,
                    ProductAge = DateTime.UtcNow,
                    Price = 1000
                }
            };
            await advertismentRepository.Add(advertise);

            return View(ad);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}