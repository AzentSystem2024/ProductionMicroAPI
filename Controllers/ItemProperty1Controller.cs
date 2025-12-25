using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemProperty1Controller : ControllerBase
    {
        private readonly IItemProperty1Service _itemProperty1Service;
        public ItemProperty1Controller(IItemProperty1Service itemProperty1Service)
        {
            _itemProperty1Service = itemProperty1Service;
        }
        [HttpPost]
        [Route("list")]
        public List<ItemProperty1> List(ItemPropertyList request)
        {
            List<ItemProperty1> itemProperty1 = new List<ItemProperty1>();

            try
            {
                
                itemProperty1 = _itemProperty1Service.GetAllItemProperty1(request);
            }
            catch (Exception ex)
            {
            }
            return itemProperty1.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public ItemProperty1 Select(int id)
        {
            ItemProperty1 objItemProperty1 = new ItemProperty1();
            try
            {
                
                objItemProperty1 = _itemProperty1Service.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objItemProperty1;
        }

        [HttpPost]
        [Route("save")]
        public ItemProperty1Response SaveData(ItemProperty1 itemProperty1Data)
        {
            ItemProperty1Response res = new ItemProperty1Response();

            try
            {
                
                Int32 ID = _itemProperty1Service.SaveData(itemProperty1Data);

                res.flag = "1";
                res.message = "Success";
                res.data = _itemProperty1Service.GetItems(ID);
            }
            catch (Exception ex)
            {
                res.flag = "1";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public ItemProperty1Response Delete(int id)
        {
            ItemProperty1Response res = new ItemProperty1Response();

            try
            {
                

                _itemProperty1Service.DeleteItemProperty1(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _itemProperty1Service.GetItems(id);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }
    }
}
