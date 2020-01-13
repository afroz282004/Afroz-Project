using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JVVNLClassLIB
{
    public class sdo_Prop
    {
        private string _SDOCode;
        private string _SDOName;
        

        public string SDOCode{get {return _SDOCode;}set{_SDOCode= value;}}
        public string SDOName { get { return _SDOName; } set { _SDOName= value; } }
    }
}
