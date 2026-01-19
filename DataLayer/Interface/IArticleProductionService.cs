using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IArticleProductionService
    {
        ArticleProdResponse Insert(ArticleProduction model);
        ArticleBomResponse GetArticleBomList(ArticleBomRequest model);
        ProductionViewResponse GetProductionById(int id);
        ProductionListResponse articleprodlist(ProductionListRequest model);
    }
}
