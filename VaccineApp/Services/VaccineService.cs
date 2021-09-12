using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccineApp.Models;

namespace VaccineApp.Services
{
    public interface IVaccineService
    {
        List<Vaccine> GetVaccines();
        Vaccine GetVaccine(int id);
        Vaccine SearchByName(string name);
        void AddVaccine(Vaccine vaccine);
        void SaveChanges();
    }

    public class VaccineService: IVaccineService
    {
        private readonly AppDbContext _db;
        public VaccineService(AppDbContext db)
        {
            _db = db;
        }

        public void AddVaccine(Vaccine vaccine)
        {
            _db.Vaccines.Add(vaccine);
            _db.SaveChanges();
        }

        public Vaccine GetVaccine(int id)
        {
            return _db.Vaccines.Where(e => e.Id == id).SingleOrDefault();
        }

        public List<Vaccine> GetVaccines()
        {
            return _db.Vaccines.ToList();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public Vaccine SearchByName(string name)
        {
            return _db.Vaccines.Where(e => e.VaccineName == name).Single();
        }
    }
    public class MockVaccineService : IVaccineService
    {
        private List<Vaccine> vaccines;

        public MockVaccineService()
        {
            vaccines = new List<Vaccine> {
                new Vaccine(1, "Pfizer/BioNTech", 2, 21,  10000,   10000)};
        }

        public void AddVaccine(Vaccine vaccine)
        {
            vaccines.Add(vaccine);
        }

        public Vaccine GetVaccine(int id)
        {
            return vaccines.FirstOrDefault(e => e.Id == id);
        }

        public List<Vaccine> GetVaccines()
        {
            return vaccines;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Vaccine SearchByName(string name)
        {
            return vaccines.Single(e => e.VaccineName == name); ;
        }
    }
}
