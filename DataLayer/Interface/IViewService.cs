using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IViewService
    {
        ViewResponse GetArticleStockView(int userId);

    }
}
