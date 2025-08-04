using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicroApi.DataLayer.Service
{
    public class TransferService:ITransferService
    {
        public TransferListResponse GetTransferList()
        {
            TransferListResponse transferList = new TransferListResponse
            {
                Data = new List<Transfer>()
            };

            string query = @"
        SELECT 
            TB_TROUT_SUMMARY.TRANSFER_NO,
            TB_TROUT_SUMMARY.TRANSFER_DATE,
            TB_COMPANY_MASTER.COMPANY_NAME AS TRANSFER_TO,
            TB_PACKING.ART_NO,
            TB_PACKING.COLOR,
            TB_ARTICLE_CATEGORY.DESCRIPTION AS CATEGORY,
            TB_PACKING.DESCRIPTION AS PACKING,
            TB_TROUT_ENTRY.RECEVED_TIME,
            TB_TROUT_ENTRY.IS_RECEIVED,
            TB_TROUT_SUMMARY.QUANTITY,
            TB_TROUT_SUMMARY.PAIR_QTY,
            SUM(TB_TROUT_SUMMARY.QUANTITY * TB_TROUT_SUMMARY.PAIR_QTY) AS TOTAL_PAIR_QUANTITY
        FROM 
            TB_TROUT_SUMMARY
        INNER JOIN 
            TB_COMPANY_MASTER ON TB_COMPANY_MASTER.ID = TB_TROUT_SUMMARY.CUSTOMER_ID
        INNER JOIN 
            TB_PACKING ON TB_PACKING.ID = TB_TROUT_SUMMARY.PACKING_ID
        INNER JOIN 
            TB_ARTICLE_CATEGORY ON TB_ARTICLE_CATEGORY.ID = TB_PACKING.CATEGORY_ID
        INNER JOIN
            TB_TROUT_ENTRY ON TB_TROUT_ENTRY.TRANSFER_ID = TB_TROUT_SUMMARY.TRANSFER_ID
            WHERE TB_TROUT_SUMMARY.INVOICE_ID=0
        GROUP BY 
            TB_TROUT_SUMMARY.TRANSFER_NO,
            TB_TROUT_SUMMARY.TRANSFER_DATE,
            TB_COMPANY_MASTER.COMPANY_NAME,
            TB_PACKING.ART_NO,
            TB_PACKING.COLOR,
            TB_ARTICLE_CATEGORY.DESCRIPTION,
            TB_PACKING.DESCRIPTION,
            TB_TROUT_ENTRY.RECEVED_TIME,
            TB_TROUT_ENTRY.IS_RECEIVED,
            TB_TROUT_SUMMARY.QUANTITY,
            TB_TROUT_SUMMARY.PAIR_QTY";

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    //con.Open(); // Make sure to open the connection!

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transfer t = new Transfer
                            {
                                TRANSFER_NO = reader["TRANSFER_NO"].ToString(),
                                TRANSFER_DATE = Convert.ToDateTime(reader["TRANSFER_DATE"]),
                                TRANSFER_TO = reader["TRANSFER_TO"].ToString(),
                                ART_NO = Convert.ToInt32(reader["ART_NO"]),
                                COLOR = reader["COLOR"].ToString(),
                                CATEGORY = reader["CATEGORY"].ToString(),
                                PACKING = reader["PACKING"].ToString(),
                                RECEIVED_TIME = reader["RECEVED_TIME"].ToString(),
                                IS_RECEIVED =  Convert.ToBoolean(reader["IS_RECEIVED"]),
                                TRANSFER_QTY = Convert.ToInt32(reader["QUANTITY"]),
                                PAIR_QTY = Convert.ToInt32(reader["PAIR_QTY"]),
                                TOTAL_PAIR_QTY = Convert.ToInt32(reader["TOTAL_PAIR_QUANTITY"])
                            };
                            transferList.Data.Add(t);
                        }
                    }
                }

                transferList.flag = 1;
                transferList.Message = "Success";
            }
            catch (Exception ex)
            {
                transferList.flag = 0;
                transferList.Message = ex.Message;
                transferList.Data = new List<Transfer>();
            }

            return transferList;
        }




    }
}
