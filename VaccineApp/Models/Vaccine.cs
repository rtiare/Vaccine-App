using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineApp.Models
{
    public class Vaccine
    {
        //setters and getters
        public int Id { get; set; }
        public string VaccineName { get; set; }
        public int DoseRequired { get; set; }
        public int? DaysBtwDose { get; set; }
        public int TotalDose { get; set; } = default;
        public int DoseLeft { get; set; } = default;

        public Vaccine() { }

        public Vaccine(string name, int dose, int? days, int total, int left)
        {
            VaccineName = name;
            DoseRequired = dose;
            DaysBtwDose = days;
            TotalDose = total;
            DoseLeft = left;
        }

        public Vaccine(int id, string name, int dose, int? days, int total, int left)
        {
            Id = id;
            VaccineName = name;
            DoseRequired = dose;
            DaysBtwDose = days;
            TotalDose = total;
            DoseLeft = left;
        }
    }
}
