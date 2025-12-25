namespace MicroApi.Models
{
    public class ItemSubCategory
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }

        public string? CODE { get; set; }

        public string? SUBCAT_NAME { get; set; }

        public int? CAT_ID { get; set; }

        public string? CAT_NAME { get; set; }

        //public string IS_DELETED { get; set; }

        public int? DEPT_ID { get; set; }

        public string? DEPT_NAME { get; set; }
    }
    public class ItemSubCategoryResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public ItemSubCategory data { get; set; }
    }
    public class ItemSubCategoryList
    {
        public int COMPANY_ID { get; set; }

    }
}
