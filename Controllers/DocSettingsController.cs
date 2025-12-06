using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocSettingsController : ControllerBase
    {
        private readonly IDocSettingsService _docSettingsService;

        public DocSettingsController(IDocSettingsService docSettingsService)
        {
            _docSettingsService = docSettingsService;
        }
        [HttpPost]
        [Route("insert")]
        public DocSettingsResponse Insert(DocSettings model)
        {
            DocSettingsResponse res = new DocSettingsResponse();

            try
            {
                res = _docSettingsService.Insert(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public DocSettingsListResponse List(DocSettingsListRequest request)
        {
            DocSettingsListResponse res = new DocSettingsListResponse();

            try
            {
                res = _docSettingsService.List(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<DocSettingsList>();
            }

            return res;
        }
    }
}
