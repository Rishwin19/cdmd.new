using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDMD_Project.Pages;
using CDMD_Project.ViewModel;

namespace CDMD_Project
{
    public static class FormConfig
    {
        public static int UserId { get; set; }
        public static LoginWindow loginWindow = null;
        public static DashboardWindow dashboardWindow = null;
        public static PatientProfilePage patientprofilePage = null;
        public static PatientViewModel patientviewmodel = null;



        static FormConfig()
        {
            loginWindow = new LoginWindow();
            dashboardWindow = new DashboardWindow();
            patientprofilePage = new PatientProfilePage();
            patientviewmodel = new PatientViewModel();
        }
    }
    
}
