﻿using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ICustomerService
    {
        public List<CustomerUpdate> GetAllCustomers();
        public Int32 SaveData(Customer company);
        public Int32 UpdateCustomer(CustomerUpdate company);
        public CustomerUpdate GetItems(int id);
        public bool DeleteCustomers(int id);
        public List<DeliveryAddress> GetDeliveryAddressesForDealer(DELIVERYADDREQUEST custId);
    }
}
