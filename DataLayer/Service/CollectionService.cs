using MicroApi.Helper;
using MicroApi.DataLayer.Interface;
using System.Data;
using System.Data.SqlClient;
using MicroApi.Models;

namespace RetailApi.DAL.Services
{
    public class CollectionService:ICollectionService
    {
        public CollectionResponse Insert(Collection collection)
        {
            List<Collection> lst = new List<Collection>();
            CollectionResponse res = new CollectionResponse();
            SqlConnection connection = new SqlConnection();
            try
            {

                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_COLLECTION";
                cmd.Parameters.AddWithValue("ACTION", 1);

                cmd.Parameters.AddWithValue("@COLLECTION_DATE", collection.COLLECTION_DATE);
                cmd.Parameters.AddWithValue("@COLLECTION_TIME", collection.COLLECTION_TIME);
                cmd.Parameters.AddWithValue("@REFERENCE_NO", collection.REFERENCE_NO);
                cmd.Parameters.AddWithValue("@PATIENT_NAME", collection.PATIENT_NAME);
                cmd.Parameters.AddWithValue("@AGE", collection.AGE);
                cmd.Parameters.AddWithValue("@SEX", collection.SEX);
                cmd.Parameters.AddWithValue("@HOSPITAL_ID", collection.HOSPITAL_ID);
                cmd.Parameters.AddWithValue("@UNIT_NAME", collection.UNIT_NAME);
                cmd.Parameters.AddWithValue("@WARD", collection.WARD);
                cmd.Parameters.AddWithValue("@UHID", collection.UHID);
                cmd.Parameters.AddWithValue("@INCOME", collection.INCOME);
                cmd.Parameters.AddWithValue("@SPECIMEN", collection.SPECIMEN);
                cmd.Parameters.AddWithValue("@DESCRIPTION", collection.DESCRIPTION);
                cmd.Parameters.AddWithValue("@DIAGNOSIS", collection.DIAGNOSIS);
                cmd.Parameters.AddWithValue("@ANTIBIOTIC_PRESENT", collection.ANTIBIOTIC_PRESENT);
                cmd.Parameters.AddWithValue("@ANTIBIOTIC_PAST", collection.ANTIBIOTIC_PAST);
                cmd.Parameters.AddWithValue("@INVESTIGATION_ID", collection.INVESTIGATION_ID);
                cmd.Parameters.AddWithValue("@INVESTIGATION_NAME", collection.INVESTIGATION_NAME);
                cmd.Parameters.AddWithValue("PREVIOUS_RESULT", collection.PREVIOUS_RESULT);
                cmd.Parameters.AddWithValue("@OTHERS", collection.OTHERS);
                cmd.Parameters.AddWithValue("@USER_ID", collection.USER_ID);
                cmd.Parameters.AddWithValue("@DOCTOR_NAME", collection.DOCTOR_NAME);
                cmd.Parameters.AddWithValue("@DOCTOR_MOBILE", collection.DOCTOR_MOBILE);

                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = connection;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT MAX(COLLECTION_NO) FROM TB_COLLECTION";
                Int32 CollectionNo = Convert.ToInt32(cmd1.ExecuteScalar());

                Collection col = new Collection();
                col.COLLECTION_NO = CollectionNo;


                lst.Add(col);

                res.flag = 1;
                res.Message = "sucess";
                res.CollectionData = lst.ToList();


            }
            catch (Exception ex)
            {
                res.flag = 1;
                res.Message = ex.Message;
                res.CollectionData = lst.ToList();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return res;
        }
        public CollectionResponse NextCollectionNo()
        {
            List<Collection> lst = new List<Collection>();
            CollectionResponse res = new CollectionResponse();
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT MAX(COLLECTION_NO) FROM TB_COLLECTION";
                Int32 CollectionNo = Convert.ToInt32(cmd.ExecuteScalar()) + 1;

                Collection col = new Collection();
                col.COLLECTION_NO = CollectionNo;


                lst.Add(col);

                res.flag = 1;
                res.Message = "sucess";
                res.CollectionData = lst.ToList();

            }
            catch (Exception ex)
            {
                res.flag = 1;
                res.Message = ex.Message;
                res.CollectionData = lst.ToList();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return res;
        }
        public CollectionResponse Update(Collection collection)
        {
            List<Collection> lst = new List<Collection>();
            CollectionResponse res = new CollectionResponse();
            SqlConnection connection = new SqlConnection();

            try
            {
                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_COLLECTION";
                cmd.Parameters.AddWithValue("ACTION", 2);

                cmd.Parameters.AddWithValue("@ID", collection.ID);
                cmd.Parameters.AddWithValue("@COLLECTION_DATE", collection.COLLECTION_DATE);
                cmd.Parameters.AddWithValue("@COLLECTION_TIME", collection.COLLECTION_TIME);
                cmd.Parameters.AddWithValue("@REFERENCE_NO", collection.REFERENCE_NO);
                cmd.Parameters.AddWithValue("@PATIENT_NAME", collection.PATIENT_NAME);
                cmd.Parameters.AddWithValue("@AGE", collection.AGE);
                cmd.Parameters.AddWithValue("@SEX", collection.SEX);
                cmd.Parameters.AddWithValue("@HOSPITAL_ID", collection.HOSPITAL_ID);
                cmd.Parameters.AddWithValue("@UNIT_NAME", collection.UNIT_NAME);
                cmd.Parameters.AddWithValue("@WARD", collection.WARD);
                cmd.Parameters.AddWithValue("@UHID", collection.UHID);
                cmd.Parameters.AddWithValue("@INCOME", collection.INCOME);
                cmd.Parameters.AddWithValue("@SPECIMEN", collection.SPECIMEN);
                cmd.Parameters.AddWithValue("@DESCRIPTION", collection.DESCRIPTION);
                cmd.Parameters.AddWithValue("@DIAGNOSIS", collection.DIAGNOSIS);
                cmd.Parameters.AddWithValue("@ANTIBIOTIC_PRESENT", collection.ANTIBIOTIC_PRESENT);
                cmd.Parameters.AddWithValue("@ANTIBIOTIC_PAST", collection.ANTIBIOTIC_PAST);
                cmd.Parameters.AddWithValue("@INVESTIGATION_ID", collection.INVESTIGATION_ID);
                cmd.Parameters.AddWithValue("@INVESTIGATION_NAME", collection.INVESTIGATION_NAME);
                cmd.Parameters.AddWithValue("PREVIOUS_RESULT", collection.PREVIOUS_RESULT);
                cmd.Parameters.AddWithValue("@OTHERS", collection.OTHERS);
                cmd.Parameters.AddWithValue("@USER_ID", collection.USER_ID);
                cmd.Parameters.AddWithValue("@DOCTOR_NAME", collection.DOCTOR_NAME);
                cmd.Parameters.AddWithValue("@DOCTOR_MOBILE", collection.DOCTOR_MOBILE);

                cmd.ExecuteNonQuery();



                lst.Add(collection);

                res.flag = 1;
                res.Message = "sucess";
                res.CollectionData = lst.ToList();


            }
            catch (Exception ex)
            {
                res.flag = 1;
                res.Message = ex.Message;
                res.CollectionData = lst.ToList();
            }
            return res;
        }
        public MasterResponse MasterData()
        {
            MasterResponse response = new MasterResponse();
            response.Hospitals = new List<Hospital>();
            response.Sex = new List<Sex>();
            response.investigations = new List<Investigation>();

            SqlConnection connection = ADO.GetConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_TB_LIST";
            cmd.Parameters.AddWithValue("ACTION", 2);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            // Hostpital data
            //DataTable tbl = ds.Tables[0];
            //foreach (DataRow dr in tbl.Rows)
            //{
            //    response.Hospitals.Add(new Hospital
            //    {
            //        ID = ADO.ToInt32(dr["ID"]),
            //        HOSPITAL = ADO.ToString(dr["HOSPITAL"]),
            //    });
            //}

            //  sex data
            if (ds.Tables.Count > 1)
            {
                DataTable tbl1 = ds.Tables[1];
                foreach (DataRow dr1 in tbl1.Rows)
                {
                    response.Sex.Add(new Sex
                    {
                        DESCRIPTION = ADO.ToString(dr1["DESCRIPTION"]),
                    });
                }
            }

            //Investigation data
            if (ds.Tables.Count > 2)
            {
                DataTable tbl2 = ds.Tables[2];
                foreach (DataRow dr2 in tbl2.Rows)
                {
                    response.investigations.Add(new Investigation
                    {
                        ID = ADO.ToInt32(dr2["ID"]),
                        INVESTIGATION = ADO.ToString(dr2["INVESTIGATION"]),
                    });
                }
            }
            response.flag = 1;
            response.message = "Success";

            return response;
        }
        public List<Collection> GetCollection(CollectionInput collectionInput)
        {
            List<Collection> versionList = new List<Collection>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GET_COLLECTION_VIEW";

                // Handle null DATE_FROM and DATE_TO
                DateTime defaultDateFrom = new DateTime(2000, 1, 1);
                DateTime defaultDateTo = DateTime.Today;

                cmd.Parameters.AddWithValue("@USER_ID", collectionInput.USER_ID);
                cmd.Parameters.AddWithValue("@DATE_FROM", collectionInput.DATE_FROM ?? defaultDateFrom);
                cmd.Parameters.AddWithValue("@DATE_TO", collectionInput.DATE_TO ?? defaultDateTo);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                // Convert DataTable to List<Collection>
                versionList = tbl.AsEnumerable().Select(dr => new Collection
                {
                    ID = ADO.ToInt32(dr["ID"]),
                    COLLECTION_NO = ADO.ToInt32(dr["COLLECTION_NO"]),
                    PATIENT_NAME = ADO.ToString(dr["PATIENT_NAME"]),
                    COLLECTION_DATE = Convert.ToDateTime(dr["COLLECTION_DATE"]),
                    COLLECTION_TIME = Convert.ToDateTime(dr["COLLECTION_TIME"]),
                    WARD = ADO.ToString(dr["WARD"]),
                    UNIT_NAME = ADO.ToString(dr["UNIT_NAME"]),
                    UHID = ADO.ToString(dr["UHID"]),
                    INVESTIGATION_ID = ADO.ToInt32(dr["INVESTIGATION_ID"]),
                    REFERENCE_NO = ADO.ToString(dr["REFERENCE_NO"]),
                    REPORT_ID = ADO.ToInt32(dr["REPORT_ID"]),
                    STATUS_ID = ADO.ToInt32(dr["STATUS_ID"]),
                    STATUS_NAME = ADO.ToString(dr["STATUS_NAME"]),
                    DOCTOR_NAME = ADO.ToString(dr["DOCTOR_NAME"]),
                    INVESTIGATION_NAME = ADO.ToString(dr["INVESTIGATION_NAME"]),
                    HOSPITAL_NAME = ADO.ToString(dr["HOSPITAL"]),
                    SEX = ADO.ToString(dr["SEX"]),
                    SPECIMEN = ADO.ToString(dr["SPECIMEN"]),
                    DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                    DIAGNOSIS = ADO.ToString(dr["DIAGNOSIS"]),
                    ANTIBIOTIC_PRESENT = ADO.ToString(dr["ANTIBIOTIC_PRESENT"]),
                    ANTIBIOTIC_PAST = ADO.ToString(dr["ANTIBIOTIC_PAST"]),
                    OTHERS = ADO.ToString(dr["OTHERS"]),
                    USER_ID = ADO.ToInt32(dr["USER_ID"]),
                    USER_NAME = ADO.ToString(dr["USER_NAME"]),
                    INCOME = ADO.ToDecimal(dr["INCOME"]),
                    AGE = ADO.ToInt32(dr["AGE"]),
                    DOCTOR_MOBILE = ADO.ToString(dr["DOCTOR_MOBILE"]),

                }).ToList();
            }
            finally
            {
                connection.Close();
            }
            return versionList;
        }

