using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IArticleBrandService
    {
        ArticleBrandResponse Insert(ArticleBrand articleBrand);
        ArticleBrandResponse Update(ArticleBrandUpdate articleBrand);
        ArticleBrandResponse GetArticleBrandById(int id);
        ArticleBrandListResponse GetLogList(ArticleBrandListRequest request);
        ArticleBrandResponse DeleteArticleBrandData(int id);
    }
}
