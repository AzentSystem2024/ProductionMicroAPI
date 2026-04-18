using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;
using System.Data;
using MicroApi.DataLayer.Interface;

public class MiscPurchInvoiceService : IMiscPurchInvoiceService
{
    public Int32 Insert(MiscPurchInvoice model)
    {
        SqlConnection connection = ADO.GetConnection();
        SqlTransaction trans = connection.BeginTransaction();

        try
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add("COMPANY_ID", typeof(int));
            tbl.Columns.Add("STORE_ID", typeof(int));
            tbl.Columns.Add("VAT_PERC", typeof(int));
            tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
            tbl.Columns.Add("AMOUNT", typeof(decimal));
            tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
            tbl.Columns.Add("HEAD_ID", typeof(int));
            tbl.Columns.Add("REMARKS", typeof(string));

            if (model.Details != null && model.Details.Any())
            {
                foreach (var item in model.Details)
                {
                    DataRow row = tbl.NewRow();

                    row["COMPANY_ID"] = item.COMPANY_ID ?? (object)DBNull.Value;
                    row["STORE_ID"] = item.STORE_ID ?? (object)DBNull.Value;
                    row["VAT_PERC"] = item.VAT_PERC ?? (object)DBNull.Value;
                    row["VAT_AMOUNT"] = item.VAT_AMOUNT ?? (object)DBNull.Value;
                    row["AMOUNT"] = item.AMOUNT ?? (object)DBNull.Value;
                    row["TOTAL_AMOUNT"] = item.TOTAL_AMOUNT ?? (object)DBNull.Value;
                    row["HEAD_ID"] = item.HEAD_ID ?? (object)DBNull.Value;
                    row["REMARKS"] = item.REMARKS ?? (object)DBNull.Value;

                    tbl.Rows.Add(row);
                }
            }

            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                Transaction = trans,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_MISC_PURCH"
            };

            cmd.Parameters.AddWithValue("@ACTION", 1);
            cmd.Parameters.AddWithValue("@ID", model.ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@PURCH_DATE", model.PURCH_DATE ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPP_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@PURCH_TYPE", model.PURCH_TYPE ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@IS_APPROVED", model.IS_APPROVED == true ? 1 : 0);
            cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? (object)DBNull.Value);

            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_MISC_PURCH_DETAIL", tbl);
            tvpParam.SqlDbType = SqlDbType.Structured;

            cmd.ExecuteNonQuery();

            SqlCommand getIdCmd = new SqlCommand("SELECT MAX(TRANS_ID) FROM TB_PURCH_HEADER", connection, trans);
            int id = ADO.ToInt32(getIdCmd.ExecuteScalar());

