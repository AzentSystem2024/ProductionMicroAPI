using Microsoft.AspNetCore.Authorization;
using MicroApi.Models;
using MicroApi.DataLayer.Interface;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Configuration;
namespace MicroApi.Controllers
{
    //[Authorize]
[ApiController]
[Route("api/arreport")]
    public class RptARController : ControllerBase
    {
    private IRptARService _Service;
    public RptARController(IRptARService Service)
    {
        _Service = Service;
    }

        [HttpPost]
        [Route("getReportData")]
        public RptARResponseOutput claimDetailsReport(RptARInput vInput)
        {
            RptARResponseOutput rpt = new RptARResponseOutput();

            try
            {
                rpt = _Service.claimDetailsReport(vInput);

            }
            catch (Exception ex)
            {

            }
            return rpt;

        }




    }
     
}



