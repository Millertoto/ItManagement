using ItManagement.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using ItManagement.PersSingleton;
using ItManagement.Persistencies;
using Type = System.Type;

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
        private INotifyPropertyChanged _notifyPropertyChangedImplementation;

        #endregion

        #region Constructor

        public EquipmentViewModel()
        {
            /*_getAllEquipment = new RelayCommand(GetAllEquipmentMethod);*/
            _createEquipment = new RelayCommand(AddEquipment);
            /*_getEquipmentOfType = new RelayCommand(GetEquipmentOfTypeMethod);*/
            _listOfEquipment = Singleton.Instance.EQP.GetEquipments().Result;
        }

        #endregion

        #region Properties

        public Equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            set { _selectedEquipment = value; }
        }
        public int Uid
        {

            get { return _uid; }
            set { _uid = value; }
        }

        public string TypeOfEquipment
        {
            get { return _type; }
            set { _type = value; }
        }

        #region Lists
        public List<Equipment> AllEquipment
        {
            get { return _listOfEquipment; }
            set { _listOfEquipment = value; }
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

                switch (newlyCreatedEquip.Type)
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
    }

}
