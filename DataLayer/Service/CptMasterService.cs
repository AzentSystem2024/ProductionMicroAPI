using MicroApi.DataLayer.Interface;
using System.Data;
using System.Data.SqlClient;
using MicroApi.Models;
using MicroApi.Helper;

namespace MicroApi.DataLayer.Service
{
    public class CptMasterService:ICptMasterService
    {
        public List<CptMaster> GetAllCptMasters(int intUserID)
        {
            List<CptMaster> cptmenuList;

            SqlConnection connection = ADO.GetConnection();

            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_TB_CPT_MASTER",
                CommandTimeout = 0
            };
            cmd.Parameters.AddWithValue("ACTION", 0);
            cmd.Parameters.AddWithValue("UserID", intUserID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);

            // Use LINQ to convert DataTable to List<CptMaster> without explicit loop
            cptmenuList = tbl.AsEnumerable().Select(dr => new CptMaster
            {
                ID = Convert.ToInt32(dr["ID"]),
                CPTTypeID = Convert.ToInt32(dr["CPTTypeID"]),
                CPTType = Convert.ToString(dr["CPTType"]),
                CPTCode = Convert.ToString(dr["CPTCode"]),
                Description = Convert.ToString(dr["Description"]),
                CPTName = Convert.ToString(dr["CPTName"]),
                IsInactive = Convert.ToBoolean(dr["IsInactive"]),
                CPTGroup = Convert.ToString(dr["CPTGroup"]),
                Department = Convert.ToString(dr["Department"]),
                CPTDepartment = Convert.ToString(dr["CPTDepartment"]),
                CostDepartment = Convert.ToString(dr["CostDepartment"]),
                FixedQuantity = Convert.ToSingle(dr["FixedQuantity"])

                //RVU = dr["RVU"] == DBNull.Value ? 0 : ADO.ToFloat(dr["RVU"])
                // Uncomment the line below if you need to include IsDeleted in the model
                // IsDeleted = Convert.ToBoolean(dr["IsDeleted"])
            }).ToList();

            return cptmenuList;
        }

