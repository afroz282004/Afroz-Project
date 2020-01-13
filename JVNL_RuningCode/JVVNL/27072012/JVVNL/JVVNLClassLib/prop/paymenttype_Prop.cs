using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JVVNLClassLIB
{
    public class paymenttype_Prop
    {
        private int _Paymentid;
        private string _PaymentType;
        
        public int Paymentid { get { return _Paymentid ; } set { _Paymentid= value; } }
        public string PaymentType{ get { return _PaymentType; } set { _PaymentType= value; } }
    }
}
