﻿using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IItemDepartmentService
    {
        public List<ItemDepartment> GetAllDepartment();
        public Int32 SaveData(ItemDepartment company);
        public ItemDepartment GetItems(int id);
        public bool DeleteDepartment(int id);
    }
}
