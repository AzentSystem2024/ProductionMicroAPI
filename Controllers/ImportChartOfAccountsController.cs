using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImportChartOfAccountsController : ControllerBase
    {
        private readonly IImportChartOfAccountsService _importItemLogService;
        public ImportChartOfAccountsController(IImportChartOfAccountsService importItemLogService)
        {
            _importItemLogService = importItemLogService;
        }


        [HttpPost]
        [Route("import")]
        public ImportAccountsResponse Import(ImportAccountsInput vinput)
        {
            ImportAccountsResponse res = new ImportAccountsResponse();

            try
            {
                
                _importItemLogService.Import(vinput);

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
        [Route("list")]
        public ImportAccountsResponse List(ImportAccountsInput vinput)
        {
            ImportAccountsResponse res = new ImportAccountsResponse();

            try
            {

                res = _importItemLogService.List(vinput);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("view")]
        public viewImportAccountsDataResponse ViewDetails(viewImportAccountsInput vInput)
        {
            viewImportAccountsDataResponse res = new viewImportAccountsDataResponse();
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

    }
}
