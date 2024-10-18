using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CDMD_Project.Command;
using CDMD_Project.EFRepo;
using CDMD_Project.Entities;
using CDMD_Project.Repo;

namespace CDMD_Project.ViewModel
{
    public delegate void DWindwoClose();
    public class PatientViewModel : ViewModelBase
    {
        private ObservableCollection<Patient> _patients;
        private Patient _newPatient;
        private Patient _selectedPatient;

        public DWindwoClose NewWindowClose;

        public ObservableCollection<Patient> Patients
        {
            get { return _patients; }
            set { _patients = value; OnPropertyChanged(nameof(Patients)); }
        }

        public Patient NewPatient
        {
            get { return _newPatient; }
            set { _newPatient = value; OnPropertyChanged(nameof(NewPatient)); }
        }

        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set { _selectedPatient = value; OnPropertyChanged(nameof(SelectedPatient)); }
        }
        private string _PatientPhonenumber;

        public string PatientPhonenumber
        {
            get { return _PatientPhonenumber; }
            set
            {
                _PatientPhonenumber = value;
                OnPropertyChanged(nameof(PatientPhonenumber));
            }
        }
        public Patient Patient { get { return CrudOperation == 1 ? NewPatient : SelectedPatient; } }
        string OperationText { get { return CrudOperation == 1 ? "Register" : "Update"; } }

        public bool Device1 { get; set; }
        public bool Device2 { get; set; }
        public bool Device3 { get; set; }
        public bool Device4 { get; set; }
        public int CrudOperation { get; set; } = 1;

        public ICommand CreateOrUpdateCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand NewCommand { get; }

        private IPatientRepo _repo = EFPatientRepo.Instance;
        private IWearableRepo _deviceRepo = EFWearableRepo.Instance;

        public PatientViewModel()
        {
            LoadPatients();
            CreateOrUpdateCommand = new RelayCommand(CreateOrUpdate);
            SearchCommand = new RelayCommand(Search);
            NewCommand = new RelayCommand(ToNewPatient);

            ToNewPatient();
        }

        public void LoadPatients()
        {
            Patients = _repo.ReadAll();
        }

        private void _Create()
        {
            try
            {
                //var existingPatient = _repo.GetPatientByPhonenumber(Patient.PhoneNumber);
                
               
                var newPatient = new Patient
                {
                    FullName = Patient.FullName,
                    Age = Patient.Age,
                    Gender = Patient.Gender,
                    PhoneNumber = Patient.PhoneNumber,
                    Address = Patient.Address,
                    Email = Patient.Email,
                    Condition = Patient.Condition,
                    AssignedDoctorID = Patient.AssignedDoctorID,
                    CreatedAt = DateTime.Now
                };

                var result = MessageBox.Show("Are you sure you want to create a new patient?", "Confirm", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _repo.Create(newPatient);
                    if (Device1)
                    {
                        var wDevice1 = new WearableDevice
                        {
                            DeviceSerialNumber = "",
                            PatientID = newPatient.PatientID,
                            CreatedAt = DateTime.Now,
                            DeviceType = "Blood Sugar"
                        };

                        _deviceRepo.Create(wDevice1);
                    }
                    if (Device2)
                    {
                        var wDevice2 = new WearableDevice
                        {
                            DeviceSerialNumber = "",
                            Patient = newPatient,
                            CreatedAt = DateTime.Now,
                            DeviceType = "Heart Rate"
                        };
                        _deviceRepo.Create(wDevice2);

                    }
                    if (Device3)
                    {
                        var wDevice3 = new WearableDevice
                        {
                            DeviceSerialNumber = "",
                            Patient = newPatient,
                            CreatedAt = DateTime.Now,
                            DeviceType = "Blood Pressure"
                        };
                        _deviceRepo.Create(wDevice3);

                    }
                    if (Device4)
                    {
                        var wDevice4 = new WearableDevice
                        {
                            DeviceSerialNumber = "",
                            Patient = newPatient,
                            CreatedAt = DateTime.Now,
                            DeviceType = "Activity"
                        };
                        _deviceRepo.Create(wDevice4);
                    }
                        MessageBox.Show("Patient created successfully.");
                    
                    }
                

                ToNewPatient();
                //LoadPatients();
                NewPatient = new Patient();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void _Update()
        {
            var existingPatient = _repo.GetPatientByPhonenumber(Patient.PhoneNumber);
            if (existingPatient != null)
            {
                existingPatient.FullName = Patient.FullName;
                existingPatient.Age = Patient.Age;
                existingPatient.Gender = Patient.Gender;
                existingPatient.Address = Patient.Address;
                existingPatient.Email = Patient.Email;
                existingPatient.Condition = Patient.Condition;
                existingPatient.AssignedDoctorID = Patient.AssignedDoctorID;

                var result = MessageBox.Show("Are you sure you want to update the patient?", "Confirm", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _repo.Update(existingPatient);
                    MessageBox.Show("Patient updated successfully.");
                    ToNewPatient();
                }
            }
        }
        public void CreateOrUpdate()
        {
            if (CrudOperation == 1)
            {
                _Create();
            }
            else
            {
                _Update();
            }
        }

        public void ToNewPatient()
        {
            CrudOperation = 1;
            NewPatient = new Patient
            {
                FullName = "",
                Age = 0,
                Gender = "Female",
                PhoneNumber = "",
                Address = "",
                Email = "",
                Condition = "",
                AssignedDoctorID = 1,
                CreatedAt = DateTime.Now,

            };
            Device1 = false;
            Device2 = false;
            Device3 = false;
            Device4 = false;
            OnPropertyChanged(nameof(Patient));
            OnPropertyChanged(nameof(OperationText));
        }
        public void Search()
        {
            var patient = _repo.GetPatientByPhonenumber(PatientPhonenumber);
            if (patient != null)
            {
                SelectedPatient = patient;
                CrudOperation = 2;
                Device1 = false;
                Device2 = false;
                Device3 = false;
                Device4 = false;
                foreach (var wdevice in patient.WearableDevices)
                {
                    if (wdevice.DeviceType.Equals("Blood Sugar"))
                    {
                        Device1 = true;
                    }
                    if (wdevice.DeviceType.Equals("Heart Rate"))
                    {
                        Device2 = true;
                    }
                    if (wdevice.DeviceType.Equals("Blood Pressure"))
                    {
                        Device3 = true;
                    }
                    if (wdevice.DeviceType.Equals("Activity"))
                    {
                        Device4 = true;
                    }

                }
                OnPropertyChanged(nameof(Device1));
                OnPropertyChanged(nameof(Device2));
                OnPropertyChanged(nameof(Device3));
                OnPropertyChanged(nameof(Device4));
                OnPropertyChanged(nameof(Patient));
                OnPropertyChanged(nameof(OperationText));


            }
            else
            {
                MessageBox.Show("Notfound");
                _Create();
            }

        }

        
       
    }
}
