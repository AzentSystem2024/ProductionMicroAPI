using MicroApi.DataLayer.Interface;
using System.Data;
using System.Data.SqlClient;
using MicroApi.Models;
using MicroApi.Helper;

namespace RetailApi.DAL.Services
{
    public class ClinicianService:IClinicianService
    {
        public List<Clinician> GetAllClinicians(int intUserID)
        {
            List<Clinician> clinicianList = new List<Clinician>();
            SqlConnection connection = ADO.GetConnection();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_CLINICIAN";
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("UserID", intUserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                // Use LINQ to convert DataTable to List<Clinician> without explicit loop
                clinicianList = tbl.AsEnumerable().Select(dr => new Clinician
                {
                    ID = ADO.ToInt32(dr["ID"]),
                    ClinicianLicense = ADO.ToString(dr["ClinicianLicense"]),
                    ClinicianName = ADO.ToString(dr["ClinicianName"]),
                    ClinicianShortName = ADO.ToString(dr["ClinicianShortName"]),
                    SpecialityID = ADO.ToInt32(dr["SpecialityID"]),
                    MajorID = ADO.ToInt32(dr["MajorID"]),
                    ProfessionID = ADO.ToInt32(dr["ProfessionID"]),
                    CategoryID = ADO.ToInt32(dr["CategoryID"]),
                    IsInactive = ADO.Toboolean(dr["IsInactive"]),
                    SpecialityName = ADO.ToString(dr["SpecialityName"]),
                    Major = ADO.ToString(dr["Major"]),
                    Profession = ADO.ToString(dr["Profession"]),
                    Category = ADO.ToString(dr["Category"]),
                    Gender = ADO.ToString(dr["Gender"]),
                    Department = ADO.ToString(dr["Department"])
                }).ToList();
            }
            finally
            {
                connection.Close();
            }

            return clinicianList;
        }


        //public List<Clinician> GetAllClinicians(Int32 intUserID)
        //{

        //    List<Clinician> ClinicianList = new List<Clinician>();
        //    SqlConnection connection = ADO.GetConnection();

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "SP_TB_CLINICIAN";
        //        cmd.Parameters.AddWithValue("ACTION", 0);
        //        cmd.Parameters.AddWithValue("UserID", intUserID);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable tbl = new DataTable();
        //        da.Fill(tbl);

        //        foreach (DataRow dr in tbl.Rows)
        //        {
        //            ClinicianList.Add(new Clinician
        //            {
        //                ID = ADO.ToInt32(dr["ID"]),
        //                ClinicianLicense =ADO.ToString(dr["ClinicianLicense"]),
        //                ClinicianName = ADO.ToString(dr["ClinicianName"]),
        //                ClinicianShortName = ADO.ToString(dr["ClinicianShortName"]),
        //                SpecialityID = ADO.ToInt32(dr["SpecialityID"]),
        //                MajorID = ADO.ToInt32(dr["MajorID"]),
        //                ProfessionID = ADO.ToInt32(dr["ProfessionID"]),
        //                CategoryID = ADO.ToInt32(dr["CategoryID"]),
        //                IsInactive = ADO.Toboolean(dr["IsInactive"]),
        //                SpecialityName = ADO.ToString(dr["SpecialityName"]),
        //                Major = ADO.ToString(dr["Major"]),
        //                Profession = ADO.ToString(dr["Profession"]),
        //                Category = ADO.ToString(dr["Category"]),
        //                Gender = ADO.ToString(dr["Gender"])
        //            });
        //        }
        //        connection.Close();

