using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AC_CreditNoteController : ControllerBase
    {
        private readonly IAC_CreditNoteService _creditNoteService;

        public AC_CreditNoteController(IAC_CreditNoteService creditNoteService)
        {
            _creditNoteService = creditNoteService;
        }

        [HttpPost]
        [Route("insert")]
        public CreditNoteResponse Insert(AC_CreditNote model)
        {
            CreditNoteResponse res = new CreditNoteResponse();

            try
            {
                res = _creditNoteService.SaveCreditNote(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("update")]
        public CreditNoteResponse Update(AC_CreditNoteUpdate model)
        {
            CreditNoteResponse res = new CreditNoteResponse();

            try
            {
                res = _creditNoteService.UpdateCreditNote(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Update Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public CreditNoteListResponse GetCreditNoteList()
        {
            CreditNoteListResponse res = new CreditNoteListResponse();

            try
            {
                res = _creditNoteService.GetCreditNoteList();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<CreditNoteListItem>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public AC_CreditNoteSelect Select(int id)
        {
            AC_CreditNoteSelect response = new AC_CreditNoteSelect();
            try
            {
                response = _creditNoteService.GetCreditNoteById(id);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
    }
}