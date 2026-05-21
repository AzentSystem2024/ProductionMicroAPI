using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroApi.Models
{
    public class DenialMaster
    {
        public int? ID { get; set; }
        public string? DenialCode { get; set; }
        public int? DenialCategoryID { get; set; }
        public string? DenialCategory { get; set; }
        public int? DenialTypeID { get; set; }
        public string? DenialType { get; set; }
        public string? Description { get; set; }
        public string? DenialName { get; set; }
        public bool? IsInactive { get; set; }
    }
    public class DenialMasterResponse
    {
        public string? flag { get; set; }
        public string? message { get; set; }
        public List<DenialMaster>? data { get; set; } // Add this property
    }
}
