using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JVVNLClassLIB
{
    public class dailycollection_Prop
    {
        private int _Id;
        private double  _ThosandNotes;
        private double _FiveHundredNotes;
        private double _HundredNotes;
        private double _FiftyNote;
        private double _TwentyNotes;
        private double _TenNotes;
        private double _FiveNotes;
        private double _TwoNotes;
        private double _OneNotes;
        private string _Username;
        private string _CDate;
        private double _cashAmt;
        private double _chequeAmt;


        public int Id { get { return _Id; } set { _Id= value; } }
        public double ThosandNotes { get { return _ThosandNotes; } set { _ThosandNotes = value; } }
        public double FiveHundredNotes { get { return _FiveHundredNotes; } set { _FiveHundredNotes = value; } }
        public double HundredNotes { get { return _HundredNotes; } set { _HundredNotes = value; } }
        public double FiftyNote { get { return _FiftyNote; } set { _FiftyNote  = value; } }
        public double TwentyNotes { get { return _TwentyNotes; } set { _TwentyNotes = value; } }
        public double TenNotes { get { return _TenNotes; } set { _TenNotes = value; } }
        public double FiveNotes { get { return _FiveNotes; } set { _FiveNotes = value; } }
        public double TwoNotes { get { return _TwoNotes; } set { _TwoNotes = value; } }
        public double OneNotes { get { return _OneNotes; } set { _OneNotes = value; } }
        public string CDate { get { return _CDate; } set { _CDate= value; } }
        public string UserName{get {return _Username;}set{_Username= value;}}

        public double CashAmt { get { return _cashAmt; } set { _cashAmt = value; } }
        public double ChequeAmt { get { return _chequeAmt; } set { _chequeAmt = value; } }
        
    }
}
