using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KVICClassLIB
{
    public class impagency_prop
    {
        private int _id;
        private string _ImpAgencyName;
        private string _Address;
        private string _Phone;
        private string _Fax;
        private string _Email;
        private int _productId;
        private string _productType;

        public int id { get { return _id; } set { _id = value; } }
        public string ImpAgencyName { get { return _ImpAgencyName; } set { _ImpAgencyName = value; } }
        public string Address { get { return _Address; } set { _Address = value; } }
        public string Phone { get { return _Phone; } set { _Phone = value; } }
        public string Fax { get { return _Fax; } set { _Fax = value; } }
        public string Email { get { return _Email; } set { _Email = value; } }
        public int ProductId { get { return _productId; } set { _productId = value; } }

        public string ProductType { get { return _productType; } set { _productType = value; } }
    }
}
