using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleCategoryController : ControllerBase
    {
        private readonly IArticleCategoryService _articlecategoryService;
        public ArticleCategoryController(IArticleCategoryService articlecategoryService)
        {
            _articlecategoryService = articlecategoryService;
        }
        [HttpPost]
        [Route("list/{id:int}")]
        public ArticleCategoryListResponse ArticleCategoryLogList(int Id)
        {

            ArticleCategoryListResponse res = new ArticleCategoryListResponse();
            try
            {
                res = _articlecategoryService.GetArticleCategory(Id);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("select/{Id}")]
        public CategoryResponse GetCategoryPackingList(int Id)
        {
            CategoryResponse res = new CategoryResponse();
            try
            {
                res = _articlecategoryService.GetCategoryPackingDetails(Id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("insert")]
        public ArticleCategoryResponse InsertCategory(ArticleCategoryInsert request)
        {
            var res = new ArticleCategoryResponse();

            try
            {
                res = _articlecategoryService.InsertCategoryDetails(request); 
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = $"Error: {ex.Message}";
            }

            return res;
        }
        [HttpPost]
        [Route("update")]

        public ArticleCategoryResponse Update(ArticleCategoryUpdate request)
        {
            ArticleCategoryResponse res = new ArticleCategoryResponse();
            try
            {
                res = _articlecategoryService.UpdateCategoryDetails(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("listpacking")]
        public PackingListResponse GetPackingListByCategory(PackingListRequest request)
        {
            var res = new PackingListResponse();

            try
            {
                res = _articlecategoryService.GetPackingListByCategoryName(request.CATEGORY_NAME);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = $"Error: {ex.Message}";
            }

            return res;
        }


        [HttpPost]
        [Route("save")]
        public ArticleCategoryResponse InsertPacking(PackingSave request)
        {
            var res = new ArticleCategoryResponse();

            try
            {
                res = _articlecategoryService.SavePackingDetails(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = $"Error: {ex.Message}";
            }

            return res;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public ArticleCategoryResponse Delete(int id)
        {
            ArticleCategoryResponse res = new ArticleCategoryResponse();
            try
            {
                res = _articlecategoryService.DeleteArticleCategoryData(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("listdata")]
        public CategoryListResponse GetArticleCategoryList()
        {
            CategoryListResponse res = new CategoryListResponse();

            try
            {
                res = _articlecategoryService.GetAllArticleCategories();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }

    }
}

