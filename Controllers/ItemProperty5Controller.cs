using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemProperty5Controller : ControllerBase
    {
        private readonly IItemProperty5Service _itemProperty2Service;
        public ItemProperty5Controller(IItemProperty5Service itemProperty2Service)
        {
            _itemProperty2Service = itemProperty2Service;
        }
        [HttpPost]
        [Route("list")]
        public List<ItemProperty5> List()
        {
            List<ItemProperty5> itemProperty2 = new List<ItemProperty5>();

            try
            {
                
                itemProperty2 = _itemProperty2Service.GetAllItemProperty5();
            }
            catch (Exception ex)
            {
            }
            return itemProperty2.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public ItemProperty5 Select(int id)
        {
            ItemProperty5 objItemProperty5 = new ItemProperty5();
            try
            {
                
                objItemProperty5 = _itemProperty2Service.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objItemProperty5;
        }

        [HttpPost]
        [Route("save")]
        public ItemProperty5Response SaveData(ItemProperty5 itemProperty2Data)
        {
            ItemProperty5Response res = new ItemProperty5Response();

            try
            {
                
                Int32 ID = _itemProperty2Service.SaveData(itemProperty2Data);

                res.flag = "1";
                res.message = "Success";
                res.data = _itemProperty2Service.GetItems(ID);
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
        public ItemProperty5Response Delete(int id)
        {
            ItemProperty5Response res = new ItemProperty5Response();

            try
            {
                

                _itemProperty2Service.DeleteItemProperty5(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _itemProperty2Service.GetItems(id);
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
