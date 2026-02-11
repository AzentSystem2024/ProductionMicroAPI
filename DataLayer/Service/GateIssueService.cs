using Azure.Core;
using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.DataLayer.Service
{
    public class GateIssueService:IGateIssueService
    {
        public GateIssueResponse InsertGateIssue(GateIssue model)
        {
            GateIssueResponse res = new GateIssueResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_GATE_ISSUE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@ISSUED_TO", model.ISSUE_TO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ISSUED_BY", model.ISSUED_BY ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ISSUE_TIME", model.ISSUE_TIME);
                        cmd.Parameters.AddWithValue("@REMARKS", model.REMARKS ?? (object)DBNull.Value);

                        //  Build UDT DataTable
                        DataTable dt = new DataTable();
                        dt.Columns.Add("ARTICLE_PRODUCTION_ID", typeof(long));

                        if (model.Articles != null)
                        {
                            foreach (var item in model.Articles)
                            {
                                dt.Rows.Add(item.ARTICLE_PRODUCTION_ID);
                            }
                        }

                        SqlParameter tvp = cmd.Parameters.AddWithValue("@UDT_TB_GATE_ISSUE", dt);
                        tvp.SqlDbType = SqlDbType.Structured;
                        tvp.TypeName = "UDT_TB_GATE_ISSUE";

                        cmd.ExecuteNonQuery();
                    }
                }

                res.Flag = 1;
                res.Message = "Gate issue inserted successfully";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

    }
}
