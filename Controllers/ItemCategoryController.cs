using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemCategoryController : ControllerBase
    {
        private readonly IItemCategoryService _itemCategoryService;
        public ItemCategoryController(IItemCategoryService itemCategoryService)
        {
            _itemCategoryService = itemCategoryService;
        }
        [HttpPost]
        [Route("list")]
        public List<ItemCategory> List()
        {
            List<ItemCategory> itemCategories = new List<ItemCategory>();

            try
            {
                
                itemCategories = _itemCategoryService.GetAllItemCategory();
            }
            catch (Exception ex)
            {
            }
            return itemCategories.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public ItemCategory Select(int id)
        {
            ItemCategory objItemCategory = new ItemCategory();
            try
            {
                
                objItemCategory = _itemCategoryService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objItemCategory;
        }

        [HttpPost]
        [Route("save")]
        public ItemCategoryResponse SaveData(ItemCategory itemcategoryData)
        {
            ItemCategoryResponse res = new ItemCategoryResponse();

            try
            {
                
                Int32 ID = _itemCategoryService.SaveData(itemcategoryData);

                res.flag = "1";
                res.message = "Success";
                res.data = _itemCategoryService.GetItems(ID);
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
        public ItemCategoryResponse Delete(int id)
        {
            ItemCategoryResponse res = new ItemCategoryResponse();

            try
            {
                

                _itemCategoryService.DeleteItemCategory(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _itemCategoryService.GetItems(id);
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
