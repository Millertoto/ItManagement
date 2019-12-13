using ItManagement.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ItManagement.PersSingleton;
using ItManagement.Persistencies;
using ItManagement.View;

namespace ItManagement.ViewModel
{
    class EquipmentViewModel : INotifyPropertyChanged
    {
        #region Instance Field
        private Computer _computer;
        private SmartBoard _smartBoard;
        private Tablet _tablet;
        private SmartPhone _smartPhone;
        private List<Equipment> _listOfEquipment;
        private Equipment _selectedEquipment;
        private int _uid;
        private string _type;
        private bool _isWorking;
        private RelayCommand _getAllEquipment;
        private RelayCommand _createEquipment;
        private RelayCommand _getEquipmentOfType;
        private RelayCommand _deleteEquipment;
        private INotifyPropertyChanged _notifyPropertyChangedImplementation;
        private ObservableCollection<Equipment> _obsequipment;
        private RelayCommand _editEquipment;
        private List<Error> _listOfErrors;

        #endregion

        #region Constructor

        public EquipmentViewModel()
        {
            /*_getAllEquipment = new RelayCommand(GetAllEquipmentMethod);*/
            _createEquipment = new RelayCommand(AddEquipment);
            /*_getEquipmentOfType = new RelayCommand(GetEquipmentOfTypeMethod);*/
            _listOfEquipment = Singleton.Instance.EQP.GetEquipments().Result;
            _deleteEquipment = new RelayCommand(DeleteEquipMethod);
            _editEquipment = new RelayCommand(EditMethod);
            _obsequipment = new ObservableCollection<Equipment>();
            _goBack = new RelayCommand(GoBackMethod);
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            _equipmentTypes = new ObservableCollection<string>() { "Computer", "Tablet", "Smartboard", "Smartphone" }; 
=======
            _equipmentTypes = new ObservableCollection<string>() { "Computer", "Tablet", "Smartboard", "Smartphone" };
>>>>>>> parent of 02c1b0c... Merge branch 'master' into Caspar
=======
            _equipmentTypes = new ObservableCollection<string>() { "Computer", "Tablet", "Smartboard", "Smartphone" };
>>>>>>> parent of 02c1b0c... Merge branch 'master' into Caspar
=======
>>>>>>> parent of b3ac666... Merge pull request #44 from Millertoto/Fixmaybe
            ConvertToObs();
        }

        #endregion

        #region Properties


        public bool IsWorking
        {
            get { return _isWorking; }
            set { _isWorking = value; }
        }

