namespace MicroApi.Models
{
    public class Uom
    {
        public int? ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public string? UOM { get; set; }
        public string? flag { get; set; }
        public string? message { get; set; }
    }
    public class UomResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
    }
    public class UOMListReq
    {
        public int COMPANY_ID { get; set; }
    }
}
