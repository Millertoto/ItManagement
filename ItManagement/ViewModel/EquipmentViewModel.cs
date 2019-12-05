using ItManagement.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ItManagement.Folder;
using ItManagement.Persistencies;
using Type = System.Type;

namespace ItManagement.ViewModel
{
    class EquipmentViewModel
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


        #endregion

        #region Constructor

        public EquipmentViewModel()
        {
            /*_getAllEquipment = new RelayCommand(GetAllEquipmentMethod);*/
            _createEquipment = new RelayCommand(AddEquipment);
            /*_getEquipmentOfType = new RelayCommand(GetEquipmentOfTypeMethod);*/
            _listOfEquipment = EmployeeSingleton.Instance.EQP.GetEquipments().Result;
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

        public List<Equipment> AllEquipment
        {
            get { return _listOfEquipment; }
            set { _listOfEquipment = value; }
        }


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

        #region Methods

        public async void AddEquipment()
        {
            if (TypeOfEquipment == "Computer"
                || TypeOfEquipment == "Smartboard"
                || TypeOfEquipment == "Smartphone"
                || TypeOfEquipment == "Tablet")
            {
                Equipment e = new Equipment(TypeOfEquipment);
                await EmployeeSingleton.Instance.EQP.CreateEquipment(e);

                AllEquipment = EmployeeSingleton.Instance.EQP.GetEquipments().Result;
                Equipment newlyCreatedEquip = AllEquipment.Last();

                switch (newlyCreatedEquip.Type)
                {
                    case "Computer":
                        Computer pc = new Computer(newlyCreatedEquip.Uid);
                        await EmployeeSingleton.Instance.COM.CreateComputer(pc);
                        break;
                    case "Smartboard":
                        SmartBoard sb = new SmartBoard(newlyCreatedEquip.Uid);
                        await EmployeeSingleton.Instance.SB.CreateSmartboard(sb);
                        break;
                    case "Smartphone":
                        SmartPhone sp = new SmartPhone(newlyCreatedEquip.Uid);
                        await EmployeeSingleton.Instance.SP.CreateSmartphone(sp);
                        break;
                    case "Tablet":
                        Tablet tab = new Tablet(newlyCreatedEquip.Uid);
                        await EmployeeSingleton.Instance.TAB.CreateTablet(tab);
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

        #endregion
    }

}
