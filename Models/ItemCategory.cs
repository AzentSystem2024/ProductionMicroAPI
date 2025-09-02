namespace MicroApi.Models
{
    public class ItemCategory
    {
        public int ID { get; set; }

        public string CODE { get; set; }

        public string CAT_NAME { get; set; }

        public int LOYALTY_POINT { get; set; }

        public int COST_HEAD_ID { get; set; }

        public int COMPANY_ID { get; set; }

        public string? COMPANY_NAME { get; set; }

        public int DEPT_ID { get; set; }

        public string? DEPT_NAME { get; set; }

        public string? IS_DELETED { get; set; }

    }
    public class ItemCategoryResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public ItemCategory data { get; set; }
    }
}
