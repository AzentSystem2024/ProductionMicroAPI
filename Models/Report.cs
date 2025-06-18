namespace MicroApi.Models
{
    public class WetFilm
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    public class WetFilmResult
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    public class GramStain
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    public class GramStainResult
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    public class Isolate
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    public class Remarks
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    public class Antibiotic
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    public class Sensitivity
    {
        public string Description { get; set; }
    }
    public class ReportListData
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<WetFilm> WetFilm { get; set; }
        public List<WetFilmResult> WetFilmResult { get; set; }
        public List<GramStain> GramStain { get; set; }
        public List<GramStainResult> GramStainResult { get; set; }
        public List<Isolate> Isolate { get; set; }
        public List<Remarks> Remarks { get; set; }
        public List<Antibiotic> Antibiotic { get; set; }
        public List<Sensitivity> Sensitivity { get; set; }
    }

    public class Report
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public int ID { get; set; }
        public int COLLECTION_ID { get; set; }
        public string ZIEHEL_STAIN { get; set; }
        public string SPECIAL_STAIN { get; set; }
        public string CULTURE { get; set; }
        public string REMARKS { get; set; }
        public string ISOLATE1_IDENTIFICATION { get; set; }
        public string ISOLATE1_GROWTH_RATE { get; set; }
        public string ISOLATE1_COLONY_COUNT { get; set; }
        public string ISOLATE2_IDENTIFICATION { get; set; }
        public string ISOLATE2_GROWTH_RATE { get; set; }
        public string ISOLATE2_COLONY_COUNT { get; set; }
        public string ISOLATE3_IDENTIFICATION { get; set; }
        public string ISOLATE3_GROWTH_RATE { get; set; }
        public string ISOLATE3_COLONY_COUNT { get; set; }
        public bool IS_PRELIMINERY { get; set; }
        public bool IS_PUBLISHED { get; set; }
        public int USER_ID { get; set; }
        public List<ReportEntry> ReportEntry { get; set; }
        public List<ReportGramStain> ReportGramStain { get; set; }
        public List<ReportWetFilm> ReportWetFilm { get; set; }

    }
    public class ReportEntry
    {
        public int ID { get; set; }
        public int REPORT_ID { get; set; }
        public int ANTIBIOTIC_ID { get; set; }
        public string ISOLATE1 { get; set; }
        public string ISOLATE2 { get; set; }
        public string ISOLATE3 { get; set; }
        public string ADDL_INFO { get; set; }
        public string REMARKS { get; set; }

    }
    public class ReportGramStain
    {
        public int ID { get; set; }
        public int REPORT_ID { get; set; }
        public string GRAM_STAIN { get; set; }
        public string PRESENCE { get; set; }
    }
    public class ReportWetFilm
    {
        public int ID { get; set; }
        public int REPORT_ID { get; set; }
        public string WET_FILM { get; set; }
        public string PRESENCE { get; set; }
    }


    public class SaveReportResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
}
