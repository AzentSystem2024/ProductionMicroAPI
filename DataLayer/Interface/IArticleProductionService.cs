using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IArticleProductionService
    {
        ArticleProdResponse Insert(ArticleProduction model);
    }
}
