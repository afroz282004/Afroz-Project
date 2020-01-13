using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace JVVNLClassLIB
{
    public class users_bal
    {
        #region GENERAL DECLARATION
        users_dal userdal=null  ;
        string _ErrMessage;
        public string ErrMessage{get { return _ErrMessage; }set { _ErrMessage = value; }}
        #endregion
        public string UserCheckLogin(users_Prop userprop)
        {
            try
            {
                userdal = new users_dal();
                string strMsg = userdal.UsersCheckLogin(userprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public string UserInsert(users_Prop userprop)
        {
            try
            { 
                userdal= new users_dal();
                string strMsg = userdal.UsersInsert(userprop);
                return  strMsg; 
            }
            catch (Exception e)
            {
                ErrMessage = e.Message; 
                return "";
            }
        }
        public string UserUpdate(users_Prop userprop)
        {
            try
            {
                userdal = new users_dal();
                string strMsg = userdal.UsersUpdate(userprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public DataTable UserSearch(users_Prop userprop)
        {
            try
            {
                userdal = new users_dal();
                DataTable dt = userdal.UserSearch(userprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public DataTable UserSelect(users_Prop userprop)
        {
            try
            {
                userdal = new users_dal();
                DataTable dt = userdal.UserSelect(userprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        public string UserUpdatePassword(users_Prop userprop )
        {
            try
            { 
                userdal= new users_dal();
                string strMsg = userdal.UserUpdatePassword(userprop);
                return  strMsg; 
            }
            catch (Exception e)
            {
                ErrMessage = e.Message; 
                return "";
            }
        }
        public string LogOut(users_Prop userprop)
        {
            try
            {
                userdal = new users_dal();
                string strMsg = userdal.Logout(userprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public string UserBlock(users_Prop userprop)
        {
            try
            {
                userdal = new users_dal();
                string strMsg = userdal.UserBlock(userprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        
    }
}
