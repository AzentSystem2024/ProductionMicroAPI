using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImportTemplateController : ControllerBase
    {
        private readonly IImportTemplateService _importTemplateService;
        public ImportTemplateController(IImportTemplateService importTemplateService)
        {
            _importTemplateService = importTemplateService;
        }
        [HttpPost]
        [Route("list")]
        public ImportTemplateResponse List()
        {
            ImportTemplateResponse res = new ImportTemplateResponse();
            List<ImportTemplate> importTemplates = new List<ImportTemplate>();

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

                
                importTemplates = _importTemplateService.GetAllimport(intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = importTemplates;
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
        public ImportTemplateResponse Select(int id)
        {
            ImportTemplateResponse res = new ImportTemplateResponse();

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
                res.data = _importTemplateService.GetItems(id);
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
        public ImportTemplateResponse Insert(ImportTemplate objimptemplate)
        {
            ImportTemplateResponse res = new ImportTemplateResponse();


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
                
                Int32 TemplateID = _importTemplateService.Insert(objimptemplate, intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = _importTemplateService.GetItems(TemplateID);
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
        public ImportTemplateResponse Update(ImportTemplate objimptemplate)
        {
            ImportTemplateResponse res = new ImportTemplateResponse();


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
                
                Int32 TemplateID = _importTemplateService.Update(objimptemplate, intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = _importTemplateService.GetItems(TemplateID);
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
        public ImportTemplateResponse Delete(int id)
        {
            ImportTemplateResponse res = new ImportTemplateResponse();
            ImportTemplate objiimptemplate = new ImportTemplate();

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
                
                _importTemplateService.DeleteImportTemplate(id, intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = _importTemplateService.GetItems(id);

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