        public Int32 Insert(CptMaster cptmaster, Int32 userID)
        {
            CptMaster res = new CptMaster();
            res.data = new List<costSettingsData>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CPT_MASTER";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("CPTTypeID", cptmaster.CPTTypeID);
                    cmd.Parameters.AddWithValue("CPTCode", cptmaster.CPTCode);
                    //cmd.Parameters.AddWithValue("CPTShortName", cptmaster.CPTShortName);
                    cmd.Parameters.AddWithValue("CPTName", cptmaster.CPTName);
                    cmd.Parameters.AddWithValue("Description", cptmaster.Description);
                    cmd.Parameters.AddWithValue("IsInactive", cptmaster.IsInactive);
                    cmd.Parameters.AddWithValue("UserID", userID);
                    cmd.Parameters.AddWithValue("CPTGroup", cptmaster.CPTGroup);
                    cmd.Parameters.AddWithValue("DepartmentID", cptmaster.DepartmentID);
                    cmd.Parameters.AddWithValue("CPTDepartmentID", cptmaster.CPTDepartmentID);
                    cmd.Parameters.AddWithValue("CostDepartmentID", cptmaster.CostDepartmentID);
                    cmd.Parameters.AddWithValue("CostDriveID", cptmaster.CostDriveID);
                    cmd.Parameters.AddWithValue("FixedQuantity", cptmaster.FixedQuantity);
                    cmd.Parameters.AddWithValue("IsDifferentCPTDepartment", cptmaster.IsDifferentCPTDepartment);
                    cmd.Parameters.AddWithValue("IsDifferentLedger", cptmaster.IsDifferentLedger);
                    cmd.Parameters.AddWithValue("selectedLedgerID", cptmaster.SelectedLedgerID);
                    //cmd.Parameters.AddWithValue("RVU", cptmaster.RVU);

                    //DataTable dtUDT = new DataTable();
                    //dtUDT.Columns.Add("CostBucketID", typeof(int));
                    //dtUDT.Columns.Add("Description", typeof(string));
                    //dtUDT.Columns.Add("RVU", typeof(decimal));
                    //dtUDT.Columns.Add("CostTypeID", typeof(int));
                    //dtUDT.Columns.Add("ClinicianID", typeof(int));
                    //dtUDT.Columns.Add("ClinicianRoleID", typeof(int));
                    //dtUDT.Columns.Add("FacilityID", typeof(string));

                    //if (cptmaster.data != null)
                    //{
                    //    foreach (var ur in cptmaster.data)
                    //    {
                    //        DataRow dRow = dtUDT.NewRow();
                    //        dRow["CostBucketID"] = ur.CostBucketID == 0 ? (object)DBNull.Value : ur.CostBucketID;
                    //        dRow["Description"] = string.IsNullOrEmpty(ur.Description) ? (object)DBNull.Value : ur.Description;
                    //        dRow["RVU"] = ur.RVU == 0 ? (object)DBNull.Value : ur.RVU;
                    //        dRow["CostTypeID"] = ur.CostTypeID == 0 ? (object)DBNull.Value : ur.CostTypeID;
                    //        dRow["ClinicianID"] = ur.ClinicianID == 0 ? (object)DBNull.Value : ur.ClinicianID;
                    //        dRow["ClinicianRoleID"] = ur.ClinicianRoleID == 0 ? (object)DBNull.Value : ur.ClinicianRoleID;
                    //        dRow["FacilityID"] = string.IsNullOrEmpty(ur.FacilityID) ? (object)DBNull.Value : ur.FacilityID;

                    //        dtUDT.Rows.Add(dRow);
                    //    }

                    //    dtUDT.AcceptChanges();

                    //    cmd.Parameters.AddWithValue("@UDT_TB_CPT_COSTING", dtUDT);

                    //}


                    DataTable dtUDT = new DataTable();

                    dtUDT.Columns.Add("FacilityID", typeof(string));
                    dtUDT.Columns.Add("RVU_Doctor", typeof(decimal));
                    dtUDT.Columns.Add("RVU_Nurse", typeof(decimal));
                    dtUDT.Columns.Add("RVU_Allied", typeof(decimal));
                    dtUDT.Columns.Add("RVU_Cost", typeof(decimal));


                    if (cptmaster.data != null)
                    {
                        foreach (var ur in cptmaster.data)
                        {
                            DataRow dRow = dtUDT.NewRow();
                            dRow["FacilityID"] = string.IsNullOrEmpty(ur.FacilityID) ? (object)DBNull.Value : ur.FacilityID;
                            dRow["RVU_Doctor"] = ur.RVU_Doctor == 0 ? (object)DBNull.Value : ur.RVU_Doctor;
                            dRow["RVU_Nurse"] = ur.RVU_Nurse == 0 ? (object)DBNull.Value : ur.RVU_Nurse;
                            dRow["RVU_Allied"] = ur.RVU_Allied == 0 ? (object)DBNull.Value : ur.RVU_Allied;
                            dRow["RVU_Cost"] = ur.RVU_Cost == 0 ? (object)DBNull.Value : ur.RVU_Cost;

                            dtUDT.Rows.Add(dRow);
                        }

                        dtUDT.AcceptChanges();
                        cmd.Parameters.AddWithValue("@UDT_TB_CPT_COSTING_NEW", dtUDT);
                    }


                    string encounterDeptJson = null;

                    if (cptmaster.CPTEncounterDepartments != null &&
                        cptmaster.CPTEncounterDepartments.Count > 0)
                    {
                        encounterDeptJson = Newtonsoft.Json.JsonConvert
                            .SerializeObject(cptmaster.CPTEncounterDepartments);
                    }

                    cmd.Parameters.AddWithValue(
                        "@CPTEncounterDepartmentsJson",
                        (object)encounterDeptJson ?? DBNull.Value
                    );


                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_CPT_MASTER";
                    Int32 CptMasterID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return CptMasterID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 Update(CptMaster cptmaster, Int32 userID)
        {
            CptMaster res = new CptMaster();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CPT_MASTER";
                    cmd.Parameters.AddWithValue("ACTION", 3);

                    cmd.Parameters.AddWithValue("ID", cptmaster.ID);
                    cmd.Parameters.AddWithValue("CPTTypeID", cptmaster.CPTTypeID);
                    cmd.Parameters.AddWithValue("CPTCode", cptmaster.CPTCode);
                    cmd.Parameters.AddWithValue("CPTName", cptmaster.CPTName);
                    cmd.Parameters.AddWithValue("Description", cptmaster.Description);
                    cmd.Parameters.AddWithValue("IsInactive", cptmaster.IsInactive);
                    cmd.Parameters.AddWithValue("UserID", userID);
                    cmd.Parameters.AddWithValue("CPTGroup", cptmaster.CPTGroup);
                    cmd.Parameters.AddWithValue("DepartmentID", cptmaster.DepartmentID);
                    cmd.Parameters.AddWithValue("CPTDepartmentID", cptmaster.CPTDepartmentID);
                    cmd.Parameters.AddWithValue("CostDepartmentID", cptmaster.CostDepartmentID);
                    cmd.Parameters.AddWithValue("CostDriveID", cptmaster.CostDriveID);
                    cmd.Parameters.AddWithValue("FixedQuantity", cptmaster.FixedQuantity);
                    cmd.Parameters.AddWithValue("IsDifferentCPTDepartment", cptmaster.IsDifferentCPTDepartment);
                    cmd.Parameters.AddWithValue("IsDifferentLedger", cptmaster.IsDifferentLedger);
                    cmd.Parameters.AddWithValue("selectedLedgerID", cptmaster.SelectedLedgerID);
                    //cmd.Parameters.AddWithValue("RVU", cptmaster.RVU);

                    //DataTable dtUDT = new DataTable();
                    //dtUDT.Columns.Add("CostBucketID", typeof(int));
                    //dtUDT.Columns.Add("Description", typeof(string));
                    //dtUDT.Columns.Add("RVU", typeof(decimal));
                    //dtUDT.Columns.Add("CostTypeID", typeof(int));
                    //dtUDT.Columns.Add("ClinicianID", typeof(int));
                    //dtUDT.Columns.Add("ClinicianRoleID", typeof(int));
                    //dtUDT.Columns.Add("FacilityID", typeof(string));


                    //if (cptmaster.data != null)
                    //{
                    //    foreach (var ur in cptmaster.data)
                    //    {
                    //        DataRow dRow = dtUDT.NewRow();
                    //        dRow["CostBucketID"] = ur.CostBucketID == 0 ? (object)DBNull.Value : ur.CostBucketID;
                    //        dRow["Description"] = string.IsNullOrEmpty(ur.Description) ? (object)DBNull.Value : ur.Description;
                    //        dRow["RVU"] = ur.RVU == 0 ? (object)DBNull.Value : ur.RVU;
                    //        dRow["CostTypeID"] = ur.CostTypeID == 0 ? (object)DBNull.Value : ur.CostTypeID;
                    //        dRow["ClinicianID"] = ur.ClinicianID == 0 ? (object)DBNull.Value : ur.ClinicianID;
                    //        dRow["ClinicianRoleID"] = ur.ClinicianRoleID == 0 ? (object)DBNull.Value : ur.ClinicianRoleID;
                    //        dRow["FacilityID"] = string.IsNullOrEmpty(ur.FacilityID) ? (object)DBNull.Value : ur.FacilityID;


                    //        dtUDT.Rows.Add(dRow);
                    //    }

                    //    dtUDT.AcceptChanges();
                    //    cmd.Parameters.AddWithValue("@UDT_TB_CPT_COSTING", dtUDT);
                    //}


                    DataTable dtUDT = new DataTable();

                    dtUDT.Columns.Add("FacilityID", typeof(string));
                    dtUDT.Columns.Add("RVU_Doctor", typeof(decimal));
                    dtUDT.Columns.Add("RVU_Nurse", typeof(decimal));
                    dtUDT.Columns.Add("RVU_Allied", typeof(decimal));
                    dtUDT.Columns.Add("RVU_Cost", typeof(decimal));


                    if (cptmaster.data != null)
                    {
                        foreach (var ur in cptmaster.data)
                        {
                            DataRow dRow = dtUDT.NewRow();
                            dRow["FacilityID"] = string.IsNullOrEmpty(ur.FacilityID) ? (object)DBNull.Value : ur.FacilityID;
                            dRow["RVU_Doctor"] = ur.RVU_Doctor == 0 ? (object)DBNull.Value : ur.RVU_Doctor;
                            dRow["RVU_Nurse"] = ur.RVU_Nurse == 0 ? (object)DBNull.Value : ur.RVU_Nurse;
                            dRow["RVU_Allied"] = ur.RVU_Allied == 0 ? (object)DBNull.Value : ur.RVU_Allied;
                            dRow["RVU_Cost"] = ur.RVU_Cost == 0 ? (object)DBNull.Value : ur.RVU_Cost;

                            dtUDT.Rows.Add(dRow);
                        }

                        dtUDT.AcceptChanges();
                        cmd.Parameters.AddWithValue("@UDT_TB_CPT_COSTING_NEW", dtUDT);
                    }


                    string encounterDeptJson = null;

                    if (cptmaster.CPTEncounterDepartments != null &&
                        cptmaster.CPTEncounterDepartments.Count > 0)
                    {
                        encounterDeptJson = Newtonsoft.Json.JsonConvert
                            .SerializeObject(cptmaster.CPTEncounterDepartments);
                    }

                    cmd.Parameters.AddWithValue(
                        "@CPTEncounterDepartmentsJson",
                        (object)encounterDeptJson ?? DBNull.Value
                    );



                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_CPT_MASTER";
                    Int32 CptMasterID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return CptMasterID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CptMaster> GetItems(int id)
        {
            List<CptMaster> CptMasterList = new List<CptMaster>();
            try
            {
                string strSQL = "SELECT TB_CPT_MASTER.ID, TB_CPT_MASTER.CPTTypeID, " +
                                "TB_CPT_MASTER.CPTCode, TB_CPT_MASTER.CPTName, TB_CPT_MASTER.Description, TB_CPT_MASTER.IsInactive, " +
                                "TB_CPT_MASTER.IsDeleted, TB_CPT_TYPES.CPTType, TB_CPT_MASTER.CPTGroup, TB_CPT_MASTER.CostDriveID, TB_CPT_MASTER.FixedQuantity, TB_CPT_MASTER.IsDifferentCPTDepartment, TB_CPT_MASTER.IsSelectedLedger, " +
                                "TB_DEPARTMENT.ID AS DepartmentID, TB_CPT_DEPARTMENT.ID as CPTDepartmentID, TB_COSTING_DEPARTMENT.ID AS CostDepartmentID " +
                                "FROM TB_CPT_MASTER " +
                                "LEFT JOIN TB_CPT_TYPES ON TB_CPT_MASTER.CPTTypeID = TB_CPT_TYPES.ID " +
                                "LEFT JOIN TB_DEPARTMENT ON TB_DEPARTMENT.ID = TB_CPT_MASTER.DepartmentID " +
                                "LEFT JOIN TB_CPT_DEPARTMENT ON TB_CPT_DEPARTMENT.ID = TB_CPT_MASTER.CPTDepartmentID " +
                                "LEFT JOIN TB_COSTING_DEPARTMENT ON TB_COSTING_DEPARTMENT.ID = TB_CPT_MASTER.CostingDepartmentID " +
                                "WHERE TB_CPT_MASTER.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "CptMaster");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    // Create a single CptMaster object
                    CptMaster master = new CptMaster
                    {
                        ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                        CPTTypeID = dr["CPTTypeID"] != DBNull.Value ? Convert.ToInt32(dr["CPTTypeID"]) : 0,
                        CPTType = dr["CPTType"] != DBNull.Value ? Convert.ToString(dr["CPTType"]) : string.Empty,
                        CPTCode = dr["CPTCode"] != DBNull.Value ? Convert.ToString(dr["CPTCode"]) : string.Empty,
                        Description = dr["Description"] != DBNull.Value ? Convert.ToString(dr["Description"]) : string.Empty,
                        CPTName = dr["CPTName"] != DBNull.Value ? Convert.ToString(dr["CPTName"]) : string.Empty,
                        IsInactive = dr["IsInactive"] != DBNull.Value ? Convert.ToBoolean(dr["IsInactive"]) : false,
                        CPTGroup = dr["CPTGroup"] != DBNull.Value ? Convert.ToString(dr["CPTGroup"]) : string.Empty,
                        DepartmentID = dr["DepartmentID"] != DBNull.Value ? Convert.ToInt32(dr["DepartmentID"]) : 0,
                        CPTDepartmentID = dr["CPTDepartmentID"] != DBNull.Value ? Convert.ToInt32(dr["CPTDepartmentID"]) : 0,
                        CostDepartmentID = dr["CostDepartmentID"] != DBNull.Value ? Convert.ToInt32(dr["CostDepartmentID"]) : 0,
                        CostDriveID = dr["CostDriveID"] != DBNull.Value ? Convert.ToInt32(dr["CostDriveID"]) : 0,
                        FixedQuantity = dr["FixedQuantity"] != DBNull.Value
                                        ? Convert.ToSingle(dr["FixedQuantity"])
                                        : 0f,
                        IsDifferentCPTDepartment = dr["IsDifferentCPTDepartment"] != DBNull.Value ? Convert.ToBoolean(dr["IsDifferentCPTDepartment"]) : false,
                        IsDifferentLedger = dr["IsSelectedLedger"] != DBNull.Value ? Convert.ToBoolean(dr["IsSelectedLedger"]) : false,
                        data = new List<costSettingsData>() , // initialize the list
                        CPTEncounterDepartments = new List<cptEncounterDepartmentData>()
                    };

                    if (master.IsDifferentLedger == true)
                    {
                        string query = "SELECT LedgerID FROM TB_CPT_LEDGER WHERE CPTID = " + id;

                        DataTable dt = ADO.GetDataTable(query);

                        master.SelectedLedgerID = string.Join(",",
                            dt.AsEnumerable()
                              .Select(r => r["LedgerID"].ToString())
                        );
                    }
                    else
                    {
                        master.SelectedLedgerID = "";
                    }


                    // Get Costing Rows
                    //string strSQL1 = "SELECT * FROM TB_CPT_COSTING WHERE CPTID = " + id;
                    //DataTable tbl1 = ADO.GetDataTable(strSQL1, "CptMasterCosting");

                    //foreach (DataRow costRow in tbl1.Rows)
                    //{
                    //    master.data.Add(new costSettingsData
                    //    {
                    //        CostBucketID = costRow["CostBucketID"] != DBNull.Value ? Convert.ToInt32(costRow["CostBucketID"]) : 0,
                    //        Description = costRow["Description"] != DBNull.Value ? Convert.ToString(costRow["Description"]) : string.Empty,
                    //        CostTypeID = costRow["CostTypeID"] != DBNull.Value ? Convert.ToInt32(costRow["CostTypeID"]) : 0,
                    //        ClinicianID = costRow["ClinicianID"] != DBNull.Value ? Convert.ToInt32(costRow["ClinicianID"]) : 0,
                    //        ClinicianRoleID = costRow["ClinicianRoleID"] != DBNull.Value ? Convert.ToInt32(costRow["ClinicianRoleID"]) : 0,
                    //        RVU = costRow["RVU"] != DBNull.Value ? (decimal)ADO.ToFloat(costRow["RVU"]) : 0,
                    //        FacilityID = costRow["FacilityID"] != DBNull.Value ? Convert.ToString(costRow["FacilityID"]) : string.Empty,
                    //    });
                    //}

                    //CptMasterList.Add(master);

                    string strSQL1 = "SELECT * FROM TB_CPT_COSTING WHERE CPTID = " + id;
                    DataTable tbl1 = ADO.GetDataTable(strSQL1, "CptMasterCosting");


                    // Create a dictionary to group rows by FacilityID
                    Dictionary<string, costSettingsData> grouped = new Dictionary<string, costSettingsData>();

                    foreach (DataRow costRow in tbl1.Rows)
                    {
                        string facility = costRow["FacilityID"] != DBNull.Value
                                            ? costRow["FacilityID"].ToString()
                                            : string.Empty;

                        string description = costRow["Description"] != DBNull.Value
                                                ? costRow["Description"].ToString().ToLower()
                                                : string.Empty;

                        decimal rvuValue = costRow["RVU"] != DBNull.Value
                                            ? (decimal)ADO.ToFloat(costRow["RVU"])
                                            : 0;

                        // If facility not added, create one
                        if (!grouped.ContainsKey(facility))
                        {
                            grouped[facility] = new costSettingsData
                            {
                                FacilityID = facility,
                                RVU_Doctor = 0,
                                RVU_Nurse = 0,
                                RVU_Allied = 0,
                                RVU_Cost = 0
                            };
                        }

                        // Update proper RVU
                        switch (description)
                        {
                            case "doctor":
                                grouped[facility].RVU_Doctor = rvuValue;
                                break;

                            case "nurse":
                                grouped[facility].RVU_Nurse = rvuValue;
                                break;

                            case "allied":
                                grouped[facility].RVU_Allied = rvuValue;
                                break;

                            case "cost":
                                grouped[facility].RVU_Cost = rvuValue;
                                break;
                        }
                    }

                    // Add grouped rows to master.data
                    master.data = grouped.Values.ToList();


                    string strSQL2 = @"
                                    SELECT *
                                    FROM TB_CPT_ENCOUNTER_DEPARTMENT
                                    WHERE CPTID = " + id;

                    DataTable tbl2 = ADO.GetDataTable(strSQL2, "CptEncounterDepartments");

                    foreach (DataRow row in tbl2.Rows)
                    {
                        master.CPTEncounterDepartments.Add(new cptEncounterDepartmentData
                        {
                            CPTID = row["CPTID"] != DBNull.Value
                                                ? Convert.ToInt32(row["CPTID"])
                                                : 0,
                            EncounterType = row["EncounterType"] != DBNull.Value
                                                ? Convert.ToString(row["EncounterType"])
                                                : string.Empty,
                            DepartmentID = row["DepartmentID"] != DBNull.Value
                                                ? Convert.ToInt32(row["DepartmentID"])
                                                : 0
                        });
                    }

                    // Add once after processing all rows

                    CptMasterList.Add(master);

                }
            }
            catch (Exception ex)
            {
                // Log exception
            }

            return CptMasterList;
        }

        public bool DeleteCptMaster(int Id, int userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CPT_MASTER";
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

        public subDepartmentResponse getSubDepartment(CptMaster vInput)
        {
            subDepartmentResponse res = new subDepartmentResponse();
            List<subDepartment> suList = new List<subDepartment>();

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    string query = "SELECT ID, SubDepartment AS DESCRIPTION FROM TB_CPT_DEPARTMENT WHERE DepartmentID = @DepartmentID";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@DepartmentID", vInput.DepartmentID);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable tbl = new DataTable();
                        da.Fill(tbl);

                        suList = tbl.AsEnumerable().Select(dr => new subDepartment
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            DESCRIPTION = Convert.ToString(dr["DESCRIPTION"])
                        }).ToList();

                        res.flag = "1";
                        res.message = "Success";
                        res.data = suList;
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = "Error: " + ex.Message;
                res.data = new List<subDepartment>();
            }

            return res;
        }



    }
}
