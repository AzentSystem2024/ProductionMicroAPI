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

namespace MicroApi.Service
{
    public class ArticleLookUpService : IArticleLookUpService
    {
        private readonly ILogger<ArticleLookUpService> _logger;

        public ArticleLookUpService(ILogger<ArticleLookUpService> logger)
        {
            _logger = logger;
        }

        public ArticleLookUpResponse GetArticleList()
        {
            ArticleLookUpResponse response = new ArticleLookUpResponse();

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    using (var command = new SqlCommand("sp_GetArticleLookup", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                           
                            response.Data = dataTable.AsEnumerable()
                                .Select(row => new Articlelist
                                {
                                    ART_NO = row.Field<string>("ART_NO"),
                                    Category = row.Field<string>("Category"),
                                    Color = row.Field<string>("Color"),
                                    PRICE = Convert.ToDecimal(row.Field<double>("PRICE")),
                                    ArticleType = row.Field<string>("ArticleType"),
                                    Brand = row.Field<string>("Brand"),
                                    ALIAS_NO = row.Field<string>("ALIAS_NO"),
                                    PART_NO = row.Field<string>("PART_NO"),
                                    Status = row.Field<string>("Status"),
                                    DESCRIPTION = row.Field<string>("DESCRIPTION")
                                }).ToList();
                        }
                    }
                }

                response.flag = 1;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching article details.");
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}