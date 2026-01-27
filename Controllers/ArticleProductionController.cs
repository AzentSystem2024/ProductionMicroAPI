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
        [HttpPost]
        [Route("Update")]
        public ArticleProdResponse Update(ArticleProductionUpdate model)
        {
            ArticleProdResponse res = new ArticleProdResponse();

            try
            {
                res = _articleProductionService.Update(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("commit")]
        public ArticleProdResponse commit(ArticleProductionUpdate model)
        {
            ArticleProdResponse res = new ArticleProdResponse();

            try
            {
                res = _articleProductionService.commit(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public ArticleProdResponse delete(int id)
        {
            ArticleProdResponse res = new ArticleProdResponse();

            try
            {
                res = _articleProductionService.Delete(id);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("articlebomlist")]
        public ArticleBomResponse GetArticleBomList(ArticleBomRequest model)
        {
            ArticleBomResponse res = new ArticleBomResponse();

            try
            {
                res = _articleProductionService.GetArticleBomList(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
       
        [HttpPost]
        [Route("select/{id:int}")]
        public ProductionViewResponse GetProductionById(int id)
        {
            ProductionViewResponse res = new ProductionViewResponse();

            try
            {
                res = _articleProductionService.GetProductionById(id);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("articlelist")]
        public ProductionListResponse articleprodlist(ProductionListRequest model)
        {
            ProductionListResponse res = new ProductionListResponse();

            try
            {
                res = _articleProductionService.articleprodlist(model);
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
