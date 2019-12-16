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
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ItManagement.ViewModel
{
    class EquipmentViewModel : INotifyPropertyChanged
    {
        #region Instance Field

        private Employee _repairMan;
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
        private RelayCommand _searchEquipment;
        private INotifyPropertyChanged _notifyPropertyChangedImplementation;
        private ObservableCollection<Equipment> _obsequipment;
        private RelayCommand _editEquipment;
        private List<Error> _listOfErrors;
        private ObservableCollection<string> _equipmentTypes;
        private ObservableCollection<string> _equipmentTypesOverView;
        private string _workingOrNot;
        private List<Equipment> _temporaryList;
        private List<Equipment> _temporaryList2;

        private int _searchUid;
        private ObservableCollection<string> _listBool;

        private ObservableCollection<Equipment> _filteredEquipment;

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
            _searchEquipment = new RelayCommand(SearchEquipmentMethod1);
            _obsequipment = new ObservableCollection<Equipment>();
            _equipmentTypes = new ObservableCollection<string>() {"Computer", "Smartboard", "Tablet", "Smartphone"};
            _equipmentTypesOverView = new ObservableCollection<string>() { "Computer", "Smartboard", "Tablet", "Smartphone", "---" };
            _listBool = new ObservableCollection<string>() {"True", "False", "---"};
            _temporaryList = new List<Equipment>();
            _temporaryList2 = new List<Equipment>();
            _filteredEquipment = new ObservableCollection<Equipment>();

            SearchUid = 0;
            _goBack = new RelayCommand(GoBackMethod);
            ConvertToObs();
        }

        #endregion

        #region Properties

        public int SearchUid
        {
            get { return _searchUid; }
            set { _searchUid = value; }
        }

        public string WorkingOrNot
        {
            get { return _workingOrNot; }
            set { _workingOrNot = value; }
        }
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

        public Employee RepairMan
        {
            get { return _repairMan; }
            set { _repairMan = value; }
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

        public List<Equipment> TemporaryList
        {
            get { return _temporaryList; }
            set { _temporaryList = value; }
        }

        public List<Equipment> TemporaryList2
        {
            get { return _temporaryList2; }
            set { _temporaryList2 = value; }
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

        public ObservableCollection<string> EquipmentTypes
        {
            get { return _equipmentTypes; }
            set { _equipmentTypes = value; }
        }

        public ObservableCollection<string> EquipmentTypesOverview
        {
            get { return _equipmentTypesOverView; }
            set { _equipmentTypesOverView = value; }
        }

        public ObservableCollection<Equipment> FilteredEquipment
        {
            get { return _filteredEquipment; }
            set { _filteredEquipment = value; }
        }

        public ObservableCollection<string> ListBool
        {
            get { return _listBool; }
            set { _listBool = value; }
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

        public RelayCommand SearchEquipment
        {
            get { return _searchEquipment; }
            set { _searchEquipment = value; }
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

                AllEquipment = Singleton.Instance.EQP.GetEquipments().Result;

                Equipment newlyCreatedEquip = AllEquipment.Last();
 
                 switch (TypeOfEquipment)
                 {
                     case "Computer":
                         Computer pc = new Computer(newlyCreatedEquip.Uid);
                         await Singleton.Instance.COM.CreateComputer(pc);
                         
 
                         var messageDialogue1 = new MessageDialog($"A computer has been added");
                         messageDialogue1.Commands.Add(new UICommand("Close"));
                         await messageDialogue1.ShowAsync();
                         break;

                     case "Smartboard":

                         SmartBoard sb = new SmartBoard(newlyCreatedEquip.Uid);
                        await Singleton.Instance.SB.CreateSmartboard(sb);
 
                         var messageDialogue2 = new MessageDialog($"A smartboard has been added");
                         messageDialogue2.Commands.Add(new UICommand("Close"));
                         await messageDialogue2.ShowAsync();
                         break;

                     case "Smartphone":

                        SmartPhone sp = new SmartPhone(newlyCreatedEquip.Uid);
                        await Singleton.Instance.SP.CreateSmartphone(sp);
 
                         var messageDialogue3 = new MessageDialog($"A Smartphone has been added");
                         messageDialogue3.Commands.Add(new UICommand("Close"));
                         await messageDialogue3.ShowAsync();
                         break;

                     case "Tablet":
                         Tablet tab = new Tablet(newlyCreatedEquip.Uid);
                        await Singleton.Instance.TAB.CreateTablet(tab);
 
                         var messageDialogue4 = new MessageDialog($"A tablet has been added");
                         messageDialogue4.Commands.Add(new UICommand("Close"));
                         await messageDialogue4.ShowAsync();
                         break;
 
                     default:
                         break;
 
 
                 }

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

                    switch (SelectedEquipment.Type)
                    {
                        case "Computer":
                            await Singleton.Instance.COM.DeleteComputer(SelectedEquipment.Uid);
                            await Singleton.Instance.EQP.DeleteEquipment(SelectedEquipment.Uid);

                            var messageDialogue1 = new MessageDialog($"A computer has been removed");
                            messageDialogue1.Commands.Add(new UICommand("Close"));
                            await messageDialogue1.ShowAsync();
                            break;

                        case "Smartboard":

                            await Singleton.Instance.SB.DeleteSmartboard(SelectedEquipment.Uid);
                            await Singleton.Instance.EQP.DeleteEquipment(SelectedEquipment.Uid);

                            var messageDialogue2 = new MessageDialog($"A smartboard has been removed");
                            messageDialogue2.Commands.Add(new UICommand("Close"));
                            await messageDialogue2.ShowAsync();
                            break;

                        case "Smartphone":

                            await Singleton.Instance.SP.DeleteSmartphone(SelectedEquipment.Uid);
                            await Singleton.Instance.EQP.DeleteEquipment(SelectedEquipment.Uid);

                            var messageDialogue3 = new MessageDialog($"A Smartphone has been removed");
                            messageDialogue3.Commands.Add(new UICommand("Close"));
                            await messageDialogue3.ShowAsync();
                            break;

                        case "Tablet":
                            await Singleton.Instance.TAB.DeleteTablet(SelectedEquipment.Uid);
                            await Singleton.Instance.EQP.DeleteEquipment(SelectedEquipment.Uid);

                            var messageDialogue4 = new MessageDialog($"A tablet has been removed");
                            messageDialogue4.Commands.Add(new UICommand("Close"));
                            await messageDialogue4.ShowAsync();
                            break;


                    }
            }
            else
            {
                var messageDialogue = new MessageDialog($"Select an equipment you wish to remove");
                messageDialogue.Commands.Add(new UICommand("Close"));
                await messageDialogue.ShowAsync();
            }
            ObsEquipment.Clear();
            ConvertToObs();
        }

        public async void EditMethod()
        {
            if (SelectedEquipment != null)
            {
                SelectedEquipment.Type = TypeOfEquipment;
                await Singleton.Instance.EQP.UpdateEquipment(SelectedEquipment.Uid, SelectedEquipment);
                var messageDialogue = new MessageDialog($"Equipment: {SelectedEquipment} has been updated");
                messageDialogue.Commands.Add(new UICommand("Close"));
                await messageDialogue.ShowAsync();

            }
            else
            {
                var messageDialogue = new MessageDialog($"Select an equipment you wish to update");
                messageDialogue.Commands.Add(new UICommand("Close"));
                await messageDialogue.ShowAsync();
            }
            
            ObsEquipment.Clear();
            ConvertToObs();

        }

        public async void SearchEquipmentMethod1()
        {
            if (FilteredEquipment != null)
            {
                FilteredEquipment.Clear();
            }
            
            AllEquipment = await Singleton.Instance.EQP.GetEquipments();

            if ((TypeOfEquipment == null || TypeOfEquipment == "---") && (WorkingOrNot == null || WorkingOrNot == "---") && (SearchUid == 0 || SearchUid == default(int)))
            {
                foreach (Equipment e in AllEquipment)
                {
                    FilteredEquipment.Add(e);
                }
                var messageDialogue4 = new MessageDialog($"All Equipment");
                messageDialogue4.Commands.Add(new UICommand("Close"));
                await messageDialogue4.ShowAsync();
            }
            else if ((TypeOfEquipment != null && TypeOfEquipment != "---") || (WorkingOrNot != null && WorkingOrNot != "---"))
            {
                TemporaryList.Clear();
                TemporaryList2.Clear();
                bool t = false;
                bool w = false;

                if (TypeOfEquipment != null)
                {
                    foreach (Equipment e in AllEquipment)
                    {
                        if (TypeOfEquipment == e.Type)
                        {
                            TemporaryList.Add(e);
                            t = true;
                        }
                    }
                }

                if (WorkingOrNot != null)
                {
                    foreach (Equipment e in AllEquipment)
                    {
                        if (WorkingOrNot == e.IsWorkingString)
                        {
                            TemporaryList2.Add(e);
                            w = true;
                        }
                    }
                }

                if (w && t)
                {
                    foreach (Equipment e in TemporaryList)
                    {
                        foreach (Equipment e2 in TemporaryList2)
                        {
                            if (e.Uid == e2.Uid)
                            {
                                FilteredEquipment.Add(e2);
                                
                            }
                        }
                    }
                    var messageDialogue4 = new MessageDialog($"Current list is showing equipment with type: {TypeOfEquipment} and the working condition of the equipment is {IsWorking}");
                    messageDialogue4.Commands.Add(new UICommand("Close"));
                    await messageDialogue4.ShowAsync();
                    TemporaryList.Clear();
                    TemporaryList2.Clear();
                }
                else if (w || t )
                {
                    if (w)
                    {
                        foreach (Equipment e in TemporaryList2)
                        {
                            FilteredEquipment.Add(e);
                            
                        }
                        var messageDialogue4 = new MessageDialog($"Current list is showing equipment with the working condition as {IsWorking}");
                        messageDialogue4.Commands.Add(new UICommand("Close"));
                        await messageDialogue4.ShowAsync();
                        TemporaryList2.Clear();
                    }
                    else
                    {
                        foreach (Equipment e in TemporaryList)
                        {
                            FilteredEquipment.Add(e);
                            

                        }
                        var messageDialogue4 = new MessageDialog($"Current list is showing equipment of the type: {TypeOfEquipment}");
                        messageDialogue4.Commands.Add(new UICommand("Close"));
                        await messageDialogue4.ShowAsync();

                        TemporaryList.Clear();
                    }
                }
            }
            else
            {
                bool c = false;
                foreach (Equipment e in AllEquipment)
                {
                    if (SearchUid == e.Uid)
                    {
                        FilteredEquipment.Add(e);
                        c = true;

                    }

                }

                if (c == false)
                {
                    var messageDialogue4 = new MessageDialog($"{SearchUid} is not a valid Uid");
                    messageDialogue4.Commands.Add(new UICommand("Close"));
                    await messageDialogue4.ShowAsync();
                }
            }

        }

        /*public async void SearchEquipmentMethod2()
        {
        }*/

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
