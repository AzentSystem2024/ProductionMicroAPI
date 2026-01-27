using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IArticleProductionService
    {
        ArticleProdResponse Insert(ArticleProduction model);
        ArticleProdResponse Update(ArticleProductionUpdate model);
        ArticleProdResponse commit(ArticleProductionUpdate model);
        ArticleProdResponse Delete(int id);
        ArticleBomResponse GetArticleBomList(ArticleBomRequest model);
        ProductionViewResponse GetProductionById(int id);
        ProductionListResponse articleprodlist(ProductionListRequest model);
    }
}
