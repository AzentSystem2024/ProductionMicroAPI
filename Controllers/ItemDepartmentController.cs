using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemDepartmentController : ControllerBase
    {
        private readonly IItemDepartmentService _itemDepartmentService;
        public ItemDepartmentController(IItemDepartmentService itemDepartmentService)
        {
            _itemDepartmentService = itemDepartmentService;
        }
        [HttpPost]
        [Route("list")]
        public List<ItemDepartment> List(ItemDepartmentListReq request)
        {
            List<ItemDepartment> companies = new List<ItemDepartment>();

            try
            {
                
                companies = _itemDepartmentService.GetAllDepartment(request);
            }
            catch (Exception ex)
            {
            }
            return companies.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public ItemDepartment Select(int id)
        {
            ItemDepartment objDepartment = new ItemDepartment();
            try
            {
                
                objDepartment = _itemDepartmentService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objDepartment;
        }

        [HttpPost]
        [Route("save")]
        public ItemDepartmentResponse SaveData(ItemDepartment departmentData)
        {
            ItemDepartmentResponse res = new ItemDepartmentResponse();

            try
            {
                
                Int32 ID = _itemDepartmentService.SaveData(departmentData);

                res.flag = "1";
                res.message = "Success";
                res.data = _itemDepartmentService.GetItems(ID);

            }
            catch (Exception ex)
            {
                res.flag = "1";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public ItemDepartmentResponse Delete(int id)
        {
            ItemDepartmentResponse res = new ItemDepartmentResponse();

            try
            {
                

                _itemDepartmentService.DeleteDepartment(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _itemDepartmentService.GetItems(id);
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
