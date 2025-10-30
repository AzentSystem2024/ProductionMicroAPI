using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Services;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerAddressController : ControllerBase
    {
        private readonly ICustomerAddressService _customerAddressService;
        public CustomerAddressController(ICustomerAddressService customerAddressService)
        {
            _customerAddressService = customerAddressService;
        }
        [HttpPost]
        [Route("save")]
        public CustomerAddressResponse Insert(CustomerAddress address)
        {
            CustomerAddressResponse res = new CustomerAddressResponse();

            try
            {

                int ID = _customerAddressService.Insert(address);

                res.flag = "1";
                res.message = "Success";
                //res.data = _customerAddressService.GetItems(ID);
            }
            catch (Exception ex)
            {
                res.flag = "1";
                res.message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("update")]
        public CustomerAddressResponse Update(CustomerAddressUpdate address)
        {
            CustomerAddressResponse res = new CustomerAddressResponse();

            try
            {

                int ID = _customerAddressService.Update(address);

                res.flag = "1";
                res.message = "Success";
                //res.data = _customerAddressService.GetItems(ID);
            }
            catch (Exception ex)
            {
                res.flag = "1";
                res.message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public List<CustomerAddressUpdate> List()
        {
            List<CustomerAddressUpdate> customers = new List<CustomerAddressUpdate>();

            try
            {

                customers = _customerAddressService.GetAllCustomers();
            }
            catch (Exception ex)
            {
            }
            return customers.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public CustomerAddressUpdate Select(int id)
        {
            CustomerAddressUpdate objCustomer = new CustomerAddressUpdate();
            try
            {

                objCustomer = _customerAddressService.GetById(id);
            }
            catch (Exception ex)
            {

            }

            return objCustomer;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public CustomerAddressResponse Delete(int id)
        {
            CustomerAddressResponse res = new CustomerAddressResponse();

            try
            {


                _customerAddressService.DeleteCustomers(id);
                res.flag = "1";
                res.message = "Success";
                //res.data = _customerAddressService.GetItems(id);
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
