using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroApi.Models
{
    public class Clinician
    {
        public int ID { get; set; }

        public string ClinicianLicense { get; set; }
        public string ClinicianName { get; set; }
        public string ClinicianShortName { get; set; }
        public int? SpecialityID { get; set; }
        public int? MajorID { get; set; }
        public int? ProfessionID { get; set; }
        public int? CategoryID { get; set; }
        public bool? IsInactive { get; set; }
        public string SpecialityName { get; set; }
        public string Major { get; set; }
        public string Profession { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }
        public int? DepartmentID { get; set; }
        public string? Department { get; set; }

    }
    public class ClinicianResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<Clinician> data { get; set; }
   
}
}
