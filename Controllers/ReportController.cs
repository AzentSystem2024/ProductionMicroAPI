using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using System.Data.SqlClient;
using MicroApi.Models;

namespace RetailApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _ReportService;
        public ReportController(IReportService ReportService)
        {
            _ReportService = ReportService;
        }
        [HttpPost]
        [Route("initData")]
        public ReportListData InitData()
        {
            ReportListData res = new ReportListData();
            try
            {
                // Call DAL to get data
                
                res = _ReportService.GetReportInitData();
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
        [Route("insert")]
        public SaveReportResponse SaveReport(Report report)
        {
            SaveReportResponse res = new SaveReportResponse();



            try
            {
                
                res = _ReportService.Insert(report);
                res.flag = 1;
                res.Message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;

            }

            return res;
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public Report select(int id)
        {
            Report res = new Report();
            try
            {
                
                res = _ReportService.GetReportById(id);
                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;

            }

            return res;
        }

        [HttpPost]
        [Route("update")]
        public SaveReportResponse Update(Report Data)
        {
            SaveReportResponse res = new SaveReportResponse();

            try
            {
                
                res = _ReportService.Update(Data);
                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
    }
}
