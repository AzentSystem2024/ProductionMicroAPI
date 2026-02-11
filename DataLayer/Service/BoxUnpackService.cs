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
    public class BoxUnpackService:IBoxUnpackService
    {
        public BoxUnpackResponse UnpackBox(BoxUnpack model)
        {
            BoxUnpackResponse res = new BoxUnpackResponse();

            try
            {
                using (SqlConnection con = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_BOX_UNPACK", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@COMPANY_ID", model.COMPANY_ID);
                        cmd.Parameters.AddWithValue("@BOX_ID", model.BOX_ID);
                        cmd.Parameters.AddWithValue("@UNPACK_DATE", model.UNPACK_DATE ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }

                res.Flag = 1;
                res.Message = "Box unpacked successfully";
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
