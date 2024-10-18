using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDMD_Project.Entities;

namespace CDMD_Project.Repo
{
    public interface IPatientRepo
    {
        Patient GetPatientByPhonenumber(string PhoneNumber);
        void Create(Patient newPatient);
        ObservableCollection<Patient> ReadAll();
        void Update(Patient patient);
        void Delete(Patient patient);
    }
}
