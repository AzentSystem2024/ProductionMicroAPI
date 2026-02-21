using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImportTemplateColoumnController : ControllerBase
    {
        private readonly IImportTemplateColoumnService _importTemplateColoumnService;
        public ImportTemplateColoumnController(IImportTemplateColoumnService importTemplateColoumnService)
        {
            _importTemplateColoumnService = importTemplateColoumnService;
        }
        [HttpPost]
        [Route("list")]
        public ImportTemplateColoumnResponse List()
        {
            ImportTemplateColoumnResponse res = new ImportTemplateColoumnResponse();
            List<ImportTemplateColoumns> impTemplateColoumns = new List<ImportTemplateColoumns>();
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

                
                impTemplateColoumns = _importTemplateColoumnService.GetAllTemplateColoumns(intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = impTemplateColoumns;
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }
    }
}
