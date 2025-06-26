using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
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
        public JournalListResponse JournalList()
        {
            JournalListResponse res = new JournalListResponse();
            try
            {
                res = _journalService.GetJournalVoucherList();
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
        public JournalResponse UpdateApprovalStatus(ApprovalRequest request)
        {
            JournalResponse res = new JournalResponse();

            try
            {
                res = _journalService.JournalApproval(request.TRANS_ID, request.IS_APPROVED ?? false);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = null;
            }

            return res;
        }


    }
}

