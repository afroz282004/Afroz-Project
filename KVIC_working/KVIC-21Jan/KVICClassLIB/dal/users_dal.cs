using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KVICClassLIB
{
     partial class users_dal
    {
        #region "General Declaration"
            Common com = new Common();
        #endregion

        #region "User Data Methods"
            public string UsersCheckLogin(users_Prop userprop)
            {
                try
                {
                    DataSet ds= SqlHelper.ExecuteDataset(com.con, "usp_Users_checklogin", new object[] {userprop.UserName,userprop.Password  }) ;
                    if (ds.Tables.Count > 1)
                        return ds.Tables[1].Rows[0][0].ToString();
                    else
                        return ds.Tables[0].Rows[0][0].ToString();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            public DataTable UserSearch(users_Prop userprop)
            {
                try
                {
                return SqlHelper.ExecuteDataset(com.con, "usp_Users_Search", new object[] { userprop.UserName}).Tables[0];  
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            public DataTable UserSelect(users_Prop userprop)
            {
                try
                {
                    return SqlHelper.ExecuteDataset(com.con, "usp_Users_Select", new object[] { userprop.UserName }).Tables[0];
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            public string UserUpdatePassword(users_Prop userprop)
            { 
                try
                {
                    return SqlHelper.ExecuteDataset(com.con, "usp_Users_UpdatePassword", new object[] { userprop.UserName, userprop.Password}).Tables[0].Rows[0][0].ToString();  
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        public string UserBlock(users_Prop userprop)
            { 
                try
                {
                    return SqlHelper.ExecuteDataset(com.con, "usp_Users_Block", new object[] { userprop.UserName}).Tables[0].Rows[0][0].ToString();  
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        
            public string UsersInsert(users_Prop userprop)
            {
                try
                {
                    return SqlHelper.ExecuteDataset(com.con, "usp_Users_Insert", new object[] { 
                                                                                userprop.UserName
                                                                                ,userprop.Password
                                                                                ,userprop.FirstName
                                                                                ,userprop.LastName
                                                                                ,userprop.StateID
                                                                                ,userprop.GroupID
                                                                                ,userprop.PhoneNo
                                                                                ,userprop.EmailID
                                                                                }).Tables[0].Rows[0][0].ToString() ;  
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            public string UsersUpdate(users_Prop userprop)
            {
                try
                {
                    return SqlHelper.ExecuteDataset(com.con, "usp_Users_update", new object[] { 
                                                                                userprop.UserName
                                                                                ,userprop.FirstName
                                                                                ,userprop.LastName
                                                                                ,userprop.StateID
                                                                                ,userprop.GroupID
                                                                                ,userprop.PhoneNo
                                                                                ,userprop.EmailID
                                                                                }).Tables[0].Rows[0][0].ToString();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            public string Logout(users_Prop userprop)
            {
                try
                {
                    return SqlHelper.ExecuteDataset(com.con, "usp_GetLoginLogout", new object[] { userprop.UserName, "OUT" }).Tables[0].Rows[0][0].ToString();  
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            public DataTable UserClusterSelect(users_Prop userprop)
            {
                try
                {
                    return SqlHelper.ExecuteDataset(com.con, "usp_Select_UserClusterDetails", new object[] { userprop.UserName }).Tables[0];
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        #endregion
    }
}
