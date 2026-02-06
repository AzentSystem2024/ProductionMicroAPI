using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;
        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpPost]
        [Route("list")]
        public AttachmentResponse List(AttachmentInput attachments)
        {
            AttachmentResponse res = new AttachmentResponse();

            try
            {
                
                List<Attachments> attachmentList = _attachmentService.list(attachments); // Get the list of attachments

                res.flag = 1;
                res.Message = "Success";
                res.data = attachmentList; // Assign the list to the response's data property
            }
            catch (Exception ex)
            {
                res.flag = 0; // Set flag to 0 in case of an error
                res.Message = ex.Message;
                res.data = null; // Optionally set data to null or an empty list
            }

            return res;
        }

        [HttpPost]
        [Route("insert")]
        public AttachmentResponse SaveData(Attachments attachments)
        {
            AttachmentResponse res = new AttachmentResponse();

            try
            {
                
                Int32 ID = _attachmentService.Insert(attachments);

                res.flag = 1;
                res.Message = "Success";
                //  res.data = _attachmentService.GetItems(ID);
            }
            catch (Exception ex)
            {
                res.flag = 1;
                res.Message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public AttachmentResponse Delete(Attachments attachments)
        {
            AttachmentResponse res = new AttachmentResponse();
            try
            {
                
                _attachmentService.Delete(attachments); // Pass userId to the Delete method
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
