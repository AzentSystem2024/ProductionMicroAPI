using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepreciationReportController : ControllerBase
    {
    private readonly IDepreciationReportService _service;

    public DepreciationReportController(IDepreciationReportService service)
    {
        _service = service;
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
}
}
