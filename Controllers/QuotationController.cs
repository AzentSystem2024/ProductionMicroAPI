﻿using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationController : ControllerBase
    {
        private readonly IQuotationService _quotationService;

        public QuotationController(IQuotationService quotationService)
        {
            _quotationService = quotationService;
        }

        [HttpPost]
        [Route("list")]
        public QuotationListResponse List()
        {
            QuotationListResponse response = new QuotationListResponse();
            try
            {
                response = _quotationService.GetAllQuotations();
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
                response.Data = new List<QuotationList>();
            }
            return response;
        }

        [HttpPost]
        [Route("select/{qtnId:int}")]
        public IActionResult Select(int qtnId)
        {
            try
            {
                QuotationDetailResponse response = _quotationService.GetQuotation(qtnId);
                if (response.Flag == 1)
                    return Ok(response);
                else
                    return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new QuotationDetailResponse { Flag = 0, Message = ex.Message, Data = new Quotation { Details = new List<QuotationDetail>() } });
            }
        }

        [HttpPost]
        [Route("save")]
        public QuotationResponse SaveData([FromBody] Quotation quotation)
        {
            QuotationResponse res = new QuotationResponse();
            try
            {
                _quotationService.SaveData(quotation);
                res.Flag = "1";
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = "0";
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult EditData([FromBody] QuotationUpdate quotationUpdate)
        {
            try
            {
                _quotationService.EditData(quotationUpdate);
                return Ok(new { Flag = "1", Message = "Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Flag = "0", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("delete/{qtnId:int}")]
        public QuotationResponse Delete(int qtnId)
        {
            QuotationResponse response = new QuotationResponse();
            try
            {
                bool success = _quotationService.DeleteQuotation(qtnId);
                if (success)
                {
                    response.Flag = "1";
                    response.Message = "Success";
                }
                else
                {
                    response.Flag = "0";
                    response.Message = "Failed to delete";
                }
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("list-items")]
        public IActionResult ListItems([FromBody] QuotationRequest request)
        {
            try
            {
                var response = _quotationService.GetQuotationItems(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
            }
        }
    }
}

