using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Services;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicroApi.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;
        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        //[HttpPost]
        //[Route("list")]
        //public ItemsResponse List(DateRequest objFilter)
        //{
        //    ItemsResponse res = new ItemsResponse();
        //    List<Items> items = new List<Items>();

        //    try
        //    {
        //        // Ensure filter is not null
        //        if (objFilter == null)
        //        {
        //            objFilter = new DateRequest
        //            {
        //                DATE_FROM = null,
        //                DATE_TO = null
        //            };
        //        }

        //        // Prepare request for service
        //        DateRequest request = new DateRequest
        //        {
        //            DATE_FROM = objFilter.DATE_FROM ?? DateTime.MinValue,
        //            DATE_TO = objFilter.DATE_TO ?? DateTime.MinValue
        //        };

        //        // Get items from service
        //        items = _itemsService.GetAllItems(request);

        //        res.flag = "1";
        //        res.message = "Success";
        //        res.data = items;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.flag = "0";
        //        res.message = ex.Message;
        //        res.data = null;
        //    }

        //    return res;
        //}
        [HttpPost]
        [Route("list")]
        public ItemsResponse List()
        {
            ItemsResponse res = new ItemsResponse();
            try
            {
                var items = _itemsService.GetAllItems();

                res.flag = "1";
                res.message = "Success";
                res.data = items;
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
                res.data = null;
            }
            return res;
        }


        [HttpPost]
        [Route("Itemslist")]
        public IActionResult Itemslist()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            // return Ok(_itemsService.GetAllItemsNew());
            //var data = _itemsService.GetAllItemsNew();

            sw.Stop();

            //return Ok(_itemsService.GetAllItemsNew().Take(5000));
            return Ok(_itemsService.GetAllItemsNew());
        }
        //[HttpPost]
        //[Route("Itemslist")]
        //public IActionResult Itemslist(int pageNumber = 1, int pageSize = 5000)
        //{
        //    var sw = System.Diagnostics.Stopwatch.StartNew();

        //    var data = _itemsService.GetAllItemsNew();

        //    var totalRecords = data.Count;

        //    var pagedData = data
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();

        //    sw.Stop();

        //    return Ok(new
        //    {
        //        TotalRecords = totalRecords,
        //        PageNumber = pageNumber,
        //        PageSize = pageSize,
        //        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
        //        ServiceTime = sw.ElapsedMilliseconds,
        //        Data = pagedData
        //    });
        //}

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
        public ItemResponse Insert(Items itemsData)
        {
            var (flag, msg) = _itemsService.Insert(itemsData);

            return new ItemResponse
            {
                flag = flag.ToString(),
                message = msg
            };
        }
        [HttpPost]
        [Route("Update")]
        public IActionResult Update(Items items)
        {
            try
            {
                var response = _itemsService.Update(items);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new ItemsResponse
                {
                    flag = "0",
                    message = ex.Message
                });
            }
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
