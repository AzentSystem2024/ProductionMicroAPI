using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.Controllers
{
    [Authorize]
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

