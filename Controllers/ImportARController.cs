using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImportARController : ControllerBase
    {
        private readonly IImportARService _importItemLogService;
        public ImportARController(IImportARService importItemLogService)
        {
            _importItemLogService = importItemLogService;
        }


        [HttpPost]
        [Route("import")]
        public ImportARResponse Import(ImportARInput vinput)
        {
            ImportARResponse res = new ImportARResponse();

            try
            {
                
                _importItemLogService.Import(vinput);

                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("list")]
        public ImportARResponse List(ImportARInput vinput)
        {
            ImportARResponse res = new ImportARResponse();

            try
            {

                res = _importItemLogService.List(vinput);

            }
            catch (Exception ex)
            {
                res.flag =  "0";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("view")]
        public viewImportARDataResponse ViewDetails(viewImportARInput vInput)
        {
            viewImportARDataResponse res = new viewImportARDataResponse();
            try
            {
                res = _importItemLogService.ViewDetails(vInput);
            }
            catch(Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }
            return res;
        }


        [HttpPost]
        [Route("columns")]
        public ImportArColumnsResponse columnsList()
        {
            ImportArColumnsResponse res = new ImportArColumnsResponse();

            try
            {

                res = _importItemLogService.columnsList();

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
