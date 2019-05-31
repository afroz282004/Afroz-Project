using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//using MyApp.Models;

namespace MyApp.Models
{
   
    public class db
    {
        string connectionString = @"Data Source=.;Initial Catalog=EmpDb;Integrated Security=SSPI;";
        public DataSet GetMenu()
        {

            string sQL = "select * from SiteMenu";
            SqlDataAdapter da = new SqlDataAdapter(sQL, connectionString);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataSet Getrecord()
        {

            string sQL = "select * from register";
            SqlDataAdapter da = new SqlDataAdapter(sQL, connectionString);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet GetCountry()
        {

            string sQL = "select * from Country";
            SqlDataAdapter da = new SqlDataAdapter(sQL, connectionString);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

       
        public DataSet GetState(int id)
        {
            //string sQL = "select * from TblState";
            string sQL = "select * from TblState where CountryId=" + id + "";
            SqlDataAdapter da = new SqlDataAdapter(sQL, connectionString);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataSet GetCity(int id)
        {

            string sQL = "select * from TblCity where StateId=" + id + "";
            SqlDataAdapter da = new SqlDataAdapter(sQL, connectionString);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet Getrecordbyid(int id)
        {

            string sQL = "select * from register where Sr_no=" + id + "";
            SqlDataAdapter da = new SqlDataAdapter(sQL, connectionString);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public void Add(register reg)
        {
            //string strSQL = "";
            //SqlCommand cmd = new SqlCommand(strSQL, connectionString);
            //connectionString.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();

        }
    }
}