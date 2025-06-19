using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using MicroApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistributorController : ControllerBase
    {
        private readonly IDistributorService _distributorService;
        public DistributorController(IDistributorService distributorService)
        {
            _distributorService = distributorService;
        }
        [HttpPost]
        [Route("insert")]
        public DistributorResponse Insert(Distributor distributor)
        {
            DistributorResponse res = new DistributorResponse();
            try
            {
                res = _distributorService.Insert(distributor);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("update")]
        public DistributorResponse Update(DistributorUpdate distributor)
        {
            DistributorResponse res = new DistributorResponse();
            try
            {
                res = _distributorService.UpdateDistributor(distributor);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("list")]
        public DistributorListResponse GetDistributorList()
        {
            DistributorListResponse res = new DistributorListResponse();

            try
            {
                res = _distributorService.GetDistributorList();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public DistributorResponse GetDistributorById(int id)
        {
            DistributorResponse res = new DistributorResponse();

            try
            {
                res = _distributorService.GetDistributorById(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("Delete/{id:int}")]
        public DistributorResponse Delete(int id)
        {
            DistributorResponse res = new DistributorResponse();
            try
            {
                res = _distributorService.DeleteDistributorData(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        //[HttpPost]
        //[Route("save")]
        //public DistributorLocationResponse InsertLocation(DistributorLocation location)
        //{
        //    DistributorLocationResponse res = new DistributorLocationResponse();

        //    try
        //    {
        //        res = _distributorService.InsertDistributorLocation(location); 
        //    }
        //    catch (Exception ex)
        //    {
        //        res.flag = 0;
        //        res.Message = ex.Message;
        //    }

        //    return res;
        //}
        //[HttpPost]
        //[Route("edit")]
        //public DistributorLocationResponse UpdateLocation(DistributorLocationUpdate location)
        //{
        //    DistributorLocationResponse res = new DistributorLocationResponse();
        //    try
        //    {
        //        res = _distributorService.UpdateDistributorLocation(location);
        //    }
        //    catch (Exception ex)
        //    {
        //        res.flag = 0;
        //        res.Message = ex.Message;
        //    }
        //    return res;
        //}


    }
}
