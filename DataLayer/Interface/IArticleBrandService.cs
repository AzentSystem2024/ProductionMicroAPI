using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IArticleBrandService
    {
        ArticleBrandResponse Insert(ArticleBrand articleBrand);
        ArticleBrandResponse Update(ArticleBrandUpdate articleBrand);
        ArticleBrandResponse GetArticleBrandById(int id);
        ArticleBrandListResponse GetLogList(int? id = null);
        ArticleBrandResponse DeleteArticleBrandData(int id);
    }
}
