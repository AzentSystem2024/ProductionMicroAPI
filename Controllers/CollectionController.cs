using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using System.Data.SqlClient;
using MicroApi.Models;

namespace RetailApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _CollectionService;
        public CollectionController(ICollectionService CollectionService)
        {
            _CollectionService = CollectionService;
        }
        [HttpPost]
        [Route("masterlist")]
        public MasterResponse MasterList()
        {
            MasterResponse res = new MasterResponse();

            try
            {

                

                // Retrieve the full response from DAL
                res = _CollectionService.MasterData();

                if (res != null)
                {
                    res.flag = 1;
                    res.message = "Success";
                }
                else
                {
                    res.flag = 0;
                    res.message = "No data found.";
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("pendingCollection")]
        public PendingCollectionResponse pendingCollection()
        {
            PendingCollectionResponse res = new PendingCollectionResponse();

            try
            {

                

                // Retrieve the full response from DAL
                res = _CollectionService.GetPendingCollection();

                if (res != null)
                {
                    res.flag = 1;
                    res.Message = "Success";
                }
                else
                {
                    res.flag = 0;
                    res.Message = "No data found.";
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }


        [HttpPost]
        [Route("insert")]
        public CollectionResponse Insert(Collection collection)
        {
            CollectionResponse res = new CollectionResponse();



            try
            {
                
                res = _CollectionService.Insert(collection);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;

            }

            return res;
        }

        [HttpPost]
        [Route("download")]
        public CollectionDownloadOutput DownloadCollection(CollectionDownloadInput vInput)
        {

            CollectionDownloadOutput vOutput = new CollectionDownloadOutput();

            try
            {
                
                vOutput = _CollectionService.DownloadCollection(vInput);

            }
            catch (Exception ex)
            {
                vOutput.flag = 0;
                vOutput.message = ex.Message;

            }

            return vOutput;
        }

        [HttpPost]
        [Route("update")]
        public CollectionResponse Update(Collection collection)
        {
            CollectionResponse res = new CollectionResponse();



            try
            {
                
                res = _CollectionService.Update(collection);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;

            }

            return res;
        }

        [HttpPost]
        [Route("CollectionNo")]
        public CollectionResponse GetNextCollectionNo()
        {
            CollectionResponse res = new CollectionResponse();



            try
            {
                
                res = _CollectionService.NextCollectionNo();

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;

            }

            return res;
        }

        [HttpPost]
        [Route("collectionlist")]
        public CollectionResponse CollectionList(CollectionInput collectionInput)
        {
            CollectionResponse res = new CollectionResponse();
            List<Collection> collections = new List<Collection>();
            try
            {

                
                collections = _CollectionService.GetCollection(collectionInput);

                res.flag = 1;
                res.Message = "Success";
                res.CollectionData = collections;
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
    }
}
