using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Data;


// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    public Common()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #region Connection String
    //public MySqlConnection Con = new MySqlConnection(ConfigurationSettings.AppSettings["Con"].ToString());
    public string con = ConfigurationSettings.AppSettings["conn"].ToString(); 
    #endregion
    

    #region ENCRYPT PASSWORD
    public static string EncryptText(string a_strText)
    {


        // First we need to convert the string into bytes, which

        // means using a text encoder.

        Encoder l_objEnc = System.Text.Encoding.Unicode.GetEncoder();



        // Create a buffer large enough to hold the string

        byte[] l_byteUnicodeText = new byte[a_strText.Length * 2];

        l_objEnc.GetBytes(a_strText.ToCharArray(), 0, a_strText.Length, l_byteUnicodeText, 0, true);


        // Now that we have a byte array we can ask the CSP to hash it

        MD5 l_objMD5 = new MD5CryptoServiceProvider();

        byte[] l_byteResult = l_objMD5.ComputeHash(l_byteUnicodeText);

        // Build the final string by converting each byte

        // into hex and appending it to a StringBuilder

        StringBuilder l_objSB = new StringBuilder();

        for (int i = 0; i < l_byteResult.Length; i++)
        {

            l_objSB.Append(l_byteResult[i].ToString("X2"));

        }


        // And return it

        return l_objSB.ToString();

    }
    #endregion

    public string MMDDYYYY(string strDate)
    {
        if (strDate.Length == 10)
        {
            string[] date = strDate.Split('/');
            return date[1] + "/" + date[0] + "/" + date[2];
        }
            return "";
    }
   
}
