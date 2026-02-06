using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemProperty4Controller : ControllerBase
    {
        private readonly IItemProperty4Service _itemProperty2Service;
        public ItemProperty4Controller(IItemProperty4Service itemProperty2Service)
        {
            _itemProperty2Service = itemProperty2Service;
        }
        [HttpPost]
        [Route("list")]
        public List<ItemProperty4> List(ItemPropertyList request)
        {
            List<ItemProperty4> itemProperty2 = new List<ItemProperty4>();

            try
            {
                
                itemProperty2 = _itemProperty2Service.GetAllItemProperty4(request);
            }
            catch (Exception ex)
            {
            }
            return itemProperty2.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public ItemProperty4 Select(int id)
        {
            ItemProperty4 objItemProperty4 = new ItemProperty4();
            try
            {
                
                objItemProperty4 = _itemProperty2Service.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objItemProperty4;
        }

        [HttpPost]
        [Route("save")]
        public ItemProperty4Response SaveData(ItemProperty4 itemProperty2Data)
        {
            ItemProperty4Response res = new ItemProperty4Response();

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
        public ItemProperty4Response Delete(int id)
        {
            ItemProperty4Response res = new ItemProperty4Response();

            try
            {
                

                _itemProperty2Service.DeleteItemProperty4(id);
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
