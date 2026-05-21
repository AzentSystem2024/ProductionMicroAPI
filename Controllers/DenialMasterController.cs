using Microsoft.AspNetCore.Authorization;
using MicroApi.Models;
using MicroApi.DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;


namespace MicroApi.Controllers
{
//[Authorize]
[ApiController]
[Route("api/[controller]")]
    public class DenialMasterController : ControllerBase
    {
    private IDenialMasterService _Service;
    public DenialMasterController(IDenialMasterService Service)
    {
        _Service = Service;
    }
        [HttpPost]
        [Route("list")]
        public DenialMasterResponse List()
        {
            DenialMasterResponse res = new DenialMasterResponse();

            try
            {
                int intUserID = 1; 
                res.data = _Service.GetAllDenial(intUserID).ToList();

                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
                res.data = null;
            }

            return res;
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public DenialMasterResponse GetItems(int id)
        {
            DenialMasterResponse res = new DenialMasterResponse();

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
        public DenialMasterResponse Insert(DenialMaster objDenialMaster)
        {
            DenialMasterResponse res = new DenialMasterResponse();

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
                Int32 DenialID = _Service.Insert(objDenialMaster, intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = _Service.GetItems(DenialID);
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
        public DenialMasterResponse Update(DenialMaster objDenialMaster)
        {
            DenialMasterResponse res = new DenialMasterResponse();

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
                Int32 DenialID = _Service.Update(objDenialMaster, intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = _Service.GetItems(DenialID);
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
        public DenialMasterResponse Delete(int id)
        {
            DenialMasterResponse res = new DenialMasterResponse();
            DenialMaster objDenialMaster = new DenialMaster();

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
                _Service.DeleteDenialMaster(id, intUserID);

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

    }
}



