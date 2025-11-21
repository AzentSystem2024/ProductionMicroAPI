using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace MicroApi.Controllers
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
        [Route("Select")]
        public ArticleResponse Select([FromBody] ArticleSelectRequest request)
        {
            ArticleResponse res = new ArticleResponse();
            try
            {
                res = _articleService.GetArticleById(request);
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
                res = _articleService.GetArticleList();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("listitem")]
        public ListItemsResponse GetItems()
        {
            ListItemsResponse res = new ListItemsResponse();
            try
            {
                res = _articleService.GetItems();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("getitem")]
        public ItemdataResponse GetItemByCode(ItemcodeRequest request)
        {
            ItemdataResponse res = new ItemdataResponse();
            try
            {
                res = _articleService.GetItemByCode(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("Delete")]
        public ArticleResponse Delete(DeleteArticleRequest request)
        {
            ArticleResponse res = new ArticleResponse();
            try
            {
                res = _articleService.DeleteArticleData(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("LastOrderNo")]
        public IActionResult GetLastOrderNo()
        {
            try
            {
                var lastOrderNo = _articleService.GetLastOrderNoByUnitId();
                return Ok(new { LastOrderNo = lastOrderNo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { flag = 0, message = ex.Message });
            }
        }
    }
}