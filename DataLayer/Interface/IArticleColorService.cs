using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IArticleColorService
    {
        ArticleColorResponse Insert(ArticleColor articleColor);
        ArticleColorResponse Update(ArticleColorUpdate articleColor);
        ArticleColorResponse GetArticleColorById(int id);
        ArticleColorListResponse GetLogList(int? id = null);
        ArticleColorResponse DeleteArticleColorData(int id);
    }
}
