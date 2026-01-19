using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleTypeController : ControllerBase
    {
        private readonly IArticleTypeService _articletypeService;
        public ArticleTypeController(IArticleTypeService articletypeService)
        {
            _articletypeService = articletypeService;
        }
        [HttpPost]
        [Route("insert")]
        public ArticleTypeResponse Insert(ArticleType articleType)
        {
            ArticleTypeResponse res = new ArticleTypeResponse();
            try
            {
                res = _articletypeService.Insert(articleType);

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

        public ArticleTypeResponse Update(ArticleTypeUpdate articleType)
        {
            ArticleTypeResponse res = new ArticleTypeResponse();
            try
            {
                res = _articletypeService.Update(articleType);
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
        public ArticleTypeResponse select(int id)
        {
            ArticleTypeResponse res = new ArticleTypeResponse();
            try
            {

                res = _articletypeService.GetArticleTypeById(id);

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
        public ArticleTypeListResponse ArticleTypeLogList()
        {

            ArticleTypeListResponse res = new ArticleTypeListResponse();
            try
            {
                res = _articletypeService.GetLogList();

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
        public ArticleTypeResponse Delete(int id)
        {
            ArticleTypeResponse res = new ArticleTypeResponse();
            try
            {
                res = _articletypeService.DeleteArticleTypeData(id);
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
