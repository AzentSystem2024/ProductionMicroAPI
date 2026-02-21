using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IImportTemplateService
    {
        public List<ImportTemplate> GetAllimport(Int32 intUserID);
        public Int32 Insert(ImportTemplate importTemplate, Int32 userID);
        public Int32 Update(ImportTemplate importTemplate, Int32 userID);
        public List<ImportTemplate> GetItems(int id);
        public bool DeleteImportTemplate(int id, int userID);
    }
}
