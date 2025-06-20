using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace ProductionMicroApi.Controllers
{
    [Route("api/article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        [Route("Insert")]
        public ArticleResponse Insert([FromBody] Article article)
        {
            ArticleResponse res = new ArticleResponse();
            try
            {
                res = _articleService.Insert(article);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public ArticleResponse Update([FromBody] ArticleUpdate article)
        {
            ArticleResponse res = new ArticleResponse();
            try
            {
                res = _articleService.Update(article);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("Select/{id:int}")]
        public ArticleResponse Select(int id, int Unit_id, string Art_no, string Color, int CategoryId, float Price)
        {
            ArticleResponse res = new ArticleResponse();
            try
            {
                res = _articleService.GetArticleById(id, Unit_id, Art_no, Color, CategoryId, Price);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("List")]
        public ArticleListResponse ArticleLogList()
        {
            ArticleListResponse res = new ArticleListResponse();
            try
            {
                res = _articleService.GetLogList();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public ArticleResponse Delete(int id)
        {
            ArticleResponse res = new ArticleResponse();
            try
            {
                res = _articleService.DeleteArticleData(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("LastOrderNo/{unitId:int}")]
        public IActionResult GetLastOrderNo(int unitId)
        {
            try
            {
                var lastOrderNo = _articleService.GetLastOrderNoByUnitId(unitId);
                return Ok(new { LastOrderNo = lastOrderNo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { flag = 0, message = ex.Message });
            }
        }
    }
}