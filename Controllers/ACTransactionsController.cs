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
    public class ACTransactionsController : ControllerBase
    {
        private readonly IACTransactionsService _journalService;
        public ACTransactionsController(IACTransactionsService journalService)
        {
            _journalService = journalService;
        }
        [HttpPost]
        [Route("insert")]
        public JournalResponse Insert(JournalHeader journal)
        {
            JournalResponse res = new JournalResponse();

            try
            {
                res = _journalService.InsertJournal(journal); 
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
        public JournalResponse Update(JournalUpdateHeader header)
        {
            JournalResponse res = new JournalResponse();
            try
            {
                res = _journalService.UpdateJournal(header);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }


        [HttpPost]
        [Route("list")]
        public JournalListResponse JournalList(JVlistRequest request)
        {
            JournalListResponse res = new JournalListResponse();
            try
            {
                res = _journalService.GetJournalVoucherList(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
                res.Data = new List<JournalListItem>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public JournalResponse GetJournalById(int id)
        {
            JournalResponse res = new JournalResponse();

            try
            {
                res = _journalService.GetJournalById(id); 
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = null;
            }

            return res;
        }
        [HttpPost]
        [Route("voucherno")]
        public VoucherResponse GetLastVoucherNo()
        {
            VoucherResponse res = new VoucherResponse();

            try
            {
                res = _journalService.GetLastVoucherNo(); 
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.VoucherNo = null;
            }

            return res;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public JournalResponse DeleteJournal(int id)
        {
            JournalResponse res = new JournalResponse();
            try
            {
                res = _journalService.DeleteJournal(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("approve")]
        public JournalResponse commit(JournalUpdateHeader header)
        {
            JournalResponse res = new JournalResponse();

            try
            {
                res = _journalService.commit(header);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = null;
            }

            return res;
        }
        //------------------END------------// 

        [HttpPost]
        [Route("debitinsert")]
        public DebitNoteResponse DebitInsert(AC_DebitNote model)
        {
            DebitNoteResponse res = new DebitNoteResponse();

            try
            {
                res = _journalService.SaveDebitNote(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("debitupdate")]
        public DebitNoteResponse DebitUpdate(AC_DebitNoteUpdate model)
        {
            DebitNoteResponse res = new DebitNoteResponse();

            try
            {
                res = _journalService.UpdateDebitNote(model);  
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("debitlist")]
        public DebitNoteListResponse DebitList(DebitlistRequest request)
        {
            DebitNoteListResponse res = new DebitNoteListResponse();
            try
            {
                res = _journalService.GetDebitNoteList(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
                res.Data = new List<DebitNoteListItem>();
            }

            return res;
        }
        [HttpPost]
        [Route("debitselect/{id:int}")]
        public AC_DebitNoteSelect GetDebitNoteById(int id)
        {
            AC_DebitNoteSelect res = new AC_DebitNoteSelect();

            try
            {
                res = _journalService.GetDebitNoteById(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = null;
            }

            return res;
        }
        [HttpPost]
        [Route("commit")]
        public DebitNoteResponse Commit(AC_DebitNoteUpdate model)
        {
            DebitNoteResponse response = new DebitNoteResponse();
            try
            {
                response = _journalService.Commit(model);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("DocNo")]
        public DocNoResponse GetLastDocNo()
        {
            DocNoResponse res = new DocNoResponse();

            try
            {
                res = _journalService.GetLastDocNo();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("debitdelete/{id:int}")]
        public DebitNoteResponse Delete(int id)
        {
            DebitNoteResponse res = new DebitNoteResponse();
            try
            {
                res = _journalService.Delete(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("invoicelist")]
        public DebitInvoiceResponse GetPendingInvoiceList(DebitInvoiceRequest request)
        {
            DebitInvoiceResponse res = new DebitInvoiceResponse();

            try
            {
                res = _journalService.GetPendingInvoiceList(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<DebitInvoicelist>();
            }

            return res;
        }
    }
}

