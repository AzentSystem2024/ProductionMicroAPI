using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace MicroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalBookController : ControllerBase
    {
        private readonly IJournalBookService _journalBookService;

        public JournalBookController(IJournalBookService journalBookService)
        {
            _journalBookService = journalBookService;
        }

        [HttpPost]
        public IActionResult GetJournalBookData([FromBody] JournalBookRequest request)
        {
            try
            {
                // Parse the dates using the format "yyyy-MM-dd" to ignore the time component
                DateTime dateFrom = DateTime.ParseExact(request.DateFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime dateTo = DateTime.ParseExact(request.DateTo, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                if (request == null)
                {
                    return BadRequest("Request body cannot be null.");
                }

                var journalBookData = _journalBookService.GetJournalBookData(request.CompanyId, request.FinId, dateFrom, dateTo);
                return Ok(new { flag = 1, message = "Success", data = journalBookData });
            }
            catch (Exception ex)
            {
                return BadRequest(new { flag = 0, message = ex.Message });
            }
        }
    }
}
