﻿using AsyncApiCaller.ApiResponse;
using AsyncApiCaller.Models;
using AsyncApiCaller.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncApiCaller.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var apiData = new ApiData();

            ViewData["RandomNumber1"] = await apiData.GetRandomNumber();
            ViewData["RandomNumber2"] = apiData.GetRandomNumberWithoutAsyncAwait();
            ViewData["ChuckNorrisFact"] = await apiData.GetChuckNorrisFact();
            var data = await apiData.GetTheSeleucids();

            var vm = new IndexViewModel
            {
                RandomNumber = await apiData.GetRandomNumber(),
                ChuckNorrisFact = await apiData.GetChuckNorrisFact(),
                Seleucids = await apiData.GetTheSeleucids()
            };

            return View(vm);
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
