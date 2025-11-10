using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.DataLayer.Service
{
    public class ListGroupHeadService : IListGroupHeadService
    {
        private readonly ILogger<ListGroupHeadService> _logger;

        public ListGroupHeadService(ILogger<ListGroupHeadService> logger)
        {
            _logger = logger;
        }

        public ListResponse GetLogList(int? id = null)
        {
            ListResponse response = new ListResponse();
            List<ListGroupHead> accountHeadDetailsList = new List<ListGroupHead>();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    using (var command = new SqlCommand("SP_GETACCOUNTHEADDETAILS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accountHeadDetailsList.Add(new ListGroupHead
                                {
                                    MainGroup = reader["MAIN_GROUP"].ToString(),
                                    SubGroup = reader["SUBGROUP"].ToString(),
                                    Category = reader["CATEGORY"].ToString(),
                                    HeadCode = reader["HEAD_CODE"].ToString(),
                                    HeadName = reader["HEAD_NAME"].ToString(),
                                    ID = !reader.IsDBNull(reader.GetOrdinal("ID")) ? reader.GetInt32(reader.GetOrdinal("ID")) : 0,
                                    IS_INACTIVE = reader["STATUS"].ToString(),


                                });
                            }
                        }
                    }
                }

                response.Data = accountHeadDetailsList;
                response.flag = 1;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching account head details.");
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}