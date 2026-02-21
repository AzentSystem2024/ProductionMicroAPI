using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImportItemLogController : ControllerBase
    {
        private readonly IImportItemLogService _importItemLogService;
        public ImportItemLogController(IImportItemLogService importItemLogService)
        {
            _importItemLogService = importItemLogService;
        }
        [HttpPost]
        public List<ImportItemLog> List()
        {
            List<ImportItemLog> importItemLog = new List<ImportItemLog>();

            try
            {
                
                importItemLog = _importItemLogService.GetAllItemLog();
            }
            catch (Exception ex)
            {
            }
            return importItemLog.ToList();
        }


        [HttpPost]
        [Route("insert")]
        public ImportItemResponse Insert(ImportItemLog itemsData)
        {
            ImportItemResponse res = new ImportItemResponse();

            try
            {
                
                _importItemLogService.Insert(itemsData);

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
        [Route("ItemLogEntryList")]
        public List<InsertImportItemLogEntry> LogEntryList(ImportItemLog itemLog)
        {
            List<InsertImportItemLogEntry> importItemLogEntry = new List<InsertImportItemLogEntry>();

            try
            {
                
                importItemLogEntry = _importItemLogService.GetAllItemLogEntry(itemLog);
            }
            catch (Exception ex)
            {
            }
            return importItemLogEntry.ToList();
        }
    }
}
