using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemProperty2Controller : ControllerBase
    {
        private readonly IItemProperty2Service _itemProperty2Service;
        public ItemProperty2Controller(IItemProperty2Service itemProperty2Service)
        {
            _itemProperty2Service = itemProperty2Service;
        }
        [HttpPost]
        [Route("list")]
        public List<ItemProperty2> List()
        {
            List<ItemProperty2> itemProperty2 = new List<ItemProperty2>();

            try
            {
                
                itemProperty2 = _itemProperty2Service.GetAllItemProperty2();
            }
            catch (Exception ex)
            {
            }
            return itemProperty2.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public ItemProperty2 Select(int id)
        {
            ItemProperty2 objItemProperty2 = new ItemProperty2();
            try
            {
                
                objItemProperty2 = _itemProperty2Service.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objItemProperty2;
        }

        [HttpPost]
        [Route("save")]
        public ItemProperty2Response SaveData(ItemProperty2 itemProperty2Data)
        {
            ItemProperty2Response res = new ItemProperty2Response();

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
        public ItemProperty2Response Delete(int id)
        {
            ItemProperty2Response res = new ItemProperty2Response();

            try
            {
                

                _itemProperty2Service.DeleteItemProperty2(id);
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
