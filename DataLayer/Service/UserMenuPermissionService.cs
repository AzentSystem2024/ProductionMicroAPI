using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class UserMenuPermissionService : IUserMenuPermissionService
    {
        public UserMenuPermission GetUserMenuPermissions(int userId, int menuId)
        {
            UserMenuPermission permissions = new UserMenuPermission();

            using (var connection = ADO.GetConnection())
            {

            
             if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (var cmd = new SqlCommand("SP_CHECK_USERMENU_PERMISSION", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@MenuID", menuId);


                    using (var reader = cmd.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            permissions.CanAdd = reader.GetBoolean(reader.GetOrdinal("CanAdd"));
                            permissions.CanView = reader.GetBoolean(reader.GetOrdinal("CanView"));
                            permissions.CanEdit = reader.GetBoolean(reader.GetOrdinal("CanEdit"));
                            permissions.CanApprove = reader.GetBoolean(reader.GetOrdinal("CanApprove"));
                            permissions.CanDelete = reader.GetBoolean(reader.GetOrdinal("CanDelete"));
                            permissions.CanPrint = reader.GetBoolean(reader.GetOrdinal("CanPrint"));
                        }
                    }
                }
            }

            return permissions;
        }
    }
}

