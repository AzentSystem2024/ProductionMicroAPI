using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class UserRoleDropdownService : IUserRoleDropdownService
    {

        public List<UserRoleDropdown> GetDropDownData()
        {
            List<UserRoleDropdown> dropDowns = new List<UserRoleDropdown>();
            using (var connection = ADO.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (var cmd = new SqlCommand("SP_GetUserRolesForDropdown", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            dropDowns.Add(new UserRoleDropdown
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                UserRole = reader.GetString(reader.GetOrdinal("UserRole"))
                            });
                        }
                    }
                }
            }

            return dropDowns;
        }
    }
}
