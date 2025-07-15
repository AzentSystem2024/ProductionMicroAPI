using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using MicroApi.DataLayer.Interface;
using System.Diagnostics.Metrics;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Route("list")]
        public List<Customer> List()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                
                customers = _customerService.GetAllCustomers();
            }
            catch (Exception ex)
            {
            }
            return customers.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public Customer Select(int id)
        {
            Customer objCustomer = new Customer();
            try
            {
                
                objCustomer = _customerService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objCustomer;
        }

        [HttpPost]
        [Route("save")]
        public CustomerResponse SaveData(Customer customerData)
        {
            CustomerResponse res = new CustomerResponse();

            try
            {
                
                Int32 ID = _customerService.SaveData(customerData);

                res.flag = "1";
                res.message = "Success";
                res.data = _customerService.GetItems(ID);
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
        public CustomerResponse Delete(int id)
        {
            CustomerResponse res = new CustomerResponse();

            try
            {
                

                _customerService.DeleteCustomers(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _customerService.GetItems(id);
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
