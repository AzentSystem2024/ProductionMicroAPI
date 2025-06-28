
using MicroApi.DataLayer.Interface;
using System.Data;
using System.Data.SqlClient;
using MicroApi.Models;
using MicroApi.Helper;

namespace MicroApi.Services
{
    public class UserRoleService : IUserRoleService
    {
        public UserRoleResponse GetAllUserRoles(int userId)
        {
            var response = new UserRoleResponse
            {
                flag = 1,
                message = "Success",
                data = new List<UserRole>()
            };

            using (SqlConnection connection = ADO.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SP_TB_USER_ROLE", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Action", 0);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();

                    try
                    {
                        da.Fill(ds);

                        if (ds.Tables.Count == 0)
                        {
                            response.flag = 0;
                            response.message = "No data returned from the stored procedure.";
                            return response;
                        }

                        var combinedTable = ds.Tables[0];
                        var userRoles = new Dictionary<int, UserRole>();

                        foreach (DataRow row in combinedTable.Rows)
                        {
                            if (row["ID"] != DBNull.Value)
                            {
                                int roleId = Convert.ToInt32(row["ID"]);

                                if (!userRoles.ContainsKey(roleId))
                                {
                                    userRoles[roleId] = new UserRole
                                    {
                                        ID = roleId,
                                        UserRoles = row["UserRole"].ToString(),
                                        LastModifiedTime = row["LastModifiedTime"] == DBNull.Value ? (DateTime?)null : ((DateTime)row["LastModifiedTime"]).ToLocalTime(),
                                        CreateTime = row["CreatedTime"] == DBNull.Value ? (DateTime?)null : ((DateTime)row["CreatedTime"]).ToLocalTime(),
                                        IsInactive = row["IsInactive"] != DBNull.Value && Convert.ToBoolean(row["IsInactive"]),
                                        usermenulist = new List<UserMenuList>()
                                    };
                                }

                                userRoles[roleId].usermenulist.Add(new UserMenuList
                                {
                                    MenuId = Convert.ToInt32(row["menuid"]),
                                    MenuName = row["menuname"].ToString(),
                                    MenuOrder = row["menuorder"].ToString(),
                                    Selected = row["Selected"] != DBNull.Value && Convert.ToBoolean(row["Selected"]),
                                    CanAdd = row["CanAdd"] != DBNull.Value && Convert.ToBoolean(row["CanAdd"]),
                                    CanView = row["CanView"] != DBNull.Value && Convert.ToBoolean(row["CanView"]),
                                    CanPrint = row["CanPrint"] != DBNull.Value && Convert.ToBoolean(row["CanPrint"]),
                                    CanEdit = row["CanEdit"] != DBNull.Value && Convert.ToBoolean(row["CanEdit"]),
                                    CanDelete = row["CanDelete"] != DBNull.Value && Convert.ToBoolean(row["CanDelete"])
                                });
                         
                            }
                        }

                        // Convert dictionary values to list for response
                        response.data = userRoles.Values.ToList();
                    }
                    catch (Exception ex)
                    {
                        response.flag = 0;
                        response.message = ex.Message;
                    }
                }
            }

