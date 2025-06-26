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
        public int USER_ROLESID { get; set; } = 0;
    }
}
