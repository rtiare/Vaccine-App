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
    public class PatientsController : Controller
    {
        public readonly IPatientService _patientService;
        private readonly IVaccineService _vaccineService;

        public PatientsController(IPatientService patientService, IVaccineService vaccineService)
        {
            _patientService = patientService;
            _vaccineService = vaccineService;
        }

        public IActionResult Index()
        {
            return View(_patientService.GetPatients());
        }

        //Display the Add form
        [HttpGet]
        public IActionResult AddPatient()
        {
            //make dropdown list with DoseRequired values (text, value) for doseLeft > 0
            ViewBag.VaccineList = _vaccineService.GetVaccines()
                .Where(e => e.DoseLeft > 0)
                .Select(e => new SelectListItem(e.VaccineName, e.VaccineName))
                .ToList();
            return View();
        }

        //process adding form
        [HttpPost]
        public IActionResult AddPatient(Patient patient)
        {

            //get the vaccine with patient vaccine name
            Vaccine selected = _vaccineService.SearchByName(patient.Vaccineid);

            //decrease it and save changes
            int stock = selected.DoseLeft - 1;
            selected.DoseLeft = stock;
            _vaccineService.SaveChanges();

            //Set the date of the first dose for the patient to the current date.
            DateTime currentTIme = DateTime.Now;
            patient.FirstDose = currentTIme.ToString("MM/dd/yyyy");

            if (selected.DoseRequired == 1)
            {
                patient.SecondDose = "-";
            }
            else
            {
                if (selected.DoseLeft == 0)
                {
                    patient.SecondDose = "Out of Stock";
                }
                else
                {
                    patient.SecondDose = "Received";
                }
            }

            //save the changes to database
            _patientService.AddPatient(patient);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddSecondDose(int id)
        {

            return View(_patientService.GetPatient(id));
        }

        [HttpPost]
        public IActionResult AddSecondDose(Patient temp)
        {
            var patient = _patientService.GetPatient(temp.Id);
            //Set the date of the second dose for the patient to the current date.
            string curent = DateTime.Now.ToString("MM/dd/yyyy");
            patient.SecondDose = curent;

            //derement stock by 1
            Vaccine vaccine = _vaccineService.SearchByName(patient.Vaccineid);
            int stock = vaccine.DoseLeft - 1;
            vaccine.DoseLeft = stock;

            //save the changes to database
            _patientService.SaveChanges();
            _vaccineService.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
