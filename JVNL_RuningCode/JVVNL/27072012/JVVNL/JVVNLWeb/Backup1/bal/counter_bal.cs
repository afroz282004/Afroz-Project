using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JVVNLClassLIB
{
    public class counter_bal
    {
        #region GENERAL DECLARATION
        counter_dal counterdal = null;
        string _ErrMessage;
        public string ErrMessage { get { return _ErrMessage; } set { _ErrMessage = value; } }
        #endregion

        public DataTable CounterSelect(counter_Prop counterprop)
        {
            try
            {
                counterdal = new counter_dal ();
                DataTable dt = counterdal.CounterSelect (counterprop );
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public DataTable CounterSearch(counter_Prop counterprop)
        {
            try
            {
                counterdal = new counter_dal();
                DataTable dt = counterdal.CounterSearch(counterprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public string CounterInsert(counter_Prop counterprop)
        {
            try
            {
                counterdal = new counter_dal();
                string strmsg = counterdal.CounterInsert(counterprop);
                return strmsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public string CounterUpdate(counter_Prop counterprop)
        {
            try
            {
                counterdal = new counter_dal();
                string strmsg = counterdal.CounterUpdate(counterprop);
                return strmsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public string CounterDelete(counter_Prop counterprop)
        {
            try
            {
                counterdal = new counter_dal();
                string strmsg = counterdal.CounterDelete(counterprop);
                return strmsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }

    }
    
}
