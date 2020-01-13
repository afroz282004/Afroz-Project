using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KVICClassLIB
{
    public class techagency_prop
    {
        private int _id;
        private string _TechAgencyName;
        private string _Address;
        private string _Phone;
        private string _Fax;
        private string _Email;

        public int id { get { return _id; } set { _id = value; } }
        public string TechAgencyName { get { return _TechAgencyName; } set { _TechAgencyName = value; } }
        public string Address { get { return _Address; } set { _Address = value; } }
        public string Phone { get { return _Phone; } set { _Phone = value; } }
        public string Fax { get { return _Fax; } set { _Fax = value; } }
        public string Email { get { return _Email; } set { _Email = value; } }
    }
}
