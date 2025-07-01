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
    public class UserRoleController : ControllerBase
    {
    private IUserRoleService _Service;
    public UserRoleController(IUserRoleService Service)
    {
        _Service = Service;
    }
        [HttpPost]
        [Route("list")]
        public UserRoleResponse List()
        {
            UserRoleResponse res = new UserRoleResponse();
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
                    res.flag = 0;
                    res.message = "Invalid authorization key";
                    return res;
                }
                */
                int userId = 1;
                res = _Service.GetAllUserRoles(intUserID); // Correctly assign the UserRoleResponse

                res.flag = 1;
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }



        [HttpPost]
        [Route("menulist")]
        public UserMenuResponse MenuList()
        {
            UserMenuResponse res = new UserMenuResponse();
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
                    res.Flag = "0";
                    res.Message = "Invalid authorization key";
                    return res;
                }
                */

                var usermenusResponse = _Service.GetAllUsermainRoles(intUserID);

                res.Flag = usermenusResponse.Flag;
                res.Message = usermenusResponse.Message;
                res.ID = usermenusResponse.ID;
                res.UserRoles = usermenusResponse.UserRoles;
                res.IsInactive = usermenusResponse.IsInactive;
                res.Data = usermenusResponse.Data;
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }


        [HttpPost]
        [Route("insert")]
        public UserMenuResponse Insert(UserRole objuserrole)
        {
            UserMenuResponse res = new UserMenuResponse();

            string apiKey = "";
           // Int32 intUserID = 1;

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
            if (objuserrole == null || objuserrole.usermenulist == null || !objuserrole.usermenulist.Any())
            {
                res.Flag = 0;
                res.Message = "User role data or menu list is required.";
                return res;
            }


            try
            {
                Int32 intUserID = 1;
                Int32 UserroleID = _Service.Insert(objuserrole, intUserID);

                res.Flag = 1;
                res.Message = "Success";
                //res.Data = _Service.GetItems(UserroleID);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
                return res;
            }

            return res;
        }


        [HttpPost]
        [Route("update")]
        public UserMenuResponse Update(UserRole objuserrole)
        {
            UserMenuResponse res = new UserMenuResponse();

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
                Int32 UserroleID = _Service.Update(objuserrole, intUserID);

                res.Flag = 1;
                res.Message = "Success";
                //res.Data = _Service.GetItems(UserroleID);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
                return res;
            }
            return res;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public UserRoleResponse Delete(int id)
        {
            UserRoleResponse res = new UserRoleResponse();
            try
            {
                return _Service.DeleteUserRole(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }
            return res;
        }

        //public UserMenuResponse Delete(int id)
        //{
            UserMenuResponse res = new UserMenuResponse();
            //UserRole objUserRole = new UserRole();

            //string apiKey = "";
           //int intUserID = 0;

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

        //    try
        //    {
                

        //        //res.Flag = 1;
        //        //res.Message = "Success";
        //        res = _Service.DeleteUserRole(id);

        //    }
        //    catch (Exception ex)
        //    {
        //        res.Flag = 0;
        //        res.Message = ex.Message;
               
        //    }

        //    return res;
        //}

        [HttpPost]
        [Route("select/{id:int}")]
        public UserRole GetItems(int id)
        {
            UserRole res = new UserRole();

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
                //res.flag = 1;
                //res.Message = "Success";
                res = _Service.GetItems(id);
            }
            catch (Exception ex)
            {
                //res.Flag = 0;
                //res.Message = ex.Message;
                return res;
            }

            return res;
        }


    }
}



