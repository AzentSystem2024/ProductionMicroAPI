using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace MicroApi.DataLayer.Service
{
    public class Delivery_ReturnService : IDelivery_ReturnService
    {
        public Int32 Insert(Delivery_Return deliveryreturn)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add("SO_DETAIL_ID", typeof(Int32));
                    tbl.Columns.Add("DN_DETAIL_ID", typeof(Int32));
                    tbl.Columns.Add("ITEM_ID", typeof(Int32));
                    tbl.Columns.Add("REMARKS", typeof(string));
                    tbl.Columns.Add("UOM", typeof(string));
                    tbl.Columns.Add("QUANTITY", typeof(double));

                    if (deliveryreturn.DETAILS != null && deliveryreturn.DETAILS.Any())
                    {
                        foreach (var d in deliveryreturn.DETAILS)
                        {
                            DataRow dRow = tbl.NewRow();
                            dRow["SO_DETAIL_ID"] = d.SO_DETAIL_ID;
                            dRow["DN_DETAIL_ID"] = d.DN_DETAIL_ID;
                            dRow["ITEM_ID"] = d.ITEM_ID;
                            dRow["REMARKS"] = (object?)d.REMARKS ?? DBNull.Value;
                            dRow["UOM"] = d.UOM;
                            dRow["QUANTITY"] = d.QUANTITY;
                            tbl.Rows.Add(dRow);
                        }
                    }

                    SqlCommand cmd = new SqlCommand("SP_TB_DELIVERY_RETURN", connection, objtrans);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 1);
                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", deliveryreturn.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", deliveryreturn.STORE_ID);
                    cmd.Parameters.AddWithValue("@DR_DATE", (object?)deliveryreturn.DR_DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REF_NO", (object?)deliveryreturn.REF_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CUST_ID", deliveryreturn.CUST_ID);
                    cmd.Parameters.AddWithValue("@CONTACT_NAME", (object?)deliveryreturn.CONTACT_NAME ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_PHONE", (object?)deliveryreturn.CONTACT_PHONE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_FAX", (object?)deliveryreturn.CONTACT_FAX ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_MOBILE", (object?)deliveryreturn.CONTACT_MOBILE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SALESMAN_ID", deliveryreturn.SALESMAN_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", deliveryreturn.FIN_ID);
                    cmd.Parameters.AddWithValue("@TOTAL_QTY", deliveryreturn.TOTAL_QTY);
                    cmd.Parameters.AddWithValue("@USER_ID", deliveryreturn.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", (object?)deliveryreturn.NARRATION ?? DBNull.Value);

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_DR_DETAIL", tbl);
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
        public Int32 Update(Delivery_ReturnUpdate deliveryreturn)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add("SO_DETAIL_ID", typeof(Int32));
                    tbl.Columns.Add("DN_DETAIL_ID", typeof(Int32));
                    tbl.Columns.Add("ITEM_ID", typeof(Int32));
                    tbl.Columns.Add("REMARKS", typeof(string));
                    tbl.Columns.Add("UOM", typeof(string));
                    tbl.Columns.Add("QUANTITY", typeof(double));

                    if (deliveryreturn.DETAILS != null && deliveryreturn.DETAILS.Any())
                    {
                        foreach (var d in deliveryreturn.DETAILS)
                        {
                            DataRow dRow = tbl.NewRow();
                            dRow["SO_DETAIL_ID"] = d.SO_DETAIL_ID;
                            dRow["DN_DETAIL_ID"] = d.DN_DETAIL_ID;
                            dRow["ITEM_ID"] = d.ITEM_ID;
                            dRow["REMARKS"] = (object?)d.REMARKS ?? DBNull.Value;
                            dRow["UOM"] = d.UOM;
                            dRow["QUANTITY"] = d.QUANTITY;
                            tbl.Rows.Add(dRow);
                        }
                    }

                    SqlCommand cmd = new SqlCommand("SP_TB_DELIVERY_RETURN", connection, objtrans);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 2);
                    cmd.Parameters.AddWithValue("@ID", deliveryreturn.ID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", deliveryreturn.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", deliveryreturn.STORE_ID);
                    cmd.Parameters.AddWithValue("@DR_DATE", (object?)deliveryreturn.DR_DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REF_NO", (object?)deliveryreturn.REF_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CUST_ID", deliveryreturn.CUST_ID);
                    cmd.Parameters.AddWithValue("@CONTACT_NAME", (object?)deliveryreturn.CONTACT_NAME ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_PHONE", (object?)deliveryreturn.CONTACT_PHONE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_FAX", (object?)deliveryreturn.CONTACT_FAX ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_MOBILE", (object?)deliveryreturn.CONTACT_MOBILE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SALESMAN_ID", deliveryreturn.SALESMAN_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", deliveryreturn.FIN_ID);
                    cmd.Parameters.AddWithValue("@TOTAL_QTY", deliveryreturn.TOTAL_QTY);
                    cmd.Parameters.AddWithValue("@USER_ID", deliveryreturn.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", (object?)deliveryreturn.NARRATION ?? DBNull.Value);

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_DR_DETAIL", tbl);
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
        public List<DeliverynoteDetail> GetDN(DNRequest request)
        {
            List<DeliverynoteDetail> items = new List<DeliverynoteDetail>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_TB_DELIVERY_RETURN", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", 3);
                cmd.Parameters.AddWithValue("@CUST_ID", request.CUST_ID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    items.Add(new DeliverynoteDetail
                    {
                        SO_DETAIL_ID = dr["SO_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["SO_ID"]),
                        DN_DETAIL_ID = dr["ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ID"]),
                        ITEM_ID = dr["ITEM_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ITEM_ID"]),
                        REMARKS = dr["REMARKS"] == DBNull.Value ? null : dr["REMARKS"].ToString(),
                        MATRIX_CODE = dr["MATRIX_CODE"] == DBNull.Value ? null : dr["MATRIX_CODE"].ToString(),
                        UOM = dr["UOM"] == DBNull.Value ? null : dr["UOM"].ToString(),
                        DELIVERED_QUANTITY = dr["QUANTITY"] != DBNull.Value ? (double?)Convert.ToDouble(dr["QUANTITY"]) : null,
                        ITEM_CODE = dr["ITEM_CODE"] == DBNull.Value ? null : dr["ITEM_CODE"].ToString(),
                        DESCRIPTION = dr["DESCRIPTION"] == DBNull.Value ? null : dr["DESCRIPTION"].ToString(),
                        SIZE = dr["SIZE"] == DBNull.Value ? null : dr["SIZE"].ToString(),
                        COLOR = dr["COLOR"] == DBNull.Value ? null : dr["COLOR"].ToString(),
                        STYLE = dr["STYLE"] == DBNull.Value ? null : dr["STYLE"].ToString(),
                        SO_QTY = dr["SO_QTY"] == DBNull.Value ? (double?)null : Convert.ToDouble(dr["SO_QTY"]),
                        QTY_AVAILABLE = dr["QTY_AVAILABLE"] == DBNull.Value ? (double?)null : Convert.ToDouble(dr["QTY_AVAILABLE"])

                    });
                }
            }
            return items;
        }
        public DeliveryRtnListResponse GetDeliveryRtnList()
        {
            DeliveryRtnListResponse response = new DeliveryRtnListResponse();
            List<DeliveryRtnList> data = new List<DeliveryRtnList>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_DELIVERY_RETURN", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ACTION", 0);
                    cmd.Parameters.AddWithValue("@ID", 0);
                    //connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DeliveryRtnList item = new DeliveryRtnList
                            {
                                ID = dr["ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["ID"]) : null,
                                COMPANY_ID = dr["COMPANY_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["COMPANY_ID"]) : null,
                                STORE_ID = dr["STORE_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["STORE_ID"]) : null,
                                DR_DATE = dr["DR_DATE"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["DR_DATE"]) : null,
                                DR_NO = dr["DR_NO"] != DBNull.Value ? dr["DR_NO"].ToString() : null,
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
        public DeliveryRtnSelectResponse GetDeliveryRtnById(int id)
        {
            DeliveryRtnSelectResponse response = new DeliveryRtnSelectResponse();
            DeliveryRtnSelect deliveryNote = new DeliveryRtnSelect();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_DELIVERY_RETURN", connection))
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
                            deliveryNote = new DeliveryRtnSelect
                            {
                                ID = firstRow["ID"] != DBNull.Value ? (int?)Convert.ToInt32(firstRow["ID"]) : null,
                                COMPANY_ID = firstRow["COMPANY_ID"] != DBNull.Value ? (int?)Convert.ToInt32(firstRow["COMPANY_ID"]) : null,
                                STORE_ID = firstRow["STORE_ID"] != DBNull.Value ? (int?)Convert.ToInt32(firstRow["STORE_ID"]) : null,
                                DR_DATE = firstRow["DR_DATE"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(firstRow["DR_DATE"]) : null,
                                DR_NO = firstRow["DR_NO"] != DBNull.Value ? firstRow["DR_NO"].ToString() : null,
                                REF_NO = firstRow["REF_NO"] != DBNull.Value ? firstRow["REF_NO"].ToString() : null,
                                CUST_ID = firstRow["CUST_ID"] != DBNull.Value ? (int?)Convert.ToInt32(firstRow["CUST_ID"]) : null,
                                CONTACT_NAME = firstRow["CONTACT_NAME"] != DBNull.Value ? firstRow["CONTACT_NAME"].ToString() : null,
                                CONTACT_PHONE = firstRow["CONTACT_PHONE"] != DBNull.Value ? firstRow["CONTACT_PHONE"].ToString() : null,
                                CONTACT_FAX = firstRow["CONTACT_FAX"] != DBNull.Value ? firstRow["CONTACT_FAX"].ToString() : null,
                                CONTACT_MOBILE = firstRow["CONTACT_MOBILE"] != DBNull.Value ? firstRow["CONTACT_MOBILE"].ToString() : null,
                                SALESMAN_ID = firstRow["SALESMAN_ID"] != DBNull.Value ? (int?)Convert.ToInt32(firstRow["SALESMAN_ID"]) : null,
                                TOTAL_QTY = firstRow["TOTAL_QTY"] != DBNull.Value ? (double?)Convert.ToDouble(firstRow["TOTAL_QTY"]) : null,
                                TRANS_ID = firstRow["TRANS_ID"] != DBNull.Value ? (int?)Convert.ToInt32(firstRow["TRANS_ID"]) : null,
                                STATUS = firstRow["STATUS"] != DBNull.Value ? firstRow["STATUS"].ToString() : null,
                                NARRATION = firstRow["NARRATION"] != DBNull.Value ? firstRow["NARRATION"].ToString() : null,
                                DETAILS = new List<DeliveryRtnDetailSelect>()
                            };

                            foreach (DataRow dr in tbl.Rows)
                            {
                                var detail = new DeliveryRtnDetailSelect
                                {
                                    DETAIL_ID = dr["DETAIL_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["DETAIL_ID"]) : null,
                                    DN_DETAIL_ID = dr["DN_DETAIL_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["DN_DETAIL_ID"]) : null,
                                    SO_DETAIL_ID = dr["SO_DETAIL_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["SO_DETAIL_ID"]) : null,
                                    ITEM_ID = dr["ITEM_ID"] != DBNull.Value ? (int?)Convert.ToInt32(dr["ITEM_ID"]) : null,
                                    REMARKS = dr["REMARKS"] != DBNull.Value ? dr["REMARKS"].ToString() : null,
                                    UOM = dr["UOM"] != DBNull.Value ? dr["UOM"].ToString() : null,
                                    QUANTITY = dr["RETURN_QUANTITY"] != DBNull.Value ? (double?)Convert.ToDouble(dr["RETURN_QUANTITY"]) : null,
                                    ITEM_CODE = dr["ITEM_CODE"] != DBNull.Value ? dr["ITEM_CODE"].ToString() : null,
                                    DESCRIPTION = dr["DESCRIPTION"] != DBNull.Value ? dr["DESCRIPTION"].ToString() : null,
                                    DELIVERED_QUANTITY = dr["QUANTITY"] != DBNull.Value ? (double?)Convert.ToDouble(dr["QUANTITY"]) : null,
                                    QTY_AVAILABLE = dr["QTY_AVAILABLE"] == DBNull.Value ? (double?)null : Convert.ToDouble(dr["QTY_AVAILABLE"]),
                                    SIZE = dr["SIZE"] == DBNull.Value ? null : dr["SIZE"].ToString(),
                                    COLOR = dr["COLOR"] == DBNull.Value ? null : dr["COLOR"].ToString(),
                                    STYLE = dr["STYLE"] == DBNull.Value ? null : dr["STYLE"].ToString(),
                                    MATRIX_CODE = dr["MATRIX_CODE"] == DBNull.Value ? null : dr["MATRIX_CODE"].ToString(),
                                   
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
        public int Delete(int id, int userId)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_TB_DELIVERY_RETURN", connection, objtrans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@USER_ID", userId);
                    object result = cmd.ExecuteScalar();
                    Int32 deletedId = ADO.ToInt32(result);
                    objtrans.Commit();
                    return deletedId;
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
        public int Approve(Delivery_Return_Approve deliveryReturn)
        {
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add("SO_DETAIL_ID", typeof(Int32));
                    tbl.Columns.Add("DN_DETAIL_ID", typeof(Int32));
                    tbl.Columns.Add("ITEM_ID", typeof(Int32));
                    tbl.Columns.Add("REMARKS", typeof(string));
                    tbl.Columns.Add("UOM", typeof(string));
                    tbl.Columns.Add("QUANTITY", typeof(double));

                    if (deliveryReturn.DETAILS != null && deliveryReturn.DETAILS.Any())
                    {
                        foreach (var d in deliveryReturn.DETAILS)
                        {
                            DataRow dRow = tbl.NewRow();
                            dRow["SO_DETAIL_ID"] = d.SO_DETAIL_ID;
                            dRow["DN_DETAIL_ID"] = d.DN_DETAIL_ID;
                            dRow["ITEM_ID"] = d.ITEM_ID;
                            dRow["REMARKS"] = (object?)d.REMARKS ?? DBNull.Value;
                            dRow["UOM"] = d.UOM;
                            dRow["QUANTITY"] = d.QUANTITY;
                            tbl.Rows.Add(dRow);
                        }
                    }

                    SqlCommand cmd = new SqlCommand("SP_TB_DELIVERY_RETURN", connection, objtrans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 5);
                    cmd.Parameters.AddWithValue("@ID", deliveryReturn.ID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", deliveryReturn.COMPANY_ID);
                    cmd.Parameters.AddWithValue("@STORE_ID", deliveryReturn.STORE_ID);
                    cmd.Parameters.AddWithValue("@DR_DATE", (object?)deliveryReturn.DR_DATE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@REF_NO", (object?)deliveryReturn.REF_NO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CUST_ID", deliveryReturn.CUST_ID);
                    cmd.Parameters.AddWithValue("@CONTACT_NAME", (object?)deliveryReturn.CONTACT_NAME ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_PHONE", (object?)deliveryReturn.CONTACT_PHONE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_FAX", (object?)deliveryReturn.CONTACT_FAX ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CONTACT_MOBILE", (object?)deliveryReturn.CONTACT_MOBILE ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SALESMAN_ID", deliveryReturn.SALESMAN_ID);
                    cmd.Parameters.AddWithValue("@FIN_ID", deliveryReturn.FIN_ID);
                    cmd.Parameters.AddWithValue("@TOTAL_QTY", deliveryReturn.TOTAL_QTY);
                    cmd.Parameters.AddWithValue("@USER_ID", deliveryReturn.USER_ID);
                    cmd.Parameters.AddWithValue("@NARRATION", (object?)deliveryReturn.NARRATION ?? DBNull.Value);

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_TB_DR_DETAIL", tbl);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    object result = cmd.ExecuteScalar();
                    Int32 approvedId = ADO.ToInt32(result);
                    objtrans.Commit();
                    return approvedId;
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
    }
}
