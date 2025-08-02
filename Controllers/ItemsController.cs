using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;
        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpPost]
        [Route("list")]

        public ItemsResponse List(MasterFilter objFilter)
        {
            ItemsResponse res = new ItemsResponse();
            List<Items> items = new List<Items>();
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

                

                // Handle null objFilter
                if (objFilter == null)
                {
                    objFilter = new MasterFilter
                    {
                        MASTER_TYPE = "All", // Or any default value you need
                        MASTER_VALUE = string.Empty
                    };
                }

                items = _itemsService.GetAllItems(intUserID, objFilter.MASTER_TYPE == "ActiveOnly", objFilter);

                res.flag = "1";
                res.message = "Success";
                res.data = items;
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
        public Items select(int id)
        {
            Items objItems = new Items();
            try
            {
                
                objItems = _itemsService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objItems;
        }





        [HttpPost]
        [Route("insert")]
        public Items Insert(Items itemsData)
        {
            Items res = new Items();

            try
            {
                
                _itemsService.Insert(itemsData);
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
        public Items Update(Items itemsData)
        {
            Items res = new Items();

            try
            {
                
                _itemsService.Update(itemsData);
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
        [Route("delete/{id:int}")]
        public ItemsResponse Delete(int id)
        {
            ItemsResponse res = new ItemsResponse();

            try
            {
                

                _itemsService.DeleteItem(id);
                res.flag = "1";
                res.message = "Success";
                //res.data = _itemsService.GetItems(id);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }



        [HttpPost]
        [Route("aliasduplicate")]
        public ItemsResponse AliasDuplicate(ALIAS_DUPLICATE vInput)
        {
            ItemsResponse res = new ItemsResponse();

            try
            {
                
                res = _itemsService.Alias(vInput);
            }
            catch (Exception ex)
            {

            }

            return res;
        }
    }
}
