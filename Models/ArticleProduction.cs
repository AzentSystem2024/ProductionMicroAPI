namespace MicroApi.Models
{
    public class ArticleProduction
    {
        public int COMPANY_ID { get; set; }
        public int USER_ID { get; set; }
        public int FIN_ID { get; set; }
        public string REF_NO { get; set; }
        public double ADDL_COST { get; set; }
        public string REMARKS { get; set; }
        public int PRODUCT_ID { get; set; }
        public double PROD_QTY { get; set; }
        public double TOTAL_ITEM_COST { get; set; }
        public double COST_OF_PRODUCTION { get; set; }
        public double UNIT_PRODUCT_COST { get; set; }
        public DateTime? PRODUCTION_DATE { get; set; }
        public int PRODUCTION_TYPE { get; set; }

        public List<ProductionRawMaterialRequest> RawMaterials { get; set; }
    }
    public class ProductionRawMaterialRequest
    {
        public int ID { get; set; }
        public string UOM { get; set; }
        public float QUANTITY { get; set; }
        public float USED_QTY { get; set; }
        public float COST { get; set; }
        public float AMOUNT { get; set; }
        public float REQUIRED_QTY { get; set; }
    }
    public class ArticleProductionUpdate
    {
        public int ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public int? USER_ID { get; set; }
        public int? FIN_ID { get; set; }
        public string? REF_NO { get; set; }
        public double? ADDL_COST { get; set; }
        public string? REMARKS { get; set; }
        public int? PRODUCT_ID { get; set; }
        public double? PROD_QTY { get; set; }
        public double? TOTAL_ITEM_COST { get; set; }
        public double? COST_OF_PRODUCTION { get; set; }
        public double? UNIT_PRODUCT_COST { get; set; }
        public DateTime? PRODUCTION_DATE { get; set; }
        public int? PRODUCTION_TYPE { get; set; }

        public List<ProductionRawMaterialRequest> RawMaterials { get; set; }
    }

    public class ArticleProduction_Item
    {
        public long ARTICLE_PRODUCTION_ID { get; set; }  
        public long ARTICLE_ID { get; set; }            
        public int PAIRS { get; set; }
        public int BOX_ID { get; set; }
        public string BARCODE { get; set; }
        public float PRICE { get; set; }

    }
    public class ArticleProdResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
    }
    public class ArticleBomItem
    {
        public int ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string UOM { get; set; }
        public decimal QUANTITY { get; set; }
        public decimal COST { get; set; }
        public decimal QTY_AVAILABLE { get; set; }
    }

    public class ArticleBomResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<ArticleBomItem> Data { get; set; }
    }
    public class ArticleBomRequest
    {
        public int ARTICLE_ID { get; set; }
    }
    public class ProductionListRequest
    {
        public int? COMPANY_ID { get; set; }
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }
    }

    public class ProductionListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<ProductionListItem> Data { get; set; }
    }

    public class ProductionListItem
    {
        public long PRODUCTION_ID { get; set; }
        public string PROD_NO { get; set; }
        public DateTime? PROD_DATE { get; set; }
        public decimal PRODUCED_QTY { get; set; }
        public decimal TOTAL_COST { get; set; }
        public long TRANS_ID { get; set; }
        public string VOUCHER_NO { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string UOM { get; set; }
        public string STATUS { get; set; }
    }
    public class ProductionViewResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }

        public ProductionHeader Header { get; set; }
        public List<ProductionRawMaterial> RawMaterials { get; set; }
    }

    public class ProductionHeader
    {
        public long PRODUCTION_ID { get; set; }
        public string PROD_NO { get; set; }
        public DateTime? PROD_DATE { get; set; }
        public int PRODUCT_ID { get; set; }
        public decimal PRODUCED_QTY { get; set; }
        public decimal TOTAL_COST { get; set; }
        public decimal UNIT_COST { get; set; }
        public decimal ADDL_COST { get; set; }
        public string REMARKS { get; set; }
        public long TRANS_ID { get; set; }
        public string VOUCHER_NO { get; set; }
        public string DESCRIPTION { get; set; }
        public string REF_NO { get; set; }
        public decimal COST_PRODUCTION { get; set; }
    }

    public class ProductionRawMaterial
    {
        public long ID { get; set; }
        public int ITEM_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string UOM { get; set; }
        public decimal BOM_QTY { get; set; }
        public decimal REQUIRED_QTY { get; set; }
        public decimal USED_QTY { get; set; }
        public decimal UNIT_COST { get; set; }
        public decimal TOTAL_COST { get; set; }
        public string ITEM_CODE { get; set; }
        public decimal QTY_AVAILABLE { get; set; }

    }



}