        //    return ClinicianList;
        //}
        public Int32 Insert(Clinician clinician, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CLINICIAN";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("ClinicianLicense", clinician.ClinicianLicense);
                    cmd.Parameters.AddWithValue("ClinicianName", clinician.ClinicianName);
                    cmd.Parameters.AddWithValue("ClinicianShortName", clinician.ClinicianShortName);
                    cmd.Parameters.AddWithValue("SpecialityID", clinician.SpecialityID);
                    cmd.Parameters.AddWithValue("MajorID", clinician.MajorID);
                    cmd.Parameters.AddWithValue("CategoryID", clinician.CategoryID);
                    cmd.Parameters.AddWithValue("ProfessionID", clinician.ProfessionID);
                    cmd.Parameters.AddWithValue("Gender", clinician.Gender);
                    cmd.Parameters.AddWithValue("UserID", userID);
                    cmd.Parameters.AddWithValue("DepartmentID", clinician.DepartmentID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_CLINICIAN";
                    Int32 clinicianID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return clinicianID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 Update(Clinician clinician, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CLINICIAN";
                    cmd.Parameters.AddWithValue("ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", clinician.ID);
                    cmd.Parameters.AddWithValue("ClinicianLicense", clinician.ClinicianLicense);
                    cmd.Parameters.AddWithValue("ClinicianName", clinician.ClinicianName);
                    cmd.Parameters.AddWithValue("ClinicianShortName", clinician.ClinicianShortName);
                    cmd.Parameters.AddWithValue("SpecialityID", clinician.SpecialityID);
                    cmd.Parameters.AddWithValue("MajorID", clinician.MajorID);
                    cmd.Parameters.AddWithValue("CategoryID", clinician.CategoryID);
                    cmd.Parameters.AddWithValue("ProfessionID", clinician.ProfessionID);
                    cmd.Parameters.AddWithValue("Gender", clinician.Gender);
                    cmd.Parameters.AddWithValue("UserID", userID);
                    cmd.Parameters.AddWithValue("DepartmentID", clinician.DepartmentID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_CLINICIAN";
                    Int32 clinicianID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return clinicianID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Clinician> GetItems(int id)
        {
            List<Clinician> ClinicianList = new List<Clinician>();
            try
            {
                string strSQL = "SELECT TB_CLINICIAN.ID, TB_CLINICIAN.ClinicianLicense, TB_CLINICIAN.ClinicianName, " +
                            "TB_CLINICIAN.ClinicianShortName, TB_CLINICIAN.SpecialityID, TB_CLINICIAN.Gender, " +
                            "TB_CLINICIAN.MajorID, TB_CLINICIAN.CategoryID, TB_CLINICIAN.ProfessionID, TB_CLINICIAN.IsInactive, " +
                            "TB_SPECIALITY.SpecialityName, TB_CLINICIAN_MAJOR.Major, TB_DEPARTMENT.ID AS DepartmentID," +
                            "TB_CLINICIAN_PROFESSION.Profession, TB_CLINICIAN_CATEGORY.Category " +
                            "FROM TB_CLINICIAN " +
                            "LEFT JOIN TB_SPECIALITY ON TB_CLINICIAN.SpecialityID = TB_SPECIALITY.ID " +
                            "LEFT JOIN TB_CLINICIAN_MAJOR ON TB_CLINICIAN.MajorID = TB_CLINICIAN_MAJOR.ID " +
                            "LEFT JOIN TB_CLINICIAN_PROFESSION ON TB_CLINICIAN.ProfessionID = TB_CLINICIAN_PROFESSION.ID " +
                            "LEFT JOIN TB_CLINICIAN_CATEGORY ON TB_CLINICIAN.CategoryID = TB_CLINICIAN_CATEGORY.ID " +
                            "LEFT JOIN TB_DEPARTMENT ON TB_CLINICIAN.DepartmentID = TB_DEPARTMENT.ID " +
                            "WHERE TB_CLINICIAN.ID = " + id;


                DataTable tbl = ADO.GetDataTable(strSQL, "Clinician");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    ClinicianList.Add(new Clinician
                    {
                        ID = dr["ID"] == DBNull.Value ? (int)0 : Convert.ToInt32(dr["ID"]),
                        ClinicianLicense = dr["ClinicianLicense"] == DBNull.Value ? null : Convert.ToString(dr["ClinicianLicense"]),
                        ClinicianName = dr["ClinicianName"] == DBNull.Value ? null : Convert.ToString(dr["ClinicianName"]),
                        ClinicianShortName = dr["ClinicianShortName"] == DBNull.Value ? null : Convert.ToString(dr["ClinicianShortName"]),
                        SpecialityID = dr["SpecialityID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["SpecialityID"]),
                        MajorID = dr["MajorID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["MajorID"]),
                        ProfessionID = dr["ProfessionID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ProfessionID"]),
                        CategoryID = dr["CategoryID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CategoryID"]),
                        IsInactive = dr["IsInactive"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dr["IsInactive"]),
                        SpecialityName = dr["SpecialityName"] == DBNull.Value ? null : Convert.ToString(dr["SpecialityName"]),
                        Major = dr["Major"] == DBNull.Value ? null : Convert.ToString(dr["Major"]),
                        Profession = dr["Profession"] == DBNull.Value ? null : Convert.ToString(dr["Profession"]),
                        Category = dr["Category"] == DBNull.Value ? null : Convert.ToString(dr["Category"]),
                        Gender = dr["Gender"] == DBNull.Value ? null : Convert.ToString(dr["Gender"]),
                        DepartmentID = dr["DepartmentID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["DepartmentID"]),
                    });

                }
            }
            catch (Exception ex)
            {

            }

            return ClinicianList;
        }
        public bool DeleteClinicians(int Id, int userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CLINICIAN";
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
