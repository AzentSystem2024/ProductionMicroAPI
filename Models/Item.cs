using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MicroApi.Models
{
    
    //public class ItemDepartment
    //{
    //    public int DEPARTMENT_ID { get; set; }
    //    public string DEPARTMENT_NAME { get; set; }
    //}
    public class Item
    {
        
            //public int ID { get; set; }
            public string ITEM_CODE { get; set; }
            public string ITEM_NAME { get; set; }
            public bool IS_FIXED_PRICE { get; set; }
            public decimal PRICE { get; set; }
            public bool IS_INACTIVE { get; set; }

            //public List<ItemDepartment> Departments { get; set; }
    }

    public class ItemUpdate
    {
        public int ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public bool IS_FIXED_PRICE { get; set; }
        public decimal PRICE { get; set; }
        public bool IS_INACTIVE { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }

    }
    public class ItemResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public ItemUpdate Data { get; set; }
    }
    public class ItemListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ItemUpdate> Data { get; set; }
        public List<ItemUpdate> Departments { get; set; }
    }
}
