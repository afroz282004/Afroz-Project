using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using JVVNLClassLIB;
using System.Text.RegularExpressions;


namespace JVVNLWeb
{
    public partial class MasterDataImport : System.Web.UI.Page
    {
        string conn = ConfigurationSettings.AppSettings["conn"].ToString();
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock);
            try
            {
                bulkCopy.DestinationTableName = "dbo.tblConsumer";
                DataTable dt1 = ImportIntoConsumerFromFile();
                DataTable dt3 = dt1;
                if (dt1 != null)
                {
                    bulkCopy.WriteToServer(dt1);

                    bulkCopy.DestinationTableName = "dbo.tblTempConsumer";
                    bulkCopy.WriteToServer(dt3);

                    SqlHelper.ExecuteNonQuery(conn, "usp_consumer_update");

                    bulkCopy.DestinationTableName = "dbo.tblBillDetails";
                    DataTable dt2 = ImportIntoBillDetailsFromFile();
                    if (dt2 != null)
                    {
                        bulkCopy.WriteToServer(dt2);
                    }

                    lblMessage.Text = "Data Imported";
                }
            }
            catch (Exception ee)
            {
                lblMessage.Text = ee.Message.ToString();
            }
        }

        private DataTable ImportIntoConsumerFromFile()
        {
            try
            {
                string xDocumentName = "";
                xDocumentName = FileUpload1.FileName;

                string xFilename = "";
                //string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FolderPath = Server.MapPath(@"~/ImportData/");

                xFilename = FolderPath + xDocumentName;
                Response.Write(xFilename);
                if (!FileUpload1.HasFile)
                {
                    lblMessage.Text = "You have not specified a file ";
                    return null;
                }
                else if (File.Exists(xFilename))
                {

                    lblMessage.Text = "File already exists";
                    return null;
                }
                else if (FileUpload1.PostedFile.ContentLength > 80000000)
                {
                    lblMessage.Text = "File size exceeds 80 MB";
                    return null;
                }

                else
                {
                    FileUpload1.SaveAs(FolderPath + xDocumentName);

                    DataTable dt = new DataTable();
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "ID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "subdivisioncode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AccountNo";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Name";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Address1";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Address2";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Address3";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    StreamReader sr = new StreamReader(xFilename);

                    string input;
                    int r = 1;
                    while ((input = sr.ReadLine()) != null)
                    {
                        if (r >= 3)
                        {
                            string[] s = Regex.Split(input, "#C#");
                            dr = dt.NewRow();
                            dr["subdivisioncode"] = s[0];
                            dr["AccountNo"] = s[1].Trim() + s[2].Trim();
                            dr["Name"] = s[10].Trim();
                            dr["Address1"] = s[11].Trim();
                            dr["Address2"] = s[12].Trim();
                            dr["Address3"] = s[13].Trim();
                            dt.Rows.Add(dr);
                        }
                        r++;
                    }
                    sr.Close();
                    return dt;
                }
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message.ToString();
                return null;
            }
        }

        private DataTable ImportIntoBillDetailsFromFile()
        {
            try
            {
                string xDocumentName = "";
                xDocumentName = FileUpload1.FileName;

                string xFilename = "";
                string FolderPath = Server.MapPath(@"~/ImportData/");
                xFilename = FolderPath + xDocumentName;

                if (FileUpload1.PostedFile.ContentLength > 80000000)
                {
                    lblMessage.Text = "File size exceeds 80 MB";
                    return null;
                }
                else
                {
                    DataTable dt = new DataTable();
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "BillID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ConsumerAccountNo";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "Month";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "Year";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.DateTime");
                    dc.ColumnName = "BillDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.DateTime");
                    dc.ColumnName = "DueDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Double");
                    dc.ColumnName = "Amount";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Double");
                    dc.ColumnName = "AmountAfterDue";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Boolean");
                    dc.ColumnName = "Status";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "subdivisioncode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    StreamReader sr = new StreamReader(xFilename);
                    string input;
                    int r = 1;
                    while ((input = sr.ReadLine()) != null)
                    {
                        if (r >= 3)
                        {
                            string[] s = Regex.Split(input, "#C#");
                            string[] status = Regex.Split(s[15], "#R#");
                            dr = dt.NewRow();
                            dr["subdivisioncode"] = s[0];
                            dr["ConsumerAccountNo"] = s[1].Trim() + s[2].Trim();
                            dr["Month"] = s[14].Trim();
                            dr["Year"] = status[0].Trim();
                            dr["BillDate"] = s[3].Trim();
                            dr["DueDate"] = s[6].Trim();
                            dr["Amount"] = s[9].Trim();
                            dr["AmountAfterDue"] = s[8].Trim();
                            if (status[1].Trim() == "")
                            {
                                dr["Status"] = false;
                            }
                            else
                            {
                                dr["Status"] = status[1].Trim();
                            }
                            dt.Rows.Add(dr);
                        }
                        r++;
                    }
                    sr.Close();
                    return dt;
                }
            }
            catch (Exception e2)
            {
                lblMessage.Text = e2.Message.ToString();
                return null;
            }
        }
    }
}
