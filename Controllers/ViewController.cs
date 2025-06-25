using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using MicroApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViewController : Controller
    {
        private readonly IViewService _viewService;

        public ViewController(IViewService viewService)
        {
            _viewService = viewService;
        }
        [HttpPost]
        [Route("list")]
        public ViewResponse GetArticleStockView(ArticleStockViewRequest request)
        {
            ViewResponse res = new ViewResponse();

            try
            {
                res = _viewService.GetArticleStockView(request.USER_ID);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
    }
}
