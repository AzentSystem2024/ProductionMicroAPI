using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImportItemInsertController : ControllerBase
    {
        private readonly IImportItemInsertService _importItemInsertService;
        public ImportItemInsertController(IImportItemInsertService importItemInsertService)
        {
            _importItemInsertService = importItemInsertService;
        }
        [HttpPost]
        [Route("insert")]
        public InsertImportItem Insert(List<InsertImportItem> insertImportItems)
        {
            InsertImportItem res = new InsertImportItem();

            try
            {
                _importItemInsertService.Insert(insertImportItems);
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
    }
}
