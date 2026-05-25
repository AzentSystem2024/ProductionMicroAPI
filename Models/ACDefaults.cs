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
        public int? HEAD_ID { get; set; }
        public string? HEAD_NAME { get; set; }
    }
    public class AcDefaultsListResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public List<AcDefaultsList> DATA { get; set; }
    }
    public class AcDefaultsDeleteReq
    {
        public int COMPANY_ID { get; set; }
        public int HEAD_ID { get; set; }
    }
    public class ACDefaultsResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
    }


    public class ACDefaultSettingsResponse
    {
        public string flag { get; set; }
        public string Message { get; set; }
        public ACDefaultSettings Data { get; set; }
    }
    public class ACDefaultSettings
    {
        public int? GP_SUPPLIER_ID { get; set; }
        public int? GP_CUSTOMER_ID { get; set; }
        public int? GP_ASSET_ID { get; set; }
        public int? GP_FIXED_ASSET_ID { get; set; }
        public int? GP_CASH_ID { get; set; }
        public int? GP_BANK_ID { get; set; }

        public int? AC_CASH_ID { get; set; }
        public int? AC_PETTY_CASH_ID { get; set; }

        public int? AC_SALE_ID { get; set; }
        public int? AC_OUTSIDE_SALE_ID { get; set; }
        public int? AC_PURCHASE_ID { get; set; }
        public int? AC_SALARY_PAYABLE_ID { get; set; }
        public int? AC_SALARY_EXPENSE_ID { get; set; }
        public int? AC_LS_PAYABLE_ID { get; set; }
        public int? AC_LS_EXPENSE_ID { get; set; }
        public int? AC_EOS_PAYABLE_ID { get; set; }

        public int? AC_EOS_EXPENSE_ID { get; set; }
        public int? AC_INVENTORY_ID { get; set; }
        public int? AC_WIP_ID { get; set; }
        public int? AC_COST_INVENTORY_ID { get; set; }
        public int? AC_PDC_RECEIVABLE_ID { get; set; }
        public int? AC_PDC_PAYABLE_ID { get; set; }
        public int? AC_ASSET_ID { get; set; }

        public int? AC_RECEIVABLE_ID { get; set; }
        public int? AC_PAYABLE_ID { get; set; }
        public int? AC_ADVANCE_PAYABLE_ID { get; set; }
        public int? AC_OUTPUT_VAT { get; set; }
        public int? AC_DEPRECIATION_EXPENSE_ID { get; set; }
        public int? AC_GOODS_TRANSIT { get; set; }
        public int? STORE_ID { get; set; }
    }

    public class ACDefaultSettingsInput
    {
        public int CompanyID { get; set; }
    }


}
