using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GSTReportController : ControllerBase
    {
        private readonly IGSTReportService _GSTReportService;
        public GSTReportController(IGSTReportService GSTReportService)
        {
            _GSTReportService = GSTReportService;
        }
        [HttpPost]
        [Route("Gstrpt")]
        public GSTReportResponse GetGSTReport(GSTReportRequest request)
        {
            var res = new GSTReportResponse();
            try
            {
                res = _GSTReportService.GetGSTReport(request);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("Gstb2cl")]
        public GSTB2CLReportResponse GetGSTB2CLReport(GSTReportRequest request)
        {
            var res = new GSTB2CLReportResponse();
            try
            {
                res = _GSTReportService.GetGSTB2CLReport(request);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("Gstcdnr")]
        public GSTReportResponse GetGSTReportCDNR(GSTReportRequest request)
        {
            var res = new GSTReportResponse();
            try
            {
                res = _GSTReportService.GetGSTReportCDNR(request);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
    }
}
