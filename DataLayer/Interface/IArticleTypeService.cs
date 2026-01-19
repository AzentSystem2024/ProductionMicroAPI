using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IArticleTypeService
    {
        ArticleTypeResponse Insert(ArticleType articleType);
        ArticleTypeResponse Update(ArticleTypeUpdate articleType);
        ArticleTypeResponse GetArticleTypeById(int id);
        ArticleTypeListResponse GetLogList();
        ArticleTypeResponse DeleteArticleTypeData(int id);
    }
}
