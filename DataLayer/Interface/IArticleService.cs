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
        ArticleResponse GetArticleById(int id);
        ArticleListResponse GetArticleList();
        ListItemsResponse GetItems();
        ItemdataResponse GetItemByCode(ItemcodeRequest request);
        ArticleResponse DeleteArticleData(DeleteArticleRequest request);
        string GetLastOrderNoByUnitId();
        string GetAliasNo();
        //ArticleUpdate GetArticleDetails(int unitId, string artNo, string color, int categoryId, decimal price);
    }
}
