using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineApp.Models
{
    public class Patient
    {
        //setters and getters
        public int Id { get; set; }
        public string Name { get; set; }
        public string Vaccineid { get; set; }
        public string FirstDose { get; set; }
        public string SecondDose { get; set; }

        public Patient() { }

        //create another class with id
        public Patient(string name, string vaccine, string firstDose, string secondDose)
        {
            Name = name;
            Vaccineid = vaccine;
            FirstDose = firstDose;
            SecondDose = secondDose;
        }
    }
}
