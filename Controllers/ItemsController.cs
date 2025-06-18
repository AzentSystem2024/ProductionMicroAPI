using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly IItemsService _itemsService;
        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }
        [HttpPost]
        [Route("insert")]
        public ItemResponse Insert(Item item)
        {
            ItemResponse res = new ItemResponse();
            try
            {
                res = _itemsService.Insert(item);
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
        public ItemResponse Update(ItemUpdate item)
        {
            ItemResponse res = new ItemResponse();
            try
            {
                res = _itemsService.Update(item);
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
        public ItemResponse select(int id)
        {
            ItemResponse res = new ItemResponse();
            try
            {
                res = _itemsService.GetItemById(id);
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
        public ItemListResponse ItmLogList()
        {

            ItemListResponse res = new ItemListResponse();
            try
            {
                res = _itemsService.GetLogList();

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
        public ItemResponse Delete(int id)
        {
            ItemResponse res = new ItemResponse();
            try
            {
                res = _itemsService.DeleteItemData(id);
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
