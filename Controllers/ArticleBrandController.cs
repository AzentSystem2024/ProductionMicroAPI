using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleBrandController : ControllerBase
    {
        private readonly IArticleBrandService _articlebrandService;
        public ArticleBrandController(IArticleBrandService articlebrandService)
        {
            _articlebrandService = articlebrandService;
        }
        [HttpPost]
        [Route("insert")]
        public ArticleBrandResponse Insert(ArticleBrand articleBrand)
        {
            ArticleBrandResponse res = new ArticleBrandResponse();
            try
            {
                res = _articlebrandService.Insert(articleBrand);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("update")]

        public ArticleBrandResponse Update(ArticleBrandUpdate articleBrand)
        {
            ArticleBrandResponse res = new ArticleBrandResponse();
            try
            {
                res = _articlebrandService.Update(articleBrand);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public ArticleBrandResponse select(int id)
        {
            ArticleBrandResponse res = new ArticleBrandResponse();
            try
            {

                res = _articlebrandService.GetArticleBrandById(id);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;

            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public ArticleBrandListResponse ArticleColorLogList(ArticleBrandListRequest request)
        {

            ArticleBrandListResponse res = new ArticleBrandListResponse();
            try
            {
                res = _articlebrandService.GetLogList(request);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public ArticleBrandResponse Delete(int id)
        {
            ArticleBrandResponse res = new ArticleBrandResponse();
            try
            {
                res = _articlebrandService.DeleteArticleBrandData(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
    }
}
