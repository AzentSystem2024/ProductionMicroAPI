using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Services;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemBrandController : ControllerBase
    {
        private readonly IItemBrandService _itemBrandService;
        public ItemBrandController(IItemBrandService itemBrandService)
        {
            _itemBrandService = itemBrandService;
        }
        [HttpPost]
        [Route("list")]
        public ItemBrandResponse List()
        {
            ItemBrandResponse res = new ItemBrandResponse();
            List<ItemBrand> itemBrands = new List<ItemBrand>();

            try
            {
                string apiKey = "";
                Int32 intUserID = 1;

                /*
                foreach (var header in Request.Headers)
                {
                    if (header.Key == "x-api-key")
                        apiKey = header.Value.ToList()[0];
                }

                
                
                User_DAL userDAL = new User_DAL();
                Int32 intUserID = userDAL.GetUserIDWithToken(apiKey);
                if (intUserID < 1)
                {
                    res.flag = "0";
                    res.message = "Invalid authorization key";
                    return res;
                }

                */


                itemBrands = _itemBrandService.GetAllItemBrand(intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = itemBrands;
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
        public ItemBrandResponse Select(int id)
        {
            ItemBrandResponse res = new ItemBrandResponse();

            string apiKey = "";
            Int32 intUserID = 1;

            /*
            foreach (var header in Request.Headers)
            {
                if (header.Key == "x-api-key")
                    apiKey = header.Value.ToList()[0];
            }



            User_DAL userDAL = new User_DAL();
            Int32 intUserID = userDAL.GetUserIDWithToken(apiKey);
            if (intUserID < 1)
            {
                res.flag = "0";
                res.message = "Invalid authorization key";
                return res;
            }

            */


            try
            {


                res.flag = "1";
                res.message = "Success";
                res.data = _itemBrandService.GetItems(id);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
                return res;
            }

            return res;
        }


        [HttpPost]
        [Route("save")]
        public ItemBrandResponse SaveData(ItemBrand objItemBrand)
        {
            ItemBrandResponse res = new ItemBrandResponse();


            string apiKey = "";
            Int32 intUserID = 1;

            /*
            foreach (var header in Request.Headers)
            {
                if (header.Key == "x-api-key")
                    apiKey = header.Value.ToList()[0];
            }



            User_DAL userDAL = new User_DAL();
            Int32 intUserID = userDAL.GetUserIDWithToken(apiKey);
            if (intUserID < 1)
            {
                res.flag = "0";
                res.message = "Invalid authorization key";
                return res;
            }

            */

            try
            {

                Int32 ClinicianMajorID = _itemBrandService.SaveData(objItemBrand, intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = _itemBrandService.GetItems(ClinicianMajorID);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
                return res;
            }

            return res;
        }


        [HttpPost]
        [Route("delete/{id:int}")]
        public ItemBrandResponse Delete(int id)
        {
            ItemBrandResponse res = new ItemBrandResponse();
            ItemBrand objitembrand = new ItemBrand();

            string apiKey = "";
            Int32 intUserID = 1;

            /*
            foreach (var header in Request.Headers)
            {
                if (header.Key == "x-api-key")
                    apiKey = header.Value.ToList()[0];
            }

            User_DAL userDAL = new User_DAL();
            Int32 intUserID = userDAL.GetUserIDWithToken(apiKey);
            if (intUserID < 1)
            {
                res.flag = "0";
                res.message = "Invalid authorization key";
                return res;
            }

            */

            try
            {

                _itemBrandService.DeleteItemBrand(id, intUserID);

                res.flag = "1";
                res.message = "Success";
                res.data = _itemBrandService.GetItems(id);

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
                return res;
            }

            return res;
        }
    }
}
