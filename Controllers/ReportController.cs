using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;
        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpPost("pdclist")]
        public PDCListReportResponse GetPDCList(PDCListReportRequest request)
        {
            var response = new PDCListReportResponse();

            try
            {
                response = _service.GetPDCList(request);
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        [HttpPost("fixedassetreport")]
        public FixedAssetReportResponse GetFixedAssetReport(FixedAssetReportRequest request)
        {
            var response = new FixedAssetReportResponse();

            try
            {
                response = _service.GetFixedAssetReport(request);
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost("depreciationreport")]
        public DepreciationReportResponse GetDepreciationReport(DepreciationReportRequest request)
        {
            var response = new DepreciationReportResponse();

            try
            {
                response = _service.GetDepreciationReport(request);
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        [HttpPost("prepaymentreport")]
        public PrepaymentReportResponse GetPrepaymentReport(PrepaymentReportRequest request)
        {
            var response = new PrepaymentReportResponse();

            try
            {
                response = _service.GetPrepaymentReport(request);
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

