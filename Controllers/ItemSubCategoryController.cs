using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemSubCategoryController : ControllerBase
    {
        private readonly IItemSubCategoryService _itemSubCategoryService;
        public ItemSubCategoryController(IItemSubCategoryService itemSubCategoryService)
        {
            _itemSubCategoryService = itemSubCategoryService;
        }
        [HttpPost]
        [Route("list")]
        public List<ItemSubCategory> List()
        {
            List<ItemSubCategory> itemSubCategories = new List<ItemSubCategory>();

            try
            {
                
                itemSubCategories = _itemSubCategoryService.GetAllItemSubCategory();
            }
            catch (Exception ex)
            {
            }
            return itemSubCategories.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public ItemSubCategory Select(int id)
        {
            ItemSubCategory objItemSubCategory = new ItemSubCategory();
            try
            {
                
                objItemSubCategory = _itemSubCategoryService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objItemSubCategory;
        }

        [HttpPost]
        [Route("save")]
        public ItemSubCategoryResponse SaveData(ItemSubCategory itemsubcategoryData)
        {
            ItemSubCategoryResponse res = new ItemSubCategoryResponse();

            try
            {
                
                Int32 ID = _itemSubCategoryService.SaveData(itemsubcategoryData);

                res.flag = "1";
                res.message = "Success";
                res.data = _itemSubCategoryService.GetItems(ID);
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
        public ItemSubCategoryResponse Delete(int id)
        {
            ItemSubCategoryResponse res = new ItemSubCategoryResponse();

            try
            {
                

                _itemSubCategoryService.DeleteItemSubCategory(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _itemSubCategoryService.GetItems(id);
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
