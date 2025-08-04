using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MicroApi.DataLayer.Service
{
    public class FixedAssetService : IFixedAssetService
    {
        public FixedAssetResponse GetFixedAssetList()
        {
            FixedAssetResponse res = new FixedAssetResponse
            {
                Data = new List<FixedAssetList>()
            };
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_AC_FIXEDASSET";
                cmd.Parameters.AddWithValue("@ACTION", 0);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    res.Data.Add(new FixedAssetList
                    {
                        ID = Convert.IsDBNull(dr["ID"]) ? 0 : Convert.ToInt32(dr["ID"]),
                        CODE = Convert.IsDBNull(dr["CODE"]) ? null : Convert.ToString(dr["CODE"]),
                        DESCRIPTION = Convert.IsDBNull(dr["DESCRIPTION"]) ? null : Convert.ToString(dr["DESCRIPTION"]),
                        ASSET_TYPE = Convert.IsDBNull(dr["ASSET_TYPE"]) ? null : Convert.ToString(dr["ASSET_TYPE"]),
                        PURCH_DATE = Convert.IsDBNull(dr["PURCH_DATE"]) ? null : Convert.ToDateTime(dr["PURCH_DATE"]).ToString("dd/MM/yy"),
                        ASSET_VALUE = Convert.IsDBNull(dr["ASSET_VALUE"]) ? 0 : Convert.ToSingle(dr["ASSET_VALUE"]),
                        USEFUL_LIFE = Convert.IsDBNull(dr["USEFUL_LIFE"]) ? 0 : Convert.ToInt32(dr["USEFUL_LIFE"]),
                        NET_DEPRECIATION = Convert.IsDBNull(dr["NET_DEPRECIATION"]) ? 0 : Convert.ToSingle(dr["NET_DEPRECIATION"]),
                        CURRENT_VALUE = Convert.IsDBNull(dr["CURRENT_VALUE"]) ? 0 : Convert.ToInt16(dr["CURRENT_VALUE"]),
                    });
                }
            }
            res.flag = 0;
            res.Message = "Success";

            return res;
        }
        
    }
}
