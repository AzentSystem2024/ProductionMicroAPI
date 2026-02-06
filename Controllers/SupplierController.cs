using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Services;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        [HttpPost]
        [Route("list")]
        public List<Suppliers> List(SuppListReq request)
        {
            List<Suppliers> Suppliers = new List<Suppliers>();

            try
            {
                
                Suppliers = _supplierService.GetAllSuppliers(request);
            }
            catch (Exception ex)
            {
            }
            return Suppliers.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public Suppliers Select(int id)
        {
            Suppliers objSupplier = new Suppliers();
            try
            {
                
                objSupplier = _supplierService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objSupplier;
        }

        [HttpPost]
        [Route("save")]
        public SupplierResponse SaveData(Suppliers supplierData)
        {
            SupplierResponse res = new SupplierResponse();

            try
            {
                
                _supplierService.SaveData(supplierData);

                res.flag = "1";
                res.message = "Success";

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
        public SupplierResponse Delete(int id)
        {
            SupplierResponse res = new SupplierResponse();
            

            try
            {
                if (_supplierService.DeleteSupplier(id))
                {
                    res.flag = "1";
                    res.message = "Supplier deleted successfully.";
                    res.data = _supplierService.GetItems(id);  // Fetch updated data if needed
                }
                else
                {
                    res.flag = "0";
                    res.message = "This supplier is used in a purchase order and cannot be deleted.";
                }
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("suppdtl")]
        public List<Supp_stateName> Getsupplist(SuppListReq request)
        {
            List<Supp_stateName> suppliers = new List<Supp_stateName>();

            try
            {

                suppliers = _supplierService.Getsupplist(request);
            }
            catch (Exception ex)
            {
            }
            return suppliers.ToList();
        }
    }
}
