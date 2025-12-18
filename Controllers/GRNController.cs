using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GRNController : ControllerBase
    {
        private readonly IGRNService _grnService;
        public GRNController(IGRNService testService)
        {
            _grnService = testService;
        }

        [HttpPost]
        [Route("pendingpo")]
        public GRNResponse PendingPoList(poinput input)
        {
            GRNResponse res = new GRNResponse();

            try
            {
                
                var result = _grnService.GetPendingPo(input);

                res.Flag = 1;
                res.Message = "Success";
                res.data = result;

            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("polist")]
        public GRNResponse POList(PODetailsInput input)
        {
            GRNResponse res = new GRNResponse();

            try
            {

                

                var result = _grnService.GetPoList(input);


                res.Flag = 1;
                res.Message = "Success";
                res.Podetails = result.Podetails;
                res.LandedCosts = result.LandedCosts;

                return res; // Return the response
            }
            catch (Exception ex)
            {

                res.Flag = 0;
                res.Message = "Error: " + ex.Message; // Include error message


                return res; // Return the error response
            }
        }


        [HttpPost]
        [Route("insert")]
        public GRNSaveResponse Insert(GRN Data)
        {
            GRNSaveResponse res = new GRNSaveResponse();

            try
            {

                _grnService.Insert(Data);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }


        [HttpPost]
        [Route("update")]
        public GRNSaveResponse Update(GRN Data)
        {
            GRNSaveResponse res = new GRNSaveResponse();

            try
            {
                
                _grnService.Update(Data);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public GRN select(int id)
        {
            GRN objScheme = new GRN();
            try
            {
                
                objScheme = _grnService.GetGRN(id);
            }
            catch (Exception ex)
            {

            }

            return objScheme;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public GRNSaveResponse Delete(int id)
        {
            GRNSaveResponse res = new GRNSaveResponse();

            try
            {
                

                _grnService.Delete(id);
                res.Flag = 1;
                res.Message = "Success";

            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("verify")]
        public GRNResponse Verify(GRN Data)
        {
            GRNResponse res = new GRNResponse();

            try
            {
                
                _grnService.Verify(Data);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("list")]
        public GRNResponse List(GRNListRequest request)
        {
            GRNResponse res = new GRNResponse();
            List<GRN> grn = new List<GRN>();

            try
            {
                string apiKey = "";
                Int32 intUserID = 1;

                /*
                foreach (var header in Request.Headers)
                {
                    if (header.Key == "x-api-key")
                        apiKey = header.Value.ToList()[0];
                }

                
                
                User_DAL userDAL = new User_DAL();
                Int32 intUserID = userDAL.GetUserIDWithToken(apiKey);
                if (intUserID < 1)
                {
                    res.flag = "0";
                    res.message = "Invalid authorization key";
                    return res;
                }

                */

                
                grn = _grnService.GetGRNList(request);
                
                res.Flag = 1;
                res.Message = "Success";
                res.grnheader = grn;
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("approve")]
        public GRNResponse Approve(GRN Data)
        {
            GRNResponse res = new GRNResponse();

            try
            {
                
                _grnService.Approve(Data);
                res.Flag = 1;
                res.Message = "Success";
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
