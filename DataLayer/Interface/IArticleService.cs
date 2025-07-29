using System.Security.Principal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroApi.Models;
namespace MicroApi.DataLayer.Interface
{
    public interface IArticleService
    {
        ArticleResponse Insert(Article article);
        ArticleResponse Update(ArticleUpdate article);
        ArticleResponse GetArticleById(ArticleSelectRequest request);
        ArticleListResponse GetLogList();
        ArticleResponse DeleteArticleData(int id);
        string GetLastOrderNoByUnitId(int unitId);
        //ArticleUpdate GetArticleDetails(int unitId, string artNo, string color, int categoryId, decimal price);
    }
}
