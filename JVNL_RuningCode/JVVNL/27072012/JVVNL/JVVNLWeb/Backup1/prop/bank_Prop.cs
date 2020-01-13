using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JVVNLClassLIB
{
    public class bank_Prop
    {
        private string _BankCode;
        private string _BankName;
        

        public string BankCode{get {return _BankCode;}set{_BankCode= value;}}
        public string BankName { get { return _BankName; } set { _BankName= value; } }
    }
}
