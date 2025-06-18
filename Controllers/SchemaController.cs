using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchemaController : ControllerBase
    {
             private readonly ISchemaService _schemaService;
            public SchemaController(ISchemaService schemaService)
            {
              _schemaService = schemaService;
            }
            [HttpPost]
            [Route("insert")]
            public SchemaResponse Insert(Schema schema)
            {
            SchemaResponse res = new SchemaResponse();
                try
                {
                    res = _schemaService.Insert(schema);
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
        public SchemaResponse Update(SchemaUpdate schema)
        {
            SchemaResponse res = new SchemaResponse();
            try
            {
                res = _schemaService.Update(schema);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public SchemaResponse select(int id)
        {
            SchemaResponse res = new SchemaResponse();
            try
            {
                res = _schemaService.GetSchemaById(id);
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
        public SchemaListResponse SchLogList()
        {

            SchemaListResponse res = new SchemaListResponse();
            try
            {
                res = _schemaService.GetLogList();

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public SchemaResponse Delete(int id)
        {
            SchemaResponse res = new SchemaResponse();
            try
            {
              res =  _schemaService.DeleteSchemaData(id);
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
