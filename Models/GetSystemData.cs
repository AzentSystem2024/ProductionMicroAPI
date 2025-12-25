namespace MicroApi.Models
{
    public class GetSystemData
    {
        public DateTime SYSTEMDATETIME { get; set; }
        public string SYSTEM_NAME { get; set; }
        public string CULTURE { get; set; }

        public SystemDateInfo DATE_INFO { get; set; }
        public SystemTimeInfo TIME_INFO { get; set; }
        public SystemNumberInfo NUMBER_INFO { get; set; }
        public SystemCurrencyInfo CURRENCY_INFO { get; set; }
    }

    public class SystemDateInfo
    {
        public string DATE { get; set; }
        public string SHORT_DATE_FORMAT { get; set; }
        public string LONG_DATE_FORMAT { get; set; }
        public string FIRST_DAY_OF_WEEK { get; set; }
    }

    public class SystemTimeInfo
    {
        public string TIME { get; set; }
        public string SHORT_TIME_FORMAT { get; set; }
        public string LONG_TIME_FORMAT { get; set; }
    }

    public class SystemNumberInfo
    {
        public string DECIMAL_SYMBOL { get; set; }
        public string DECIMAL_SEPARATOR { get; set; }
        public string THOUSAND_SEPARATOR { get; set; }
        public int DECIMAL_DIGITS { get; set; }
        public string DIGIT_GROUPING { get; set; }
        public string LIST_SEPARATOR { get; set; }
        public string NEGATIVE_SIGN_SYMBOL { get; set; }
        public int NEGATIVE_NO_FORMAT { get; set; }
    }

    public class SystemCurrencyInfo
    {
        public string CURRENCY_CODE { get; set; }
        public string CURRENCY_SYMBOL { get; set; }
        public int CURRENCY_DECIMAL_DIGITS { get; set; }
        public int POSITIVE_CURRENCY_FORMAT { get; set; }
        public int NEGATIVE_CURRENCY_FORMAT { get; set; }
    }

    public class GetSystemDataResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public GetSystemData Data { get; set; }
    }
}
