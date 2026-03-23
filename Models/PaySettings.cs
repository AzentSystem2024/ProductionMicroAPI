namespace MicroApi.Models
{
    public class PaySettings
    {
        public int? DAILY_HOURS { get; set; }
        public int? MAX_OT_MTS { get; set; }
        public decimal? NORMAL_OT_RATE { get; set; }
        public decimal? HOLIDAY_OT_RATE { get; set; }
        public decimal? LEAVE_SAL_DAYS { get; set; }
        public string? UQ_LABOUR_ID { get; set; }
        public string? BANK_AC_NO { get; set; }
        public string? BANK_CODE { get; set; }
        public int? SAL_EXPENSE_HEAD_ID { get; set; }
        public int? SAL_PAYABLE_HEAD_ID { get; set; }
        public int? LS_EXPENSE_HEAD_ID { get; set; }
        public int? LS_PAYABLE_HEAD_ID { get; set; }
        public int? EOS_EXPENSE_HEAD_ID { get; set; }
        public int EOS_PAYABLE_HEAD_ID { get; set; }
    }
    public class PaySettingsResponce
    {
        public string flag { get; set; }
        public string message { get; set; }
        public PaySettings data { get; set; }
    }
}