            return response;
        }

        public UserMenuResponse GetAllUsermainRoles(int intUserID)
        {
            UserMenuResponse response = new UserMenuResponse();
            List<UserMenuGroup> userMenuGroupList = new List<UserMenuGroup>();

            SqlConnection connection = ADO.GetConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_TB_USER_ROLE";
            cmd.Parameters.AddWithValue("ACTION", 1);
            cmd.Parameters.AddWithValue("Userid", intUserID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);

            var groupedData = tbl.AsEnumerable()
                .GroupBy(r => new
                {
                    MenuGroupId = r["menugroupid"],
                    text = r["menugroupname"],
                    MenuGroupOrder = r["menugrouporder"],
                    //icon = r["MenuIcon"]
                });

            foreach (var group in groupedData)
            {
                UserMenuGroup userMenuGroup = new UserMenuGroup
                {
                    MenuGroupId = group.Key.MenuGroupId == DBNull.Value ? 0 : Convert.ToInt32(group.Key.MenuGroupId),
                    text = group.Key.text == DBNull.Value ? null : Convert.ToString(group.Key.text),
                    //icon = group.Key.icon == DBNull.Value ? null : Convert.ToString(group.Key.icon),

                    MenuGroupOrder = group.Key.MenuGroupOrder == DBNull.Value ? null : Convert.ToString(group.Key.MenuGroupOrder),
                    Menus = group.Select(dr => new UserMenuList
                    {
                        MenuId = dr["menuid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["menuid"]),
                        MenuName = dr["menuname"] == DBNull.Value ? null : Convert.ToString(dr["menuname"]),
                        MenuOrder = dr["menuorder"] == DBNull.Value ? null : Convert.ToString(dr["menuorder"]),
                        Selected = dr["selected"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dr["selected"])
                    }).ToList()
                };

                userMenuGroupList.Add(userMenuGroup);
            }

            connection.Close();


            response.Flag = 1;
            response.Message = "Success";
            response.ID = response.ID;
            response.UserRoles = response.UserRoles;
            response.IsInactive = response.IsInactive;
            response.Data = userMenuGroupList;

            return response;
        }
        public int Insert(UserRole userrole, Int32 userID)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("MenuId", typeof(Int32));
                //tbl.Columns.Add("MenuName", typeof(string));
                //tbl.Columns.Add("MenuOrder", typeof(String));
                //tbl.Columns.Add("Selected", typeof(bool));

                if (userrole.usermenulist != null && userrole.usermenulist.Any())
                {

                    foreach (UserMenuList ur in userrole.usermenulist)
                    {
                        DataRow dRow = tbl.NewRow();
                        dRow["MenuId"] = ur.MenuId;
                        //dRow["MenuName"] = ur.MenuName;
                        //dRow["MenuOrder"] = ur.MenuOrder;
                        //dRow["Selected"] = ur.Selected;

                        tbl.Rows.Add(dRow);
                        tbl.AcceptChanges();
                    }
                }

                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.Transaction = objtrans;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_USER_ROLE";
                    cmd.Parameters.AddWithValue("ACTION", 2);

                    // cmd.Parameters.AddWithValue("ID", customer.ID);
                    cmd.Parameters.AddWithValue("UserRole", userrole.UserRoles);
                    cmd.Parameters.AddWithValue("IsInactive", userrole.IsInactive);
                    cmd.Parameters.AddWithValue("UserID", userID);
                    cmd.Parameters.AddWithValue("@CanAdd", userrole.CanAdd);
                    cmd.Parameters.AddWithValue("@CanView", userrole.CanView);
                    cmd.Parameters.AddWithValue("@CanPrint", userrole.CanPrint);
                    cmd.Parameters.AddWithValue("@CanEdit", userrole.CanEdit);
                    cmd.Parameters.AddWithValue("@CanDelete", userrole.CanDelete);

                    cmd.Parameters.AddWithValue("@MenuIDs", tbl);

                    cmd.ExecuteNonQuery();

                    objtrans.Commit();

                    connection.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }
        public int Update(UserRole userrole, Int32 userID)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("MenuId", typeof(Int32));
                tbl.Columns.Add("CanAdd", typeof(bool));
                tbl.Columns.Add("CanView", typeof(bool));
                tbl.Columns.Add("CanPrint", typeof(bool));
                tbl.Columns.Add("CanEdit", typeof(bool));
                tbl.Columns.Add("CanDelete", typeof(bool));
                //tbl.Columns.Add("MenuName", typeof(string));
                //tbl.Columns.Add("MenuOrder", typeof(String));
                //tbl.Columns.Add("Selected", typeof(bool));

                if (userrole.usermenulist != null && userrole.usermenulist.Any())
                {

                    foreach (UserMenuList ur in userrole.usermenulist)
                    {
                        DataRow dRow = tbl.NewRow();
                        dRow["MenuId"] = ur.MenuId;
                        dRow["CanAdd"] = ur.CanAdd;
                        dRow["CanView"] = ur.CanView;
                        dRow["CanPrint"] = ur.CanPrint;
                        dRow["CanEdit"] = ur.CanEdit;
                        dRow["CanDelete"] = ur.CanDelete;
                        //dRow["MenuName"] = ur.MenuName;
                        //dRow["MenuOrder"] = ur.MenuOrder;
                        //dRow["Selected"] = ur.Selected;

                        tbl.Rows.Add(dRow);
                        tbl.AcceptChanges();
                    }
                }

                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.Transaction = objtrans;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_USER_ROLE";
                    cmd.Parameters.AddWithValue("ACTION", 3);

                    cmd.Parameters.AddWithValue("ID", userrole.ID);
                    cmd.Parameters.AddWithValue("UserRole", userrole.UserRoles);
                    cmd.Parameters.AddWithValue("IsInactive", userrole.IsInactive);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.Parameters.AddWithValue("@MenuIDs", tbl);

                    cmd.ExecuteNonQuery();

                    objtrans.Commit();

                    connection.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }
        public void DeleteUserRole(int Id, int userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_USER_ROLE";
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserRole GetItems(int id)
        {
            UserRole userRole = new UserRole
            {
                usermenulist = new List<UserMenuList>()
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_TB_USER_ROLE", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", 0); // Action to get user roles
                    cmd.Parameters.AddWithValue("@ID", id); // Specific user role ID

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables.Count > 0)
                        {
                            var combinedTable = ds.Tables[0];
                            var userRoles = new Dictionary<int, UserRole>();

                            foreach (DataRow row in combinedTable.Rows)
                            {
                                if (row["ID"] != DBNull.Value)
                                {
                                    int roleId = Convert.ToInt32(row["ID"]);
                                    if (!userRoles.ContainsKey(roleId))
                                    {
                                        userRoles[roleId] = new UserRole
                                        {
                                            ID = roleId,
                                            UserRoles = row["UserRole"].ToString(),
                                            LastModifiedTime = row["LastModifiedTime"] == DBNull.Value ? (DateTime?)null : ((DateTime)row["LastModifiedTime"]).ToLocalTime(),
                                            CreateTime = row["CreatedTime"] == DBNull.Value ? (DateTime?)null : ((DateTime)row["CreatedTime"]).ToLocalTime(),
                                            IsInactive = row["IsInactive"] != DBNull.Value && Convert.ToBoolean(row["IsInactive"]),
                                            usermenulist = new List<UserMenuList>()
                                        };
                                    }

                                    userRoles[roleId].usermenulist.Add(new UserMenuList
                                    {
                                        MenuId = Convert.ToInt32(row["menuid"]),
                                        MenuName = row["menuname"].ToString(),
                                        MenuOrder = row["menuorder"].ToString(),
                                        Selected = Convert.ToBoolean(row["Selected"])
                                    });
                                }
                            }

                            // Assuming the ID is valid and exists in the dictionary
                            if (userRoles.ContainsKey(id))
                            {
                                userRole = userRoles[id];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return userRole;
        }




        //public UserRole GetItems(int id)
        //{
        //    UserRole userRole = new UserRole();
        //    List<UserMenuList> userMenuLists = new List<UserMenuList>();
        //    try
        //    {
        //        // Assuming id is an integer. If it's a string, make sure to properly escape it.
        //        string strSQL = "SELECT ID,UserRole,IsInactive,LastModifiedTime " +


        //        "FROM TB_USER_ROLES " +

        //        "WHERE TB_USER_ROLES.ID = " + id; // Assuming id is an integer. If it's a string, use parameters to avoid SQL injection.


        //        DataTable tbl = ADO.GetDataTable(strSQL, "Customer");
        //        if (tbl.Rows.Count > 0)
        //        {
        //            DataRow dr = tbl.Rows[0];


        //            userRole.ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : (int?)null;
        //            userRole.UserRoles = dr["UserRole"] != DBNull.Value ? Convert.ToString(dr["UserRole"]) : null;
        //            userRole.IsInactive = dr["IsInactive"] != DBNull.Value ? Convert.ToBoolean(dr["IsInactive"]) : (bool?)null;
        //            userRole.LastModifiedTime = dr["LastModifiedTime"] != DBNull.Value ? Convert.ToDateTime(dr["LastModifiedTime"]) : (DateTime?)null;


        //            strSQL = "SELECT TB_USER_ROLE_MENUS.MenuId, " +
        //                      "TB_MENUS.MenuName " +
        //                      "FROM TB_USER_ROLE_MENUS " +
        //                    "LEFT JOIN TB_MENUS ON TB_USER_ROLE_MENUS.MenuId = TB_MENUS.ID " +
        //                    "WHERE TB_USER_ROLE_MENUS.UserRoleID = " + id;


        //            DataTable tblDetail = ADO.GetDataTable(strSQL, "CustomerConfiguration");
        //            if (tblDetail.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr1 in tblDetail.Rows)
        //                {
        //                    userMenuLists.Add(new UserMenuList
        //                    {

        //                        MenuId = Convert.ToInt32(dr1["MenuId"]),
        //                        MenuName = Convert.ToString(dr1["MenuName"])

        //                    });
        //                }
        //            }
        //            userRole.usermenulist = userMenuLists;

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return userRole;
        //}
    }
}
