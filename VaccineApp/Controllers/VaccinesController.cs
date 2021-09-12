using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccineApp.Models;
using VaccineApp.Services;

namespace VaccineApp.Controllers
{
    public class VaccinesController : Controller
    {
        public readonly IVaccineService _vaccineService;

        public VaccinesController(IVaccineService vaccineService)
        {
            _vaccineService = vaccineService;
        }
        public IActionResult Index()
        {
            return View(_vaccineService.GetVaccines());
        }

        //Display the Add form
        [HttpGet]
        public IActionResult Edit(int id)
        {

            //make dropdown list with DoseRequired values (text, value)
            ViewBag.DoseList = _vaccineService.GetVaccines()
                .Select(e => new SelectListItem(e.DoseRequired.ToString(), e.DoseRequired.ToString()))
                .ToList();

            return View(_vaccineService.GetVaccine(id));
        }

        [HttpPost]
        public IActionResult Edit(int id, Vaccine change)
        {
            //search by id
            var vaccineFound = _vaccineService.GetVaccine(id);
            //update
            vaccineFound.VaccineName = change.VaccineName;
            vaccineFound.DoseRequired = change.DoseRequired;
            vaccineFound.DaysBtwDose = change.DaysBtwDose;
            vaccineFound.TotalDose = change.TotalDose;
            vaccineFound.DoseLeft = change.DoseLeft;
            //save in backend
            _vaccineService.SaveChanges();

            return RedirectToAction("Index");
        }

        //Display the Add form
        [HttpGet]
        public IActionResult AddVaccine()
        {
            return View();
        }

        //process adding form
        [HttpPost]
        public IActionResult AddVaccine(Vaccine vaccine)
        {
            _vaccineService.AddVaccine(vaccine);
            return RedirectToAction("Index");
        }

        //Display the Add dose form
        [HttpGet]
        public IActionResult AddDose()
        {
            //make a dropdown list
            ViewBag.NameList = _vaccineService.GetVaccines()
                .Select(e => new SelectListItem(e.VaccineName, e.Id.ToString()))
                .ToList();
            return View();
        }

        //process adding dose form
        [HttpPost]
        public IActionResult AddDose(Vaccine vaccine)
        {
            //look based on id value in viewBag dropdown
            var vaccineFound = _vaccineService.GetVaccine(vaccine.Id);
            vaccineFound.TotalDose += vaccine.TotalDose;
            //add to left
            vaccineFound.DoseLeft += vaccine.TotalDose;

            //save changes to database
            _vaccineService.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
