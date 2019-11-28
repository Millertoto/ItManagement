using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Persistencies
{
    class Persistency
    {
        public static List<Error> GetErrors()
        {
            using (var db = new SkoledbContext())
            {
                return db.Errors.ToList();
                //List<Error> list = new List<Error>();
                /*foreach (var e in db.Errors)
                {
                    list.Add(e);
                }
                return list; */
            }
        }
    }
}
