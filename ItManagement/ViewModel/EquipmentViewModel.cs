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
     public class EquipmentViewModel : INotifyPropertyChanged
    {

        // Skrevet af Martin

        #region Instance Field

        private int _searchUid;
        private int _uid;

        private string _workingOrNot;
        private string _type;

        private bool _isWorking;

        private RelayCommand _createEquipment;
        private RelayCommand _deleteEquipment;
        private RelayCommand _searchEquipment;
        private RelayCommand _editEquipment;
        private RelayCommand _goBack;

        private Equipment _newlyCreatedEquipment;
        private Equipment _selectedEquipment;

        private List<Equipment> _listOfEquipment;
        private List<Equipment> _temporaryList;
        private List<Equipment> _temporaryList2;
        private List<Error> _listOfErrors;

        private ObservableCollection<string> _listBool;
        private ObservableCollection<Equipment> _obsequipment;
        private ObservableCollection<string> _equipmentTypes;
        private ObservableCollection<string> _equipmentTypesOverView;
        private ObservableCollection<Equipment> _filteredEquipment;

        #endregion

        #region Constructor

        public EquipmentViewModel()
        {
            

            _listOfEquipment = Singleton.Instance.EQP.GetEquipments().Result;
            _temporaryList = new List<Equipment>();
            _temporaryList2 = new List<Equipment>();

            _obsequipment = new ObservableCollection<Equipment>();
            _equipmentTypes = new ObservableCollection<string>() { "Computer", "Smartboard", "Tablet", "Smartphone" };
            _equipmentTypesOverView = new ObservableCollection<string>() { "Computer", "Smartboard", "Tablet", "Smartphone", "---" };
            _listBool = new ObservableCollection<string>() { "True", "False", "---" };
            _filteredEquipment = new ObservableCollection<Equipment>();

            _createEquipment = new RelayCommand(AddEquipment);
            _deleteEquipment = new RelayCommand(DeleteEquipMethod);
            _editEquipment = new RelayCommand(EditMethod);
            _searchEquipment = new RelayCommand(SearchEquipmentMethod1);
            _goBack = new RelayCommand(GoBackMethod);

            SearchUid = 0;

            ConvertToObs();
        }

        #endregion

        #region Properties

        #region EquipmentProps

        public Equipment NewlyCreatedEquipment
        {
            get { return _newlyCreatedEquipment; }
            set { _newlyCreatedEquipment = value; }
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


        #endregion

        #region SearchProps
        public int SearchUid
        {
            get { return _searchUid; }
            set { _searchUid = value; }
        }
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

        public RelayCommand GoBack
        {
            get { return _goBack; }
            set { _goBack = value; }
        }
        #endregion

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

        #endregion

        #region Methods

        #region AddEquipment
        /// <summary>
        /// Tilføjer et udstyr
        /// </summary>
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

                Equipment NewlyCreatedEquip = AllEquipment.Last();
 
                 switch (TypeOfEquipment)
                 {
                     case "Computer":
                         Computer pc = new Computer(NewlyCreatedEquip.Uid);
                         await Singleton.Instance.COM.CreateComputer(pc);
                         
 
                         var messageDialogue1 = new MessageDialog($"En computer er tilføjet");
                         messageDialogue1.Commands.Add(new UICommand("Luk"));
                         await messageDialogue1.ShowAsync();
                         break;

                     case "Smartboard":

                         SmartBoard sb = new SmartBoard(NewlyCreatedEquip.Uid);
                        await Singleton.Instance.SB.CreateSmartboard(sb);
 
                         var messageDialogue2 = new MessageDialog($"Et smartboard er tilføjet");
                         messageDialogue2.Commands.Add(new UICommand("Luk"));
                         await messageDialogue2.ShowAsync();
                         break;

                     case "Smartphone":

                        SmartPhone sp = new SmartPhone(NewlyCreatedEquip.Uid);
                        await Singleton.Instance.SP.CreateSmartphone(sp);
 
                         var messageDialogue3 = new MessageDialog($"En smartphone er tilføjet");
                         messageDialogue3.Commands.Add(new UICommand("Luk"));
                         await messageDialogue3.ShowAsync();
                         break;

                     case "Tablet":
                         Tablet tab = new Tablet(NewlyCreatedEquip.Uid);
                        await Singleton.Instance.TAB.CreateTablet(tab);
 
                         var messageDialogue4 = new MessageDialog($"En tablet er tilføjet");
                         messageDialogue4.Commands.Add(new UICommand("Luk"));
                         await messageDialogue4.ShowAsync();
                         break;
 
                     default:
                         break;
 
 
                 }
                


            }
            else
            {
                throw new ArgumentException("TypeOfEquipment is null");
            }

            ObsEquipment.Clear();
            ConvertToObs();

        }
        #endregion

        #region DeleteEquipment
        /// <summary>
        /// Fjerner et udstyr
        /// </summary>
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

                            var messageDialogue1 = new MessageDialog($"En computer er blevet fjernet");
                            messageDialogue1.Commands.Add(new UICommand("Luk"));
                            await messageDialogue1.ShowAsync();
                            break;

                        case "Smartboard":

                            await Singleton.Instance.SB.DeleteSmartboard(SelectedEquipment.Uid);
                            await Singleton.Instance.EQP.DeleteEquipment(SelectedEquipment.Uid);

                            var messageDialogue2 = new MessageDialog($"Et smartboard er blevet fjernet");
                            messageDialogue2.Commands.Add(new UICommand("Luk"));
                            await messageDialogue2.ShowAsync();
                            break;

                        case "Smartphone":

                            await Singleton.Instance.SP.DeleteSmartphone(SelectedEquipment.Uid);
                            await Singleton.Instance.EQP.DeleteEquipment(SelectedEquipment.Uid);

                            var messageDialogue3 = new MessageDialog($"En smartphone er blevet fjernet");
                            messageDialogue3.Commands.Add(new UICommand("Luk"));
                            await messageDialogue3.ShowAsync();
                            break;

                        case "Tablet":
                            await Singleton.Instance.TAB.DeleteTablet(SelectedEquipment.Uid);
                            await Singleton.Instance.EQP.DeleteEquipment(SelectedEquipment.Uid);

                            var messageDialogue4 = new MessageDialog($"En tablet er blevet fjernet");
                            messageDialogue4.Commands.Add(new UICommand("Luk"));
                            await messageDialogue4.ShowAsync();
                            break;


                    }

            }
            else
            {
                var messageDialogue = new MessageDialog($"Vælg et udstyr før du kan fjerne det");
                messageDialogue.Commands.Add(new UICommand("Luk"));
                await messageDialogue.ShowAsync();

            }
            ObsEquipment.Clear();
            ConvertToObs();
        }
        #endregion

        #region EditEquipment
        /// <summary>
        /// Ændrer et udstyr
        /// </summary>
        public async void EditMethod()
        {
            if (SelectedEquipment != null || SelectedEquipment.Uid > 0)
            {
                SelectedEquipment.Type = TypeOfEquipment;
                await Singleton.Instance.EQP.UpdateEquipment(SelectedEquipment.Uid, SelectedEquipment);
                var messageDialogue = new MessageDialog($"Udstyr: {SelectedEquipment} er blevet opdateret");
                messageDialogue.Commands.Add(new UICommand("Luk"));
                await messageDialogue.ShowAsync();


            }
            else
            {
                var messageDialogue = new MessageDialog($"Vælg et udstyr før du kan opdatere det");
                messageDialogue.Commands.Add(new UICommand("Close"));
                await messageDialogue.ShowAsync();

            }
            
            ObsEquipment.Clear();
            ConvertToObs();

        }
        #endregion

        #region SearchMethod
        /// <summary>
        /// Søæger efter et eller flere udstyr
        /// </summary>
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
                var messageDialogue4 = new MessageDialog($"Alt Udstyr");
                messageDialogue4.Commands.Add(new UICommand("Luk"));
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
                    var messageDialogue4 = new MessageDialog($"Færdig!");
                    messageDialogue4.Commands.Add(new UICommand("Luk"));
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
                        var messageDialogue4 = new MessageDialog($"Færdig!");
                        messageDialogue4.Commands.Add(new UICommand("Luk"));
                        await messageDialogue4.ShowAsync();
                        TemporaryList2.Clear();
                    }
                    else
                    {
                        foreach (Equipment e in TemporaryList)
                        {
                            FilteredEquipment.Add(e);
                            

                        }
                        var messageDialogue4 = new MessageDialog($"Færdig!");
                        messageDialogue4.Commands.Add(new UICommand("Luk"));
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
                    var messageDialogue4 = new MessageDialog($"{SearchUid} eksisterer ikke");
                    messageDialogue4.Commands.Add(new UICommand("Luk"));
                    await messageDialogue4.ShowAsync();

                }
            }

        }
        #endregion

        #region List/Obs Methods
        public void ConvertToObs()
        {
            AllEquipment = Singleton.Instance.EQP.GetEquipments().Result;
            
                foreach (Equipment e in AllEquipment)
                {
                    ObsEquipment.Add(e);
                }
            
        }
        #endregion

        #region GoBack

        public void GoBackMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(AdminMainpage));
        }
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

    }

}
