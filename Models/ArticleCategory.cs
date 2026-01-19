namespace MicroApi.Models
{
    public class ArticleCategory
    {
        public int ID { get; set; }
        public int CATEGORY_ID { get; set; }
        public int SIZE { get; set; }
        

    }
    public class ArticleCategoryListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ArticleCategory> Data { get; set; }

    }
    public class PackCombination
    {
        public int size { get; set; }
        public int pairQty { get; set; }
    }

    public class PackingMaster
    {
        public string NAME { get; set; }
        public bool ISEXPORT { get; set; }
        public bool ISANYCOMBINATION { get; set; }
        public int PAIR_QTY { get; set; }
        public List<PackCombination> PACKCOMBINATIONS { get; set; } = new List<PackCombination>();
    }

    public class Category
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public bool? IS_INACTIVE { get; set; }
        public List<int> SIZES { get; set; } = new List<int>();
        public List<PackingMaster> PACKING { get; set; } = new List<PackingMaster>();
    }

    public class CategoryResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public Category Data { get; set; }
    }
    public class ArticleCategoryInsert
    {
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public bool IS_INACTIVE { get; set; }
        public List<int> SIZES { get; set; } = new List<int>();
        public List<PackingMaster> PACKING { get; set; } = new List<PackingMaster>();
    }
    public class ArticleCategoryResponse
    {
        public int flag { get; set; }          
        public string Message { get; set; }
    }
    public class ArticleCategoryUpdate
    {
        public int ID { get; set; }  
        public string CODE { get; set; } = string.Empty;
        public string DESCRIPTION { get; set; } = string.Empty;
        public bool IS_INACTIVE { get; set; }
        public List<int> SIZES { get; set; } = new List<int>();
        public List<PackingMaster> PACKING { get; set; } = new List<PackingMaster>();
    }
      public class PackingSummary
    {
        public string PACKING { get; set; }
        public int PAIR { get; set; }
    }
    public class PackingListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<PackingSummary> PackingList { get; set; }
    }
    public class PackingSave
    {
       // public string CODE { get; set; }
        public string CATEGORY { get; set; }
        public string PACK_NAME { get; set; }
        public bool ISEXPORTPACKING { get; set; }
        public bool ISANYCOMBINATION { get; set; }
        public int PAIR_QTY { get; set; }
       public List<PackingSizeDetail> SIZEDETAILS { get; set; }
    }

    public class PackingSizeDetail
    {
        public int SIZE { get; set; }
        public int PAIR_QTY { get; set; }  
    }
    public class PackingListRequest
    {
        public string CATEGORY_NAME { get; set; }
    }
    public class CategoryListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<ArticleCategoryItem> CATEGORIES { get; set; }
    }

    public class ArticleCategoryItem
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public bool? IS_INACTIVE { get; set; }
    }
   

}
