using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccineApp.Models;

namespace VaccineApp.Services
{
    public interface IPatientService
    {
        List<Patient> GetPatients();
        Patient GetPatient(int id);
        void AddPatient(Patient patient);
        void SaveChanges();
    }
    public class PatientService : IPatientService
    {
        private readonly AppDbContext _db;

        //connect to database in sql
        public PatientService(AppDbContext db)
        {
            _db = db;
        }
        public void AddPatient(Patient patient)
        {
            _db.Patients.Add(patient);
            _db.SaveChanges();
        }

        public Patient GetPatient(int id)
        {
            return _db.Patients.Where(e => e.Id == id).SingleOrDefault();
        }

        public List<Patient> GetPatients()
        {
            return _db.Patients.ToList();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }

    public class MockPatientService : IPatientService
    {
        private List<Patient> patients;

        public MockPatientService()
        {
            patients = new List<Patient> {
                new Patient("John Doe", "Pfizer/BioNTech", "2/18/2015", "3/11/2015")};
        }
        public void AddPatient(Patient patient)
        {
            patients.Add(patient);
        }

        public Patient GetPatient(int id)
        {
            return patients.FirstOrDefault(e => e.Id == id);
        }

        public List<Patient> GetPatients()
        {
            return patients;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
