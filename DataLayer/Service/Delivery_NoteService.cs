using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

namespace MicroApi.DataLayer.Service
{
    public class Delivery_NoteService:IDelivery_NoteService
    {
        public Int32 Insert(Delivery_Note deliverynote)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add("SO_DETAIL_ID", typeof(Int32));
                    tbl.Columns.Add("ITEM_ID", typeof(Int32));
                    tbl.Columns.Add("REMARKS", typeof(string));
                    tbl.Columns.Add("UOM", typeof(string));
                    tbl.Columns.Add("QUANTITY", typeof(double));

                    if (deliverynote.DETAILS != null && deliverynote.DETAILS.Any())
                    {
                        foreach (var d in deliverynote.DETAILS)
                        {
                            DataRow dRow = tbl.NewRow();
                            dRow["SO_DETAIL_ID"] = d.SO_DETAIL_ID;
                            dRow["ITEM_ID"] = d.ITEM_ID;
                            dRow["REMARKS"] = (object?)d.REMARKS ?? DBNull.Value;
                            dRow["UOM"] = d.UOM;
                            dRow["QUANTITY"] = d.QUANTITY;
                            tbl.Rows.Add(dRow);
                        }
                    }

                    SqlCommand cmd = new SqlCommand("SP_TB_DELIVERY_NOTE", connection, objtrans);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 1);
                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", deliverynote.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", deliverynote.STORE_ID);
                    cmd.Parameters.AddWithValue("@DN_DATE", (object?)deliverynote.DN_DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REF_NO", (object?)deliverynote.REF_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CUST_ID", deliverynote.CUST_ID);
                    cmd.Parameters.AddWithValue("@CONTACT_NAME", (object?)deliverynote.CONTACT_NAME ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_PHONE", (object?)deliverynote.CONTACT_PHONE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_FAX", (object?)deliverynote.CONTACT_FAX ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_MOBILE", (object?)deliverynote.CONTACT_MOBILE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SALESMAN_ID", deliverynote.SALESMAN_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", deliverynote.FIN_ID);
                    cmd.Parameters.AddWithValue("@TOTAL_QTY", deliverynote.TOTAL_QTY);
                    cmd.Parameters.AddWithValue("@USER_ID", deliverynote.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", (object?)deliverynote.NARRATION ?? DBNull.Value);

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_DN_DETAIL", tbl);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    object result = cmd.ExecuteScalar();
                    Int32 newId = ADO.ToInt32(result);

                    objtrans.Commit();
                    return newId;
                }
                catch (Exception ex)
                {
                    objtrans.Rollback();
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }
        public Int32 Update(Delivery_NoteUpdate deliverynote)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add("SO_DETAIL_ID", typeof(Int32));
                    tbl.Columns.Add("ITEM_ID", typeof(Int32));
                    tbl.Columns.Add("REMARKS", typeof(string));
                    tbl.Columns.Add("UOM", typeof(string));
                    tbl.Columns.Add("QUANTITY", typeof(double));

                    if (deliverynote.DETAILS != null && deliverynote.DETAILS.Any())
                    {
                        foreach (var d in deliverynote.DETAILS)
                        {
                            DataRow dRow = tbl.NewRow();
                            dRow["SO_DETAIL_ID"] = d.SO_DETAIL_ID;
                            dRow["ITEM_ID"] = d.ITEM_ID;
                            dRow["REMARKS"] = (object?)d.REMARKS ?? DBNull.Value;
                            dRow["UOM"] = d.UOM;
                            dRow["QUANTITY"] = d.QUANTITY;
                            tbl.Rows.Add(dRow);
                        }
                    }

                    SqlCommand cmd = new SqlCommand("SP_TB_DELIVERY_NOTE", connection, objtrans);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 2);
                    cmd.Parameters.AddWithValue("@ID", deliverynote.ID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", deliverynote.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", deliverynote.STORE_ID);
                    cmd.Parameters.AddWithValue("@DN_DATE", (object?)deliverynote.DN_DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REF_NO", (object?)deliverynote.REF_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CUST_ID", deliverynote.CUST_ID);
                    cmd.Parameters.AddWithValue("@CONTACT_NAME", (object?)deliverynote.CONTACT_NAME ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_PHONE", (object?)deliverynote.CONTACT_PHONE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_FAX", (object?)deliverynote.CONTACT_FAX ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_MOBILE", (object?)deliverynote.CONTACT_MOBILE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SALESMAN_ID", deliverynote.SALESMAN_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", deliverynote.FIN_ID);
                    cmd.Parameters.AddWithValue("@TOTAL_QTY", deliverynote.TOTAL_QTY);
                    cmd.Parameters.AddWithValue("@USER_ID", deliverynote.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", (object?)deliverynote.NARRATION ?? DBNull.Value);

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_DN_DETAIL", tbl);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    object result = cmd.ExecuteScalar();
                    Int32 updateid = ADO.ToInt32(result);

                    objtrans.Commit();
                    return updateid;
                }
                catch (Exception ex)
                {
                    objtrans.Rollback();
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }
        public List<SODetail> GetSO(DeliveryRequest request)
        {
            List<SODetail> items = new List<SODetail>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_TB_DELIVERY_NOTE", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", 3);
                cmd.Parameters.AddWithValue("@CUST_ID", request.CUST_ID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    items.Add(new SODetail
                    {
                        SO_DETAIL_ID = dr["ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ID"]),
                        ITEM_ID = dr["ITEM_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ITEM_ID"]),
                        REMARKS = dr["REMARKS"] == DBNull.Value ? null : dr["REMARKS"].ToString(),
                        UOM = dr["UOM"] == DBNull.Value ? null : dr["UOM"].ToString(),
                        QUANTITY = dr["QUANTITY"] == DBNull.Value ? (double?)null : Convert.ToDouble(dr["QUANTITY"]),
                        ITEM_CODE = dr["ITEM_CODE"] == DBNull.Value ? null : dr["ITEM_CODE"].ToString(),
                        DESCRIPTION = dr["DESCRIPTION"] == DBNull.Value ? null : dr["DESCRIPTION"].ToString(),
                    });
                }
            }
            return items;
        }
        public Delivery_Note_List_Response GetDeliveryNoteList()
        {
            Delivery_Note_List_Response response = new Delivery_Note_List_Response();
            List<Delivery_Note_List> data = new List<Delivery_Note_List>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_DELIVERY_NOTE", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 0);
                    cmd.Parameters.AddWithValue("@ID", 0); 
                    //connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Delivery_Note_List item = new Delivery_Note_List
                            {
                                ID = dr["ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["ID"]) : null,
                                COMPANY_ID = dr["COMPANY_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["COMPANY_ID"]) : null,
                                STORE_ID = dr["STORE_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["STORE_ID"]) : null,
                                DN_DATE = dr["DN_DATE"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["DN_DATE"]) : null,
                                DN_NO = dr["DN_NO"] != DBNull.Value ? dr["DN_NO"].ToString() : null,
                                REF_NO = dr["REF_NO"] != DBNull.Value ? dr["REF_NO"].ToString() : null,
                                CUST_ID = dr["CUST_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["CUST_ID"]) : null,
                                CONTACT_NAME = dr["CONTACT_NAME"] != DBNull.Value ? dr["CONTACT_NAME"].ToString() : null,
                                CONTACT_PHONE = dr["CONTACT_PHONE"] != DBNull.Value ? dr["CONTACT_PHONE"].ToString() : null,
                                CONTACT_MOBILE = dr["CONTACT_MOBILE"] != DBNull.Value ? dr["CONTACT_MOBILE"].ToString() : null,
                                CONTACT_FAX = dr["CONTACT_FAX"] != DBNull.Value ? dr["CONTACT_FAX"].ToString() : null,
                                SALESMAN_ID = dr["SALESMAN_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["SALESMAN_ID"]) : null,
                                TOTAL_QTY = dr["TOTAL_QTY"] != DBNull.Value ? (double?)Convert.ToDouble(dr["TOTAL_QTY"]) : null,
                                STATUS = dr["STATUS"] != DBNull.Value ? dr["STATUS"].ToString() : null,
                                TRANS_ID = dr["TRANS_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["TRANS_ID"]) : null,
                                STORE_NAME = dr["STORE_NAME"] != DBNull.Value ? dr["STORE_NAME"].ToString() : null,
                                CUSTOMER_NAME = dr["CUST_NAME"] != DBNull.Value ? dr["CUST_NAME"].ToString() : null,
                            };
                            data.Add(item);
                        }
                    }

                    response.flag = 1;
                    response.Message = "Success";
                    response.Data = data;
                    return response;
                }
                catch (Exception ex)
                {
                    response.flag = 0;
                    response.Message = ex.Message;
                    response.Data = null;
                    return response;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }
        public Delivery_Note_Select_Response GetDeliveryNoteById(int id)
        {
            Delivery_Note_Select_Response response = new Delivery_Note_Select_Response();
            Delivery_Note_Select deliveryNote = new Delivery_Note_Select();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_DELIVERY_NOTE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 0); // Fetch by ID
                        cmd.Parameters.AddWithValue("@ID", id);

                        DataTable tbl = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tbl);

                        if (tbl.Rows.Count > 0)
                        {
                            // Initialize header object from first row
                            DataRow firstRow = tbl.Rows[0];
                            deliveryNote = new Delivery_Note_Select
                            {
                                ID = firstRow["ID"] != DBNull.Value ? (int?)Convert.ToInt32(firstRow["ID"]) : null,
                                COMPANY_ID = firstRow["COMPANY_ID"] != DBNull.Value ? (int?)Convert.ToInt32(firstRow["COMPANY_ID"]) : null,
                                STORE_ID = firstRow["STORE_ID"] != DBNull.Value ? (int?)Convert.ToInt32(firstRow["STORE_ID"]) : null,
                                DN_DATE = firstRow["DN_DATE"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(firstRow["DN_DATE"]) : null,
                                DN_NO = firstRow["DN_NO"] != DBNull.Value ? firstRow["DN_NO"].ToString() : null,
                                REF_NO = firstRow["REF_NO"] != DBNull.Value ? firstRow["REF_NO"].ToString() : null,
                                CUST_ID = firstRow["CUST_ID"] != DBNull.Value ? (int?)Convert.ToInt32(firstRow["CUST_ID"]) : null,
                                CONTACT_NAME = firstRow["CONTACT_NAME"] != DBNull.Value ? firstRow["CONTACT_NAME"].ToString() : null,
                                CONTACT_PHONE = firstRow["CONTACT_PHONE"] != DBNull.Value ? firstRow["CONTACT_PHONE"].ToString() : null,
                                CONTACT_FAX = firstRow["CONTACT_FAX"] != DBNull.Value ? firstRow["CONTACT_FAX"].ToString() : null,
                                CONTACT_MOBILE = firstRow["CONTACT_MOBILE"] != DBNull.Value ? firstRow["CONTACT_MOBILE"].ToString() : null,
                                SALESMAN_ID = firstRow["SALESMAN_ID"] != DBNull.Value ? (int?)Convert.ToInt32(firstRow["SALESMAN_ID"]) : null,
                                TOTAL_QTY = firstRow["TOTAL_QTY"] != DBNull.Value ? (double?)Convert.ToDouble(firstRow["TOTAL_QTY"]) : null,
                                TRANS_ID = firstRow["TRANS_ID"] != DBNull.Value ? (int?)Convert.ToInt32(firstRow["TRANS_ID"]) : null,
                                DETAILS = new List<Delivery_Note_Detail_Select>()
                            };

                            foreach (DataRow dr in tbl.Rows)
                            {
                                var detail = new Delivery_Note_Detail_Select
                                {
                                    DETAIL_ID = dr["DETAIL_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["DETAIL_ID"]) : null,
                                    SO_DETAIL_ID = dr["SO_DETAIL_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["SO_DETAIL_ID"]) : null,
                                    ITEM_ID = dr["ITEM_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["ITEM_ID"]) : null,
                                    REMARKS = dr["REMARKS"] != DBNull.Value ? dr["REMARKS"].ToString() : null,
                                    UOM = dr["UOM"] != DBNull.Value ? dr["UOM"].ToString() : null,
                                    QUANTITY = dr["QUANTITY"] != DBNull.Value ? (double?)Convert.ToDouble(dr["QUANTITY"]) : null
                                };
                                deliveryNote.DETAILS.Add(detail);
                            }

                            response.flag = 1;
                            response.Message = "Success";
                            response.Data = deliveryNote;
                        }
                        else
                        {
                            response.flag = 0;
                            response.Message = "No data found";
                            response.Data = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
                response.Data = null;
            }

            return response;
        }
        public Int32 Approve(Delivery_NoteUpdate deliverynote)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add("SO_DETAIL_ID", typeof(Int32));
                    tbl.Columns.Add("ITEM_ID", typeof(Int32));
                    tbl.Columns.Add("REMARKS", typeof(string));
                    tbl.Columns.Add("UOM", typeof(string));
                    tbl.Columns.Add("QUANTITY", typeof(double));

                    if (deliverynote.DETAILS != null && deliverynote.DETAILS.Any())
                    {
                        foreach (var d in deliverynote.DETAILS)
                        {
                            DataRow dRow = tbl.NewRow();
                            dRow["SO_DETAIL_ID"] = d.SO_DETAIL_ID;
                            dRow["ITEM_ID"] = d.ITEM_ID;
                            dRow["REMARKS"] = (object?)d.REMARKS ?? DBNull.Value;
                            dRow["UOM"] = d.UOM;
                            dRow["QUANTITY"] = d.QUANTITY;
                            tbl.Rows.Add(dRow);
                        }
                    }

                    SqlCommand cmd = new SqlCommand("SP_TB_DELIVERY_NOTE", connection, objtrans);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 5);
                    cmd.Parameters.AddWithValue("@ID", deliverynote.ID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", deliverynote.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", deliverynote.STORE_ID);
                    cmd.Parameters.AddWithValue("@DN_DATE", (object?)deliverynote.DN_DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REF_NO", (object?)deliverynote.REF_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CUST_ID", deliverynote.CUST_ID);
                    cmd.Parameters.AddWithValue("@CONTACT_NAME", (object?)deliverynote.CONTACT_NAME ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_PHONE", (object?)deliverynote.CONTACT_PHONE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_FAX", (object?)deliverynote.CONTACT_FAX ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_MOBILE", (object?)deliverynote.CONTACT_MOBILE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SALESMAN_ID", deliverynote.SALESMAN_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", deliverynote.FIN_ID);
                    cmd.Parameters.AddWithValue("@TOTAL_QTY", deliverynote.TOTAL_QTY);
                    cmd.Parameters.AddWithValue("@USER_ID", deliverynote.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", (object?)deliverynote.NARRATION ?? DBNull.Value);

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_DN_DETAIL", tbl);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    object result = cmd.ExecuteScalar();
                    Int32 updateid = ADO.ToInt32(result);

                    objtrans.Commit();
                    return updateid;
                }
                catch (Exception ex)
                {
                    objtrans.Rollback();
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }
        public bool Delete(int id)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_DELIVERY_NOTE";
                cmd.Parameters.AddWithValue("ACTION", 4);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();

                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DeliveryDoc GetLastDocNo()
        {
            DeliveryDoc res = new DeliveryDoc();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"
                    SELECT TOP 1 VOUCHER_NO 
                    FROM TB_AC_TRANS_HEADER 
                    WHERE TRANS_TYPE = 23
                    ORDER BY TRANS_ID DESC";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        object result = cmd.ExecuteScalar();
                        res.flag = 1;
                        res.DELIVERY_NO = result != null ? Convert.ToInt32(result) : 0;
                        res.Message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
    }
}
