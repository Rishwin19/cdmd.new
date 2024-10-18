using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CDMD_Project.ViewModel;

namespace CDMD_Project.Pages
{
    /// <summary>
    /// Interaction logic for PatientProfilePage.xaml
    /// </summary>
    public partial class PatientProfilePage : Window
    {
        public PatientProfilePage()
        {
            InitializeComponent();
            var ViewModel = new PatientViewModel();
            this.DataContext = ViewModel;
        }

        

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
