using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleColorController : ControllerBase
    {
        private readonly IArticleColorService _articlecolorService;
        public ArticleColorController(IArticleColorService articlecolorService)
        {
            _articlecolorService = articlecolorService;
        }
        [HttpPost]
        [Route("insert")]
        public ArticleColorResponse Insert(ArticleColor articleColor)
        {
            ArticleColorResponse res = new ArticleColorResponse();
            try
            {
                res = _articlecolorService.Insert(articleColor);

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

        public ArticleColorResponse Update(ArticleColorUpdate articleColor)
        {
            ArticleColorResponse res = new ArticleColorResponse();
            try
            {
                res = _articlecolorService.Update(articleColor);
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
        public ArticleColorResponse select(int id)
        {
            ArticleColorResponse res = new ArticleColorResponse();
            try
            {

                res = _articlecolorService.GetArticleColorById(id);

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
        public ArticleColorListResponse ArticleColorLogList()
        {

            ArticleColorListResponse res = new ArticleColorListResponse();
            try
            {
                res = _articlecolorService.GetLogList();

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
        public ArticleColorResponse Delete(int id)
        {
            ArticleColorResponse res = new ArticleColorResponse();
            try
            {
                res = _articlecolorService.DeleteArticleColorData(id);
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