        public CollectionDownloadOutput DownloadCollection(CollectionDownloadInput vInput)
        {
            CollectionDownloadOutput vOutput = new CollectionDownloadOutput();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();
            List<CollectionDownload> vList = new List<CollectionDownload>();
            try
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM TB_COLLECTION WHERE ID > " + vInput.ID;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                // Convert DataTable to List<Collection>
                if (tbl.Rows.Count > 0)
                {
                    vList = tbl.AsEnumerable().Select(dr => new CollectionDownload
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        COLLECTION_NO = ADO.ToInt32(dr["COLLECTION_NO"]),
                        COLLECTION_DATE = Convert.ToDateTime(dr["COLLECTION_DATE"]),
                        COLLECTION_TIME = Convert.ToDateTime(dr["COLLECTION_TIME"]),
                        REFERENCE_NO = ADO.ToString(dr["REFERENCE_NO"]),
                        PATIENT_NAME = ADO.ToString(dr["PATIENT_NAME"]),
                        AGE = ADO.ToInt32(dr["AGE"]),
                        SEX = ADO.ToString(dr["SEX"]),
                        HOSPITAL_ID = ADO.ToInt32(dr["HOSPITAL_ID"]),
                        DEPT_ID = ADO.ToInt32(dr["DEPT_ID"]),
                        UNIT_NAME = ADO.ToString(dr["UNIT_NAME"]),
                        WARD = ADO.ToString(dr["WARD"]),
                        UHID = ADO.ToString(dr["UHID"]),
                        INCOME = ADO.ToDecimal(dr["INCOME"]),
                        SPECIMEN = ADO.ToString(dr["SPECIMEN"]),
                        DOCTOR_MOBILE = ADO.ToString(dr["DOCTOR_MOBILE"]),
                        DOCTOR_NAME = ADO.ToString(dr["DOCTOR_NAME"]),
                        DESCRIPTION = ADO.ToString(dr["DESCRIPTION"]),
                        DIAGNOSIS = ADO.ToString(dr["DIAGNOSIS"]),
                        ANTIBIOTIC_PRESENT = ADO.ToString(dr["ANTIBIOTIC_PRESENT"]),
                        ANTIBIOTIC_PAST = ADO.ToString(dr["ANTIBIOTIC_PAST"]),
                        INVESTIGATION_ID = ADO.ToInt32(dr["INVESTIGATION_ID"]),
                        INVESTIGATION_NAME = ADO.ToString(dr["INVESTIGATION_NAME"]),
                        PREVIOUS_RESULT = ADO.ToString(dr["PREVIOUS_RESULT"]),
                        OTHERS = ADO.ToString(dr["OTHERS"]),
                        USER_ID = ADO.ToInt32(dr["USER_ID"]),
                        USER_TIME = Convert.ToDateTime(dr["USER_TIME"])

                    }).ToList();
                    vOutput.flag = 1;
                    vOutput.message = "success";
                    vOutput.data = vList;
                }
                else
                {
                    vOutput.flag = 2;
                    vOutput.message = "success. nothing to download";
                    vOutput.data = vList;
                }
            }
            catch (Exception ex)
            {
                vOutput.flag = 0;
                vOutput.message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return vOutput;
        }
        public PendingCollectionResponse GetPendingCollection()
        {
            PendingCollectionResponse pendingCollection = new PendingCollectionResponse();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr = null;

            try
            {
                con = ADO.GetConnection();
                pendingCollection.PendingCollectionData = new List<PendingCollection>();

                string str = "SELECT TB_COLLECTION.ID, COLLECTION_NO, COLLECTION_DATE, REFERENCE_NO, PATIENT_NAME,AGE,SEX,UNIT_NAME,WARD,UHID,DOCTOR_NAME, " +
                             "HOSPITAL, INVESTIGATION_NAME, SPECIMEN FROM TB_COLLECTION INNER JOIN TB_HOSPITAL ON TB_COLLECTION.HOSPITAL_ID = TB_HOSPITAL.ID " +
                             " LEFT JOIN TB_REPORT ON TB_COLLECTION.ID = TB_REPORT.COLLECTION_ID WHERE TB_REPORT.ID IS NULL ORDER BY TB_COLLECTION.ID ";

                cmd = new SqlCommand(str, con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    pendingCollection.PendingCollectionData.Add(new PendingCollection
                    {
                        ID = Convert.ToInt32(rdr["ID"]),
                        COLLECTION_NO = Convert.ToInt32(rdr["COLLECTION_NO"]),
                        COLLECTION_DATE = Convert.ToDateTime(rdr["COLLECTION_DATE"]),
                        REFERENCE_NO = rdr["REFERENCE_NO"].ToString(),
                        PATIENT_NAME = rdr["PATIENT_NAME"].ToString(),
                        AGE = Convert.ToInt32(rdr["AGE"]),
                        SEX = rdr["SEX"].ToString(),
                        UNIT_NAME = rdr["UNIT_NAME"].ToString(),
                        WARD = rdr["WARD"].ToString(),
                        UHID = rdr["UHID"].ToString(),
                        DOCTOR_NAME = rdr["DOCTOR_NAME"].ToString(),
                        HOSPITAL = rdr["HOSPITAL"].ToString(),
                        INVESTIGATION_NAME = rdr["INVESTIGATION_NAME"].ToString(),
                        SPECIMEN = rdr["SPECIMEN"].ToString(),
                    });
                }

                rdr.Close();

                pendingCollection.flag = 1;
                pendingCollection.Message = "Success";
            }
            catch (Exception ex)
            {
                pendingCollection.flag = 0;
                pendingCollection.Message = ex.Message;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }
            return pendingCollection;
        }
    }
}
