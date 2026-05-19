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
    public class ClinicianController : ControllerBase
    {
    private IClinicianService _Service;
    public ClinicianController(IClinicianService Service)
    {
        _Service = Service;
    }

        [HttpPost]
        [Route("list")]
        public ClinicianResponse List()
        {
            ClinicianResponse res = new ClinicianResponse();
            List<Clinician> clinician = new List<Clinician>();         

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

                clinician = _Service.GetAllClinicians(intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = clinician; // Assuming FacilityGroups property exists in FacilityGroupResponse
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
        public ClinicianResponse GetItems(int id)
        {
            ClinicianResponse res = new ClinicianResponse();
            
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
        public ClinicianResponse Insert(Clinician objClinician)
        {
            ClinicianResponse res = new ClinicianResponse();
             

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
                Int32 ClinicianID = _Service.Insert(objClinician, intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = _Service.GetItems(ClinicianID);
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
        public ClinicianResponse Update(Clinician objClinician)
        {
            ClinicianResponse res = new ClinicianResponse();

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
                Int32 ClinicianID = _Service.Update(objClinician, intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = _Service.GetItems(ClinicianID);
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
        public ClinicianResponse Delete(int id)
        {
            ClinicianResponse res = new ClinicianResponse();
            Clinician objClinician = new Clinician();

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
                _Service.DeleteClinicians(id, intUserID);

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



