using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Services
{
    public class ARManualMatchingService:IARManualMatchingService
    {

        public ARReceiptResponse receiptList(ARReceiptInput vInput)
        {
			ARReceiptResponse res = new ARReceiptResponse();
            res.data = new List<ARReceiptData>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {

					using (SqlCommand cmd = new SqlCommand())
					{
						cmd.Connection = connection;
						cmd.CommandType = CommandType.Text;
						cmd.CommandText = @"
                        SELECT 
                            H.ID AS ReceiptID,
                            D.ID AS ReceiptDetailID,
                            AC.REF_NO AS REC_NO,
                            H.REC_DATE,
                            D.BILL_NO AS BILL_NO,
	                        C.CUST_NAME AS CUSTOMER,
                            D.SERVICE_CODE,
                            D.REC_AMOUNT,
                            D.DENIED_AMOUNT,
                            D.DENIAL_CODE
                        FROM TB_CUST_REC_DETAIL D
                        INNER JOIN TB_CUST_REC_HEADER H
                            ON D.REC_ID = H.ID
                        INNER JOIN TB_CUSTOMER C
                            ON C.ID = H.CUSTOMER_ID
                        INNER JOIN TB_AC_TRANS_HEADER AC 
	                        ON AC.TRANS_ID = H.TRANS_ID
                        WHERE D.IS_MATCHED = 0
                          AND C.COMPANY_ID = @CompanyID
                        ORDER BY H.REC_DATE DESC";

						cmd.Parameters.AddWithValue("@CompanyID", vInput.CompanyID);

						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								res.data.Add(new ARReceiptData
								{
                                    ReceiptID = reader["ReceiptID"] == DBNull.Value
										? 0
										: Convert.ToInt32(reader["ReceiptID"]),
                                    ReceiptDetailID = reader["ReceiptDetailID"] == DBNull.Value
                                        ? 0
                                        : Convert.ToInt32(reader["ReceiptDetailID"]),
                                    ReceiptNo = reader["REC_NO"]?.ToString() ?? "",
                                    Customer = reader["CUSTOMER"]?.ToString() ?? "",
                                    Date = reader["REC_DATE"] == DBNull.Value
										? ""
										: Convert.ToDateTime(reader["REC_DATE"]).ToString("dd/MM/yyyy"),

									ReferenceNo = reader["BILL_NO"]?.ToString() ?? "",
									ServiceCode = reader["SERVICE_CODE"]?.ToString() ?? "",

									Amount = reader["REC_AMOUNT"] == DBNull.Value
										? 0
										: Convert.ToDecimal(reader["REC_AMOUNT"]),

									RejectedAmount = reader["DENIED_AMOUNT"] == DBNull.Value
										? 0
										: Convert.ToDecimal(reader["DENIED_AMOUNT"]),

									RejectedReason = reader["DENIAL_CODE"]?.ToString() ?? ""
								});
							}
						}
					}
				}

                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = "Error: " + ex.Message;
            }

            return res;
        }


        public ARInvoiceResponse invoiceList(ARInvoiceInput vInput)
        {
            ARInvoiceResponse res = new ARInvoiceResponse();
            res.data = new List<ARInvoiceData>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                    SELECT
                        D.ID AS InvoiceID,
                        H.REF_NO AS InvoiceNo,
                        H.SALE_DATE AS [Date],
                        C.CUST_NAME AS Customer,
                        I.ITEM_CODE AS SERVICE_CODE,
                        D.TOTAL_AMOUNT - D.RECEIVED_AMOUNT - REJECTED_AMOUNT  AS Amount
                    FROM TB_SALE_HEADER H
                    INNER JOIN TB_CUSTOMER C
                        ON C.ID = H.CUSTOMER_ID
                    INNER JOIN TB_SALE_DETAIL D
                        ON D.SALE_ID = H.ID
                    INNER JOIN TB_ITEMS I
                        ON I.ID = D.ITEM_ID
                    WHERE H.REF_NO = @ReferenceNo
                    AND D.TOTAL_AMOUNT - D.RECEIVED_AMOUNT - D.REJECTED_AMOUNT > 0
                    ORDER BY H.SALE_DATE DESC";

                        cmd.Parameters.AddWithValue("@ReferenceNo", vInput.ReferenceNo);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.data.Add(new ARInvoiceData
                                {
                                    InvoiceID = reader["InvoiceID"] == DBNull.Value
                                        ? 0
                                        : Convert.ToInt32(reader["InvoiceID"]),

                                    InvoiceNo = reader["InvoiceNo"]?.ToString() ?? "",
                                    Customer = reader["Customer"]?.ToString() ?? "",    

                                    Date = reader["Date"] == DBNull.Value
                                        ? ""
                                        : Convert.ToDateTime(reader["Date"]).ToString("dd/MM/yyyy"),

                                    ServiceCode = reader["SERVICE_CODE"]?.ToString() ?? "",

                                    Amount = reader["Amount"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDecimal(reader["Amount"])
                                });
                            }
                        }
                    }
                }

                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = "Error: " + ex.Message;
            }

            return res;
        }



        public ManualProcessResponse ManualProcess(ManualProcessInput vinput)
        {
            ManualProcessResponse response = new ManualProcessResponse();

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("InvoiceID", typeof(int));
                dt.Columns.Add("RejectedAmount", typeof(decimal));
                dt.Columns.Add("ReceivedAmount", typeof(decimal));

                if (vinput.data != null)
                {
                    foreach (var item in vinput.data)
                    {
                        dt.Rows.Add(
                            item.InvoiceID,
                            (object?)item.RejectedAmount ?? DBNull.Value,
                            (object?)item.ReceivedAmount ?? DBNull.Value
                        );
                    }
                }

                using (SqlConnection connection = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_AR_MANUAL_PROCESS", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ReceiptDetailID", vinput.ReceiptDetailID);

                    SqlParameter param = cmd.Parameters.AddWithValue("@InvoiceData", dt);
                    param.SqlDbType = SqlDbType.Structured;
                    param.TypeName = "dbo.UDT_ManualProcessInvoice";

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            response.flag = dr["Flag"].ToString();
                            response.message = dr["Message"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = "0";
                response.message = ex.Message;
            }

            return response;
        }

    }
}
