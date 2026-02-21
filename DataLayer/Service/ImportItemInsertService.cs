using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ImportItemInsertService:IImportItemInsertService
    {
        public bool Insert(List<InsertImportItem> insertImportItems)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    foreach (var item in insertImportItems)
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_INSERT_IMPORT_ITEMS", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;


                            // Pass the item parameters
                            cmd.Parameters.AddWithValue("ACTION", 1);
                            cmd.Parameters.AddWithValue("ITEM_CODE", item.ITEM_CODE);
                            cmd.Parameters.AddWithValue("BARCODE", item.BARCODE);
                            cmd.Parameters.AddWithValue("DESCRIPTION", item.DESCRIPTION);
                            cmd.Parameters.AddWithValue("POS_DESCRIPTION", item.POS_DESCRIPTION);
                            cmd.Parameters.AddWithValue("ARABIC_DESCRIPTION", item.ARABIC_DESCRIPTION);
                            cmd.Parameters.AddWithValue("DEPT_CODE", item.DEPT_CODE);
                            cmd.Parameters.AddWithValue("DEPT_NAME", item.DEPT_NAME);
                            cmd.Parameters.AddWithValue("CAT_CODE", item.CAT_CODE);
                            cmd.Parameters.AddWithValue("CAT_NAME", item.CAT_NAME);
                            cmd.Parameters.AddWithValue("SUBCAT_CODE", item.SUBCAT_CODE);
                            cmd.Parameters.AddWithValue("SUBCAT_NAME", item.SUBCAT_NAME);
                            cmd.Parameters.AddWithValue("BRAND_CODE", item.BRAND_CODE);
                            cmd.Parameters.AddWithValue("BRAND_NAME", item.BRAND_NAME);
                            cmd.Parameters.AddWithValue("SUPP_CODE", item.SUPP_CODE);
                            cmd.Parameters.AddWithValue("SUPP_NAME", item.SUPP_NAME);
                            cmd.Parameters.AddWithValue("SUPP_PRICE", item.SUPP_PRICE);
                            cmd.Parameters.AddWithValue("COST", item.COST);
                            cmd.Parameters.AddWithValue("VAT_PERCENT", item.VAT_PERCENT);
                            cmd.Parameters.AddWithValue("PRICE", item.PRICE);
                            cmd.Parameters.AddWithValue("ITEM_TYPE", item.ITEM_TYPE);
                            cmd.Parameters.AddWithValue("IS_DISCOUNTABLE", item.IS_DISCOUNTABLE);
                            cmd.Parameters.AddWithValue("COSTING_METHOD", item.COSTING_METHOD);
                            cmd.Parameters.AddWithValue("IS_NOT_SALE_ITEM", item.IS_NOT_SALE_ITEM);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed and rethrow
                throw;
            }
        }
    }
}
