using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleProductionController : ControllerBase
    {
        private readonly IArticleProductionService _articleProductionService;

        public ArticleProductionController(IArticleProductionService articleProductionService)
        {
            _articleProductionService = articleProductionService;
        }
        [HttpPost]
        [Route("insert")]
        public ArticleProdResponse Insert(ArticleProduction model)
        {
            ArticleProdResponse res = new ArticleProdResponse();

            try
            {
                res = _articleProductionService.Insert(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
    }
}