            trans.Commit();
            return id;
        }
        catch
        {
            trans.Rollback();
            throw;
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }

    public Int32 Update(MiscPurchInvoice model)
    {
        SqlConnection connection = ADO.GetConnection();
        SqlTransaction trans = connection.BeginTransaction();

        try
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add("COMPANY_ID", typeof(int));
            tbl.Columns.Add("STORE_ID", typeof(int));
            tbl.Columns.Add("VAT_PERC", typeof(int));
            tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
            tbl.Columns.Add("AMOUNT", typeof(decimal));
            tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
            tbl.Columns.Add("HEAD_ID", typeof(int));
            tbl.Columns.Add("REMARKS", typeof(string));

            if (model.Details != null && model.Details.Any())
            {
                foreach (var item in model.Details)
                {
                    DataRow row = tbl.NewRow();

                    row["COMPANY_ID"] = item.COMPANY_ID ?? (object)DBNull.Value;
                    row["STORE_ID"] = item.STORE_ID ?? (object)DBNull.Value;
                    row["VAT_PERC"] = item.VAT_PERC ?? (object)DBNull.Value;
                    row["VAT_AMOUNT"] = item.VAT_AMOUNT ?? (object)DBNull.Value;
                    row["AMOUNT"] = item.AMOUNT ?? (object)DBNull.Value;
                    row["TOTAL_AMOUNT"] = item.TOTAL_AMOUNT ?? (object)DBNull.Value;
                    row["HEAD_ID"] = item.HEAD_ID ?? (object)DBNull.Value;
                    row["REMARKS"] = item.REMARKS ?? (object)DBNull.Value;

                    tbl.Rows.Add(row);
                }
            }

            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                Transaction = trans,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_MISC_PURCH"
            };

            cmd.Parameters.AddWithValue("@ACTION", 2);

            cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@PURCH_DATE", model.PURCH_DATE ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPP_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@PURCH_TYPE", model.PURCH_TYPE ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@REF_NO", model.REF_NO ?? (object)DBNull.Value);

            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_MISC_PURCH_DETAIL", tbl);
            tvpParam.SqlDbType = SqlDbType.Structured;

            cmd.ExecuteNonQuery();

            SqlCommand getIdCmd = new SqlCommand("SELECT MAX(TRANS_ID) FROM TB_PURCH_HEADER", connection, trans);
            int id = ADO.ToInt32(getIdCmd.ExecuteScalar());

            trans.Commit();
            return id;
        }
        catch
        {
            trans.Rollback();
            throw;
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

    }
    public Int32 Commit(MiscPurchInvoice model)
    {
        SqlConnection connection = ADO.GetConnection();
        SqlTransaction trans = connection.BeginTransaction();

        try
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add("COMPANY_ID", typeof(int));
            tbl.Columns.Add("STORE_ID", typeof(int));
            tbl.Columns.Add("VAT_PERC", typeof(int));
            tbl.Columns.Add("VAT_AMOUNT", typeof(decimal));
            tbl.Columns.Add("AMOUNT", typeof(decimal));
            tbl.Columns.Add("TOTAL_AMOUNT", typeof(decimal));
            tbl.Columns.Add("HEAD_ID", typeof(int));
            tbl.Columns.Add("REMARKS", typeof(string));

            if (model.Details != null && model.Details.Any())
            {
                foreach (var item in model.Details)
                {
                    DataRow row = tbl.NewRow();

                    row["COMPANY_ID"] = item.COMPANY_ID ?? (object)DBNull.Value;
                    row["STORE_ID"] = item.STORE_ID ?? (object)DBNull.Value;
                    row["VAT_PERC"] = item.VAT_PERC ?? (object)DBNull.Value;
                    row["VAT_AMOUNT"] = item.VAT_AMOUNT ?? (object)DBNull.Value;
                    row["AMOUNT"] = item.AMOUNT ?? (object)DBNull.Value;
                    row["TOTAL_AMOUNT"] = item.TOTAL_AMOUNT ?? (object)DBNull.Value;
                    row["HEAD_ID"] = item.HEAD_ID ?? (object)DBNull.Value;
                    row["REMARKS"] = item.REMARKS ?? (object)DBNull.Value;

                    tbl.Rows.Add(row);
                }
            }

            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                Transaction = trans,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_MISC_PURCH"
            };

            cmd.Parameters.AddWithValue("@ACTION", 6);

            cmd.Parameters.AddWithValue("@TRANS_ID", model.TRANS_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@STORE_ID", model.STORE_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@PURCH_DATE", model.PURCH_DATE ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@SUPP_ID", model.SUPP_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FIN_ID", model.FIN_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@PURCH_TYPE", model.PURCH_TYPE ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@GROSS_AMOUNT", model.GROSS_AMOUNT ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@TAX_AMOUNT", model.TAX_AMOUNT ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NET_AMOUNT", model.NET_AMOUNT ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@USER_ID", model.USER_ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NARRATION", model.NARRATION ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@IS_APPROVED", model.IS_APPROVED == true ? 1 : 0);

            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@UDT_MISC_PURCH_DETAIL", tbl);
            tvpParam.SqlDbType = SqlDbType.Structured;

            cmd.ExecuteNonQuery();

            SqlCommand getIdCmd = new SqlCommand("SELECT MAX(TRANS_ID) FROM TB_PURCH_HEADER", connection, trans);
            int id = ADO.ToInt32(getIdCmd.ExecuteScalar());

            trans.Commit();
            return id;
        }
        catch
        {
            trans.Rollback();
            throw;
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

    }
    public List<MiscPurchList> GetMiscPurchList(PurchListRequest request)
    {
        List<MiscPurchList> list = new List<MiscPurchList>();

        using (SqlConnection connection = ADO.GetConnection())
        using (SqlCommand cmd = new SqlCommand("SP_MISC_PURCH", connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ACTION", 0);
            cmd.Parameters.AddWithValue("@COMPANY_ID", request.COMPANY_ID);
            cmd.Parameters.AddWithValue("@TRANS_TYPE", 43);
            cmd.Parameters.AddWithValue("@DATE_FROM", request.DATE_FROM ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@DATE_TO", request.DATE_TO ?? (object)DBNull.Value);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new MiscPurchList
                {
                    ID = ADO.ToInt32(dr["ID"]),
                    TRANS_ID = ADO.ToInt32(dr["TRANS_ID"]),
                    PURCH_NO = ADO.ToString(dr["PURCH_NO"]),
                    PURCH_DATE = dr["PURCH_DATE"] != DBNull.Value ? Convert.ToDateTime(dr["PURCH_DATE"]) : null,
                    SUPP_ID = ADO.ToInt32(dr["SUPP_ID"]),
                    STORE_ID = ADO.ToInt32(dr["STORE_ID"]),
                    SUPP_NAME = ADO.ToString(dr["SUPP_NAME"]),
                    STORE = ADO.ToString(dr["STORE"]),
                    NET_AMOUNT = ADO.ToFloat(dr["NET_AMOUNT"]),
                    NARRATION = ADO.ToString(dr["NARRATION"]),
                    STATUS = ADO.ToString(dr["STATUS"]),
                    PO_NO = ADO.ToString(dr["PO_NO"]),
                });
            }
        }

        return list;
    }
    public MiscPurchInvoice GetMiscPurchById(int Id)
    {
        MiscPurchInvoice invoice = new MiscPurchInvoice();

        using (SqlConnection connection = ADO.GetConnection())
        using (SqlCommand cmd = new SqlCommand("SP_MISC_PURCH", connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ACTION", 0);
            cmd.Parameters.AddWithValue("@TRANS_ID", Id);


            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    invoice = new MiscPurchInvoice
                    {
                        ID = ADO.ToInt32(reader["ID"]),
                        TRANS_ID = ADO.ToInt32(reader["TRANS_ID"]),
                        PURCH_DATE = reader["PURCH_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["PURCH_DATE"]) : null,
                        SUPP_ID = ADO.ToInt32(reader["SUPP_ID"]),
                        STORE_ID = ADO.ToInt32(reader["STORE_ID"]),
                        FIN_ID = ADO.ToInt32(reader["FIN_ID"]),
                        PURCH_TYPE = reader["PURCH_TYPE"] != DBNull.Value ? Convert.ToInt16(reader["PURCH_TYPE"]) : (short?)null,
                        GROSS_AMOUNT = ADO.ToDecimal(reader["GROSS_AMOUNT"]),
                        TAX_AMOUNT = reader["TAX_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TAX_AMOUNT"]) : 0,
                        NET_AMOUNT = ADO.ToDecimal(reader["NET_AMOUNT"]),
                        NARRATION = ADO.ToString(reader["NARRATION"]),
                        REF_NO = ADO.ToString(reader["REF_NO"]),
                        PURCH_NO = ADO.ToString(reader["PURCH_NO"]),
                        TRANS_STATUS = ADO.ToInt32(reader["TRANS_STATUS"]),
                        Details = new List<MiscPurchDetail>()
                    };
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        invoice.Details.Add(new MiscPurchDetail
                        {
                            COMPANY_ID = ADO.ToInt32(reader["COMPANY_ID"]),
                            STORE_ID = ADO.ToInt32(reader["STORE_ID"]),
                            HEAD_ID = ADO.ToInt32(reader["HEAD_ID"]),
                            VAT_PERC = reader["VAT_PERC"] != DBNull.Value ? Convert.ToInt32(reader["VAT_PERC"]) : 0,
                            VAT_AMOUNT = reader["VAT_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["VAT_AMOUNT"]) : 0,
                            AMOUNT = reader["AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["AMOUNT"]) : 0,
                            TOTAL_AMOUNT = reader["TOTAL_AMOUNT"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL_AMOUNT"]) : 0,
                            REMARKS = reader["REMARKS"] != DBNull.Value ? Convert.ToString(reader["REMARKS"]) : null
                        });
                    }
                }
            }
        }

        return invoice;
    }
    public MiscPurchResponse Delete(int id)
    {
        MiscPurchResponse res = new MiscPurchResponse();

        try
        {
            using (var connection = ADO.GetConnection())
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                string procedureName = "SP_MISC_PURCH";

                using (var cmd = new SqlCommand(procedureName, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 4);
                    cmd.Parameters.AddWithValue("@TRANS_ID", id);

                    int rowsAffected = cmd.ExecuteNonQuery();


                }

            }
            res.Flag = 1;
            res.Message = "Success";
        }
        catch (Exception ex)
        {
            res.Flag = 0;
            res.Message = "Error: " + ex.Message;
        }

        return res;
    }

}
