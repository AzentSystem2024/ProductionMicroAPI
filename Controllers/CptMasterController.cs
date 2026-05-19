using Microsoft.AspNetCore.Authorization;
using MicroApi.Models;
using MicroApi.DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CptMasterController : ControllerBase
    {
    private ICptMasterService _Service;
    public CptMasterController(ICptMasterService Service)
    {
        _Service = Service;
    }
        [HttpPost]
        [Route("list")]
        public CptMasterResponse List()
        {
            CptMasterResponse res = new CptMasterResponse();
            List<CptMaster> cptMasters = new List<CptMaster>();

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

                cptMasters = _Service.GetAllCptMasters(intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = cptMasters; // Assuming FacilityGroups property exists in FacilityGroupResponse
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }


        [HttpPost]
        [Route("select/{id:int}")]
        public CptMasterResponse GetItems(int id)
        {
            CptMasterResponse res = new CptMasterResponse();

            string apiKey = "";
            Int32 intUserID = 1;

            try
            {
                res.flag = "1";
                res.message = "Success";
                res.data = _Service.GetItems(id);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
                return res;
            }

            return res;
        }
        [HttpPost]
        [Route("insert")]
        public CptMasterResponse Insert(CptMaster objCptMaster)
        {
            CptMasterResponse res = new CptMasterResponse();

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

            try
            {
                Int32 CptMasterID = _Service.Insert(objCptMaster, intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = _Service.GetItems(CptMasterID);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
                return res;
            }

            return res;
        }

        [HttpPost]
        [Route("update")]
        public CptMasterResponse Update(CptMaster objCptMaster)
        {
            CptMasterResponse res = new CptMasterResponse();

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

            try
            {
                Int32 CptMasterID = _Service.Update(objCptMaster, intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = _Service.GetItems(CptMasterID);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
                return res;
            }

            return res;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public CptMasterResponse Delete(int id)
        {
            CptMasterResponse res = new CptMasterResponse();
            CptMaster objCptMaster = new CptMaster();

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

            try
            {
                _Service.DeleteCptMaster(id, intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = _Service.GetItems(id);

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
                return res;
            }

            return res;
        }

        [HttpPost]
        [Route("getSubDepartment")]
        public subDepartmentResponse getSubDepartment(CptMaster vInp)
        {
            subDepartmentResponse res = new subDepartmentResponse();

            try
            {
      
                 res=_Service.getSubDepartment(vInp);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
                return res;
            }

            return res;
        }
    }
}