        public Equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                _selectedEquipment = value; 
                OnPropertyChanged();
            }
        }
        public int Uid
        {

            get { return _uid; }
            set { _uid = value; }
        }

        public string TypeOfEquipment
        {
            get { return _type; }
            set
            {
                _type = value; 
                OnPropertyChanged();
            }
        }

        public async void DeleteMethod()
        {
            if (SelectedEquipment != null)
            {
                await Singleton.Instance.EQP.DeleteEquipment(SelectedEquipment.Uid);
            }
        }

        #region Lists
        public List<Equipment> AllEquipment
        {
            get { return _listOfEquipment; }
            set { _listOfEquipment = value; }
        }

        public ObservableCollection<Equipment> ObsEquipment
        {
            get { return _obsequipment; }
            set
            {
                _obsequipment = value;
                OnPropertyChanged();
            }
        }

        public List<Error> ListOfErrors
        {
            get { return _listOfErrors; }
            set { _listOfErrors = value; }
        }
        #endregion




        #region Computer Properties

        public Computer SComputer
        {
            get { return _computer; }
            set { _computer = value; }
        }

        #endregion

        #region Smartboard Properties

        public SmartBoard SSmartboard
        {
            get { return _smartBoard; }
            set { _smartBoard = value; }
        }

        #endregion

        #region Smartphone Properties

        public SmartPhone SSmartphone
        {
            get { return _smartPhone; }
            set { _smartPhone = value; }
        }

        #endregion


        #region Tablet Properties

        public Tablet STablet
        {
            get { return _tablet; }
            set { _tablet = value; }
        }

        #endregion

        #endregion

        #region RelayCommands

        public RelayCommand CreateEquipment
        {
            get { return _createEquipment; }
            set { _createEquipment = value; }
        }

        public RelayCommand DeleteEquipment
        {
            get { return _deleteEquipment; }
            set { _deleteEquipment = value; }
        }

        public RelayCommand EditEquipment
        {
            get { return _editEquipment; }
            set { _editEquipment = value; }
        }
        #endregion

        #region Methods

        public async void AddEquipment()
        {
            if (TypeOfEquipment == "Computer"
                || TypeOfEquipment == "Smartboard"
                || TypeOfEquipment == "Smartphone"
                || TypeOfEquipment == "Tablet")
            {
                Equipment e = new Equipment(TypeOfEquipment);
                await Singleton.Instance.EQP.CreateEquipment(e);
<<<<<<< HEAD
=======

                AllEquipment = Singleton.Instance.EQP.GetEquipments().Result;

                 Equipment newlyCreatedEquip = AllEquipment.Last();
>>>>>>> parent of de566b5... many changes

                AllEquipment = Singleton.Instance.EQP.GetEquipments().Result;

            }
            ObsEquipment.Clear();
            ConvertToObs();

        }

        public async void DeleteEquipMethod()
        {
            if (SelectedEquipment != null)
            {
                ListOfErrors = Singleton.Instance.ERP.GetErrors().Result;
                    foreach (Error e in ListOfErrors)
                    {
                        if (SelectedEquipment.Uid == e.Uid)
                        {
                            await Singleton.Instance.ERP.DeleteError(e.Fid);
                        }
                    }
                    await Singleton.Instance.EQP.DeleteEquipment(SelectedEquipment.Uid);
            }
            ObsEquipment.Clear();
            ConvertToObs();
        }

        public async void EditMethod()
        {
            SelectedEquipment.IsWorking = IsWorking;
            await Singleton.Instance.EQP.UpdateEquipment(SelectedEquipment.Uid, SelectedEquipment);
            ObsEquipment.Clear();
            ConvertToObs();

        }

        public void ConvertToObs()
        {
            AllEquipment = Singleton.Instance.EQP.GetEquipments().Result;
            
                foreach (Equipment e in AllEquipment)
                {
                    ObsEquipment.Add(e);
                }
            
        }


        #region Computer Methods

        #endregion

        #region Smartboard Methods

        #endregion

        #region Smartphone Methods

        #endregion

        #region Tablet Methods

        #endregion

        #endregion

        #region Inotify
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged
            ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new
                PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region GoBack
        private RelayCommand _goBack;

        public RelayCommand GoBack
        {
            get { return _goBack; }
            set { _goBack = value; }
        }

        public void GoBackMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(AdminMainpage));
        }
        #endregion

        #region WIP
        /* Equipment newlyCreatedEquip = AllEquipment.Last();

                 switch (TypeOfEquipment)
                 {
                     case "Computer":

                         Computer pc = new Computer(e);
         await Singleton.Instance.COM.CreateComputer(pc);

         var messageDialogue1 = new MessageDialog($"A computer has been added");
         messageDialogue1.Commands.Add(new UICommand("Close"));
                         await messageDialogue1.ShowAsync();
                         break;
                     case "Smartboard":

                         await Singleton.Instance.SB.CreateSmartboard(e.SmartBoard);

         var messageDialogue2 = new MessageDialog($"A smartboard has been added");
         messageDialogue2.Commands.Add(new UICommand("Close"));
                         await messageDialogue2.ShowAsync();
                         break;
                     case "Smartphone":

                         await Singleton.Instance.SP.CreateSmartphone(e.SmartPhone);

         var messageDialogue3 = new MessageDialog($"A Smartphone has been added");
         messageDialogue3.Commands.Add(new UICommand("Close"));
                         await messageDialogue3.ShowAsync();
                         break;
                     case "Tablet":

                         await Singleton.Instance.TAB.CreateTablet(e.Tablet);

         var messageDialogue4 = new MessageDialog($"A tablet has been added");
         messageDialogue4.Commands.Add(new UICommand("Close"));
                         await messageDialogue4.ShowAsync();
                         break;

                     default:
                         break;


                 }*/
        #endregion
    }

}
