namespace MicroApi.Models
{
    public class Tenders
    {
        public int? ID { get; set; }

        public string? CODE { get; set; }

        public string? DESCRIPTION { get; set; }

        public int? TENDER_TYPE { get; set; }

        public int? CURRENCY_ID { get; set; }

        public int? REGISTER_ID { get; set; }

        public int? DISPLAY_ORDER { get; set; }

        public bool? ROUND_VALUE { get; set; }

        public bool? ALLOW_MULTIPLE { get; set; }

        public bool? ALLOW_OPENING { get; set; }

        public bool? ALLOW_DECLARATION { get; set; }

        public int? AC_HEAD_ID { get; set; }

        public bool? ENTER_CARD_INFO { get; set; }

        public bool? PRINT_CUSTMER_COPY { get; set; }

        public bool? CAPTURE_CARD_INFO { get; set; }

        public bool? ADDITIONAL_INFO_REQUIRED { get; set; }

        public bool? IS_INACTIVE { get; set; }

        public string? ARABIC_DESCRIPTION { get; set; }

        public bool? IS_DELETED { get; set; }
        public string? TENDERTYPE_DESCRIPTION { get; set; }
        public string? CURRENCY_DESCRIPTION { get; set; }

    }
    public class TendersResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public Tenders data { get; set; }
    }
}
