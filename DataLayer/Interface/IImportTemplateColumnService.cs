using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IImportTemplateColoumnService
    {
        public List<ImportTemplateColoumns> GetAllTemplateColoumns(Int32 intUserID);
    }
}
