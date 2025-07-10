using MicroApi.DataLayer.Interface;
using System.Data.SqlClient;
using System.Data;
using MicroApi.Models;
using MicroApi.Helper;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicroApi.DataLayer.Service
{
    public class GroupService : IGroupService
    {
        private readonly ILogger<GroupService> _logger;
        // private readonly string _connectionString;

        public GroupService(ILogger<GroupService> logger)
        {
            _logger = logger;
        }

        public GroupResponse GetLogList(int? id = null)
        {
            GroupResponse response = new GroupResponse
            {
                Data = new List<Group>() // Ensure Data is initialized
            };

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    using (var command = new SqlCommand("SP_GET_ALL_GROUPS_ORDERED", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Group group = new Group
                                {
                                    GROUP_ID = !reader.IsDBNull(reader.GetOrdinal("GroupId")) ? reader.GetInt32(reader.GetOrdinal("GroupId")) : 0,
                                    GROUP_NAME = !reader.IsDBNull(reader.GetOrdinal("Groupname")) ? reader.GetString(reader.GetOrdinal("Groupname")) : string.Empty,
                                    GROUP_SUPER_ID = !reader.IsDBNull(reader.GetOrdinal("ParentGroupId")) ? reader.GetInt32(reader.GetOrdinal("ParentGroupId")) : 0,
                                    GROUP_LEVEL = !reader.IsDBNull(reader.GetOrdinal("Grouplevel")) ? reader.GetInt32(reader.GetOrdinal("Grouplevel")) : 0
                                };

                                response.Data.Add(group);
                            }
                        }
                    }
                }

                response.flag = 1;
                response.Message = "Success";
                _logger.LogInformation("Data retrieved successfully: {Data}", response.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching account list details.");
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

