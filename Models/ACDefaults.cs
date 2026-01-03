namespace MicroApi.Models
{
    public class ACDefaults
    {
        public int COMPANY_ID { get; set; }
        public int? AC_SALE_ID { get; set; }
        public int? AC_PURCHASE_ID { get; set; }
        public int? AC_INVENTORY_ID { get; set; }
        public int? AC_INPUT_VAT { get; set; }
        public int? AC_OUTPUT_VAT { get; set; }
        public int? AC_DEPRECIATION_EXPENSE_ID { get; set; }
        public int? AC_GOODS_TRANSIT { get; set; }
    }
    public class AcDefaultsListReq
    {
        public int COMPANY_ID { get; set; }
    }
    public class AcDefaultsList
    {
        public string NAME { get; set; }
        public int HEAD_ID { get; set; }
        public string HEAD_NAME { get; set; }
    }
    public class AcDefaultsListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<AcDefaultsList> DATA { get; set; }
    }

}
