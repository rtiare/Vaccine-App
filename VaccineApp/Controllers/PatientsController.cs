﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineApp.Controllers
{
    public class PatientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
