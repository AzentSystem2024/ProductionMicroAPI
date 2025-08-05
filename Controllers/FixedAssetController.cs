using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Services;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixedAssetController : ControllerBase
    {
        private readonly IFixedAssetService _fixedAssetService;
        public FixedAssetController(IFixedAssetService fixedAssetService)
        {
            _fixedAssetService = fixedAssetService;
        }

        [HttpPost]
        [Route("list")]
        public FixedAssetListResponse FixedAssetList()
        {
            FixedAssetListResponse response = new FixedAssetListResponse();
            try
            {
                response = _fixedAssetService.GetFixedAssetList();
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
                response.Data = new List<FixedAssetList>();
            }

            return response;

        }
        [HttpPost]
        [Route("save")]
        public FixedAssetSaveResponse SaveData(FixedAsset fixedAsset)
        {
            FixedAssetSaveResponse res = new FixedAssetSaveResponse();
            try
            {
                _fixedAssetService.SaveData(fixedAsset);
                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("update")]
        public FixedAssetSaveResponse UpdateData(FixedAsset fixedAsset)
        {
            FixedAssetSaveResponse res = new FixedAssetSaveResponse();
            try
            {
                _fixedAssetService.UpdateData(fixedAsset);
                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;

        }
        [HttpPost]
        [Route("select/{id:int}")]
        public FixedAssetSelectResponse selectData(int? id)
        {
            FixedAssetSelectResponse response = new FixedAssetSelectResponse();
            try
            {

                response = _fixedAssetService.GetFixedAssetbyId(id);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.message = ex.Message;
            }

            return response;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public FixedAssetSaveResponse Delete(int id)
        {
            FixedAssetSaveResponse res = new FixedAssetSaveResponse();

            try
            {

                _fixedAssetService.Delete(id);
                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }

    }
}

