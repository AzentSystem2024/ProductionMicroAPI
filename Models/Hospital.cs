
namespace MicroApi.Models
{
    public class Hospital
    {
        //public int ID { get; set; }
        public string HOSPITAL_NAME { get; set; } = string.Empty;
        public bool IS_INACTIVE { get; set; }

    }
    /// <summary>
    /// ////////
    /// </summary>
    public class HospitalListResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<HospitalUpdate> Data { get; set; }

    }
    public class HospitalResponse
    {
        public int flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public HospitalUpdate Data { get; set; }

    }
    public class HospitalUpdate
    {
        public int ID { get; set; }
        public string HOSPITAL_NAME { get; set; } = string.Empty;
        public bool IS_INACTIVE { get; set; }


    }
}
