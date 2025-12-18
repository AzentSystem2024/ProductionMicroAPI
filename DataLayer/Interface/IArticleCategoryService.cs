using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IArticleCategoryService
    {
        ArticleCategoryListResponse GetArticleCategory(int Id);
        CategoryResponse GetCategoryPackingDetails(int Id);
        ArticleCategoryResponse InsertCategoryDetails(ArticleCategoryInsert request);
        ArticleCategoryResponse UpdateCategoryDetails(ArticleCategoryUpdate request);
        PackingListResponse GetPackingListByCategoryName(string categoryName);
        ArticleCategoryResponse SavePackingDetails(PackingSave request);
        ArticleCategoryResponse DeleteArticleCategoryData(int id);
        CategoryListResponse GetAllArticleCategories(ArticleCategoryListReq request);


    }
}
