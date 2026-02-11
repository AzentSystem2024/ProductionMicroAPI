using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoxUnpackController : ControllerBase
    {
        private readonly IBoxUnpackService _boxpackService;

        public BoxUnpackController(IBoxUnpackService boxUnpackService)
        {
            _boxpackService = boxUnpackService;
        }
        [HttpPost]
        [Route("insert")]
        public BoxUnpackResponse UnpackBox(BoxUnpack model)
        {
            BoxUnpackResponse res = new BoxUnpackResponse();

            try
            {
                res = _boxpackService.UnpackBox(model);
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
