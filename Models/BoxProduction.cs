namespace MicroApi.Models
{
    public class BoxProduction
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

        public List<BoxProdRequest>? RawMaterials { get; set; }
    }
    public class BoxProdRequest
    {
        public int? ID { get; set; }
        public string? UOM { get; set; }
        public float? QUANTITY { get; set; }
        public float? USED_QTY { get; set; }
        public float? COST { get; set; }
        public float? AMOUNT { get; set; }
        public float? REQUIRED_QTY { get; set; }
    }
    public class BoxProductionUpdate
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

        public List<BoxProdRequest>? RawMaterials { get; set; }
    }
    public class BoxProdResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
    }
    public class PackingBOMResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<PackingBOMItem> Data { get; set; }
    }
    public class PackingBOMRequest
    {
        public int ITEM_ID { get; set; }
    }
    public class PackingBOMItem
    {
        public int ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string UOM { get; set; }
        public decimal QUANTITY { get; set; }
        public decimal COST { get; set; }
        public decimal QTY_AVAILABLE { get; set; }
    }
    public class BoxProductionSelectResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }

        public BoxProductionHeader Header { get; set; }
        public List<BoxProdRequestMaterial> RawMaterials { get; set; }
    }

    public class BoxProductionHeader
    {
        public long PRODUCTION_ID { get; set; }
        public string PROD_NO { get; set; }
        public DateTime? PROD_DATE { get; set; }
        public int PRODUCT_ID { get; set; }
        public decimal PRODUCED_QTY { get; set; }
        public decimal TOTAL_COST { get; set; }
        public decimal UNIT_COST { get; set; }
        public decimal ADDL_COST { get; set; }
        public string ADDL_DESCRIPTION { get; set; }
        public long TRANS_ID { get; set; }
        public string VOUCHER_NO { get; set; }
        public string DESCRIPTION { get; set; }
        public string REF_NO { get; set; }
        public decimal COST_PRODUCTION { get; set; }
    }

    public class BoxProdRequestMaterial
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
    }
    public class BoxProductionListRequest
    {
        public int COMPANY_ID { get; set; }
    }

    public class BoxProductionListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<BoxProductionListItem> Data { get; set; }
    }

    public class BoxProductionListItem
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
    }
}
