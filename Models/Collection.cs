using Newtonsoft.Json;

namespace MicroApi.Models
{
    public class Collection
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int COLLECTION_NO { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime COLLECTION_DATE { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime COLLECTION_TIME { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string REFERENCE_NO { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PATIENT_NAME { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int AGE { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SEX { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int HOSPITAL_ID { get; set; }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public int DEPT_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UNIT_NAME { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string WARD { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UHID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal INCOME { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SPECIMEN { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DESCRIPTION { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DIAGNOSIS { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ANTIBIOTIC_PRESENT { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ANTIBIOTIC_PAST { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int INVESTIGATION_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string INVESTIGATION_NAME { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PREVIOUS_RESULT { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string OTHERS { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int USER_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DOCTOR_NAME { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DOCTOR_MOBILE { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string INVESTIGATION { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int REPORT_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int STATUS_ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string STATUS_NAME { get; set; }
        public string USER_NAME { get; set; }
        public string HOSPITAL_NAME { get; set; }
    }
    public class CollectionInput
    {
        public int? DEPT_ID { get; set; }
        public DateTime? DATE_FROM { get; set; } // Nullable DateTime
        public DateTime? DATE_TO { get; set; }   // Nullable DateTime
        public int USER_ID { get; set; }
    }
    public class CollectionDownloadInput
    {
        public int ID { get; set; }
    }
    public class CollectionDownloadOutput
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<CollectionDownload> data { get; set; }
    }
    public class CollectionDownload
    {
        public int ID { get; set; }
        public int COLLECTION_NO { get; set; } // Nullable DateTime
        public DateTime COLLECTION_DATE { get; set; }   // Nullable DateTime
        public DateTime COLLECTION_TIME { get; set; }
        public string REFERENCE_NO { get; set; }
        public string PATIENT_NAME { get; set; }
        public int AGE { get; set; }
        public string SEX { get; set; }
        public int HOSPITAL_ID { get; set; }
        public int DEPT_ID { get; set; }
        public string UNIT_NAME { get; set; }
        public string WARD { get; set; }
        public string UHID { get; set; }
        public Decimal INCOME { get; set; }
        public string SPECIMEN { get; set; }
        public string DOCTOR_MOBILE { get; set; }
        public string DOCTOR_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string DIAGNOSIS { get; set; }
        public string ANTIBIOTIC_PRESENT { get; set; }
        public string ANTIBIOTIC_PAST { get; set; }
        public int INVESTIGATION_ID { get; set; }
        public string INVESTIGATION_NAME { get; set; }
        public string PREVIOUS_RESULT { get; set; }
        public string OTHERS { get; set; }
        public int USER_ID { get; set; }
        public DateTime USER_TIME { get; set; }
    }

    public class PendingCollection
    {
        public int ID { get; set; }
        public int COLLECTION_NO { get; set; }
        public DateTime COLLECTION_DATE { get; set; }
        public string REFERENCE_NO { get; set; }
        public string PATIENT_NAME { get; set; }
        public int AGE { get; set; }
        public string SEX { get; set; }
        public string UNIT_NAME { get; set; }
        public string WARD { get; set; }
        public string UHID { get; set; }
        public string SPECIMEN { get; set; }
        public string INVESTIGATION_NAME { get; set; }
        public string DOCTOR_NAME { get; set; }
        public string HOSPITAL { get; set; }
    }

    public class PendingCollectionResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<PendingCollection> PendingCollectionData { get; set; }

    }
    public class CollectionResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<Collection> CollectionData { get; set; }

    }
    //public class Hospital
    //{
    //    public int ID { get; set; }
    //    public string HOSPITAL { get; set; }
    //}
    public class Sex
    {

        public string DESCRIPTION { get; set; }
    }
    public class Investigation
    {
        public int ID { get; set; }
        public string INVESTIGATION { get; set; }
    }
    public class MasterResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<Hospital> Hospitals { get; set; }
        public List<Sex> Sex { get; set; }
        public List<Investigation> investigations { get; set; }
    }
}
