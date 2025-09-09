using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StoresController : ControllerBase
    {
        private readonly IStoresService _storesService;
        public StoresController(IStoresService storesService)
        {
            _storesService = storesService;
        }
        [HttpPost]
        [Route("list")]
        public List<Stores> List()
        {
            List<Stores> store = new List<Stores>();

            try
            {
                
                store = _storesService.GetAllStores();
            }
            catch (Exception ex)
            {
            }
            return store.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public Stores Select(int id)
        {
            Stores objStores = new Stores();
            try
            {
                
                objStores = _storesService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objStores;
        }

        [HttpPost]
        [Route("save")]
        public StoresResponse SaveData(Stores storeData)
        {
            StoresResponse res = new StoresResponse();

            try
            {
                
                Int32 ID = _storesService.SaveData(storeData);

                res.flag = "1";
                res.message = "Success";
                res.data = _storesService.GetItems(ID);
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
        public StoresResponse Delete(int id)
        {
            StoresResponse res = new StoresResponse();

            try
            {
                

                _storesService.DeleteStores(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _storesService.GetItems(id);
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
