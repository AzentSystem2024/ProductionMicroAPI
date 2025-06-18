using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISchemaService
    {
        SchemaResponse Insert(Schema schema);
        SchemaResponse Update(SchemaUpdate schema);
        SchemaResponse GetSchemaById(int id);
        SchemaListResponse GetLogList(int? id = null);
        SchemaResponse DeleteSchemaData(int id);
    }
}
