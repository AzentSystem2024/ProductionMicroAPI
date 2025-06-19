using MicroApi.DataLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Route("api/listarticle")]
    [ApiController]
    public class ArticleLookUpController : ControllerBase
    {
        private readonly IArticleLookUpService _articleService;

        public ArticleLookUpController(IArticleLookUpService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult ListLogList()
        {
            try
            {
                var response = _articleService.GetArticleList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ArticleLookUpResponse { flag = 0, Message = ex.Message });
            }
        }
    }
}

