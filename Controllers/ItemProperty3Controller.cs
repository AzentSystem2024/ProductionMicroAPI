using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemProperty3Controller : ControllerBase
    {
        private readonly IItemProperty3Service _itemProperty2Service;
        public ItemProperty3Controller(IItemProperty3Service itemProperty2Service)
        {
            _itemProperty2Service = itemProperty2Service;
        }
        [HttpPost]
        [Route("list")]
        public List<ItemProperty3> List()
        {
            List<ItemProperty3> itemProperty2 = new List<ItemProperty3>();

            try
            {
                
                itemProperty2 = _itemProperty2Service.GetAllItemProperty3();
            }
            catch (Exception ex)
            {
            }
            return itemProperty2.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public ItemProperty3 Select(int id)
        {
            ItemProperty3 objItemProperty3 = new ItemProperty3();
            try
            {
                
                objItemProperty3 = _itemProperty2Service.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objItemProperty3;
        }

        [HttpPost]
        [Route("save")]
        public ItemProperty3Response SaveData(ItemProperty3 itemProperty2Data)
        {
            ItemProperty3Response res = new ItemProperty3Response();

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
        public ItemProperty3Response Delete(int id)
        {
            ItemProperty3Response res = new ItemProperty3Response();

            try
            {
                

                _itemProperty2Service.DeleteItemProperty3(id);
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
