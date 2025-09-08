using MicroApi.Dtos;
using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IAttachmentService
    {
        public Int32 Insert(Attachments attachments);
        public bool Delete(Attachments attachments);
        public List<Attachments> list(AttachmentInput attachments);
    }
}
