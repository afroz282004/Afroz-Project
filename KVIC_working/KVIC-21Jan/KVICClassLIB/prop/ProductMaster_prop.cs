using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KVICClassLIB
{
   public  class ProductMaster_prop
    {

        private int _ProductID;
        private string _ProductName;

        public int ProductID
        {
            get
            {
                return _ProductID;
            }
            set
            {
                _ProductID = value;
            }
        }

        public string ProductName
        {
            get
            {
                return _ProductName;
            }
            set
            {
                _ProductName = value;
            }
        }
    }
}
