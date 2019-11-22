using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Models
{
     public class ErrorCatalogSingleton
    {
        private const string url = "api/Errors/";
        private List<Errors> _errors;
        private ErrorCatalogSingleton()
        {
            _errors = new List<Errors>();
            var e1 = new Employee(68486, "", "", "", false);
            var e2 = new Employee(68483, "", "", "", false);
            var eq1 = new Computer("", 897);
            var eq2 = new Computer("", 54168);
            _errors.Add(new Errors(1, "Omar",eq1, e2 ));
            _errors.Add(new Errors(2, "Lars", eq2, e1));
        }

        private static ErrorCatalogSingleton _instance;
        public static ErrorCatalogSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ErrorCatalogSingleton();
                }
                return _instance;
            }
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
            }
        }
        //public List<Errors> ErrorsList
        //{
        //    //get { return  _students; }

        //    get
        //    {
        //        return Persistencies.Persistency.getErrors();
        //    }
        //    set
        //    {
        //        _errors = value;

        //    }
        //}


        public void AddError(Errors error)
        {
            _errors.Add(error);

        }

        public void DeleteStudent(Errors e)
        {
            _errors.Remove(e);
        }

        public void UpdateStudent(Errors e)
        {

        }
    }
}
