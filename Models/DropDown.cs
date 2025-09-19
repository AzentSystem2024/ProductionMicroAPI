namespace MicroApi.Models
{
    public class DropDown
    {
        public int ID { get; set; }
        public string DESCRIPTION { get; set; }
    }
    public class DropDownInput
    {
        public string NAME { get; set; }
        public int COUNTRY_ID { get; set; } = 0;  
        public int STATE_ID { get; set; } = 0;
        public int DISTRICT_ID { get; set; } = 0;
        public int? COMPANY_ID { get; set; }
        public int PAGE_NUMBER { get; set; } = 1;   
        public int PAGE_SIZE { get; set; } = 50;

    }
}
