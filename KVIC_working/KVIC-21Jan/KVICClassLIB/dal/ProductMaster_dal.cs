using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KVICClassLIB
{
  public   class ProductMaster_dal
    {

      #region "General Declaration"
      Common com = new Common();
      #endregion

      public string ProductInser_dal(ProductMaster_prop l_ProductMaster_prop)
      {
          try
          {
              return SqlHelper.ExecuteDataset(com.con, "usp_ProductMaster_INSERT", new object[] { l_ProductMaster_prop.ProductName }).Tables[0].Rows[0][0].ToString();

          }
          catch (Exception Ex)
          {
              throw Ex;
          }

      }


      public DataTable ProductSearch_dal(ProductMaster_prop ProductMasterprop)
      {
          try
          {
              return SqlHelper.ExecuteDataset(com.con, "usp_Product_Search", new object[] { ProductMasterprop.ProductName }).Tables[0];
          }

          catch (Exception Ex)
          {
              throw Ex;
          }

      }
    }
}
