using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroApi.Models
{
    public class CptMaster
    {
        public int? ID { get; set; }
        public int? CPTTypeID { get; set; }
        public string? CPTCode { get; set; }
        public string? CPTShortName { get; set; }
        public string? CPTName { get; set; }
        public string? Description { get; set; }
        public bool? IsInactive { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CPTType { get; set; }
        public string? CPTGroup { get; set; }
        public int? DepartmentID { get; set; }
        public int? CPTDepartmentID { get; set; }
        public int? CostDepartmentID { get; set; }
        public string? Department { get; set; }
        public string? CostDepartment { get; set; }
        public string? CPTDepartment { get; set; }
        public string? CostDrive { get; set; }
        public int? CostDriveID { get; set; }
        public float? FixedQuantity { get; set; }
        public bool? IsDifferentCPTDepartment { get; set; }
        public bool? IsDifferentLedger { get; set; }
        public string? SelectedLedgerID { get; set; }
        public List<costSettingsData>? data { get; set; }
        public List<cptEncounterDepartmentData>? CPTEncounterDepartments { get; set; }
    }

    public class cptEncounterDepartmentData
    {
        public int? CPTID { get; set; }
        public string? EncounterType { get; set; }
        public int? DepartmentID { get; set; }
    }

    public class costSettingsData
    {
        //public int CostBucketID { get; set; }
        //public string Description { get; set; }
        //public decimal RVU { get; set; }
        //public int CostTypeID { get; set; }
        //public int ClinicianID { get; set; }
        //public int ClinicianRoleID { get; set; }
        public string? FacilityID { get; set; }
        public decimal? RVU_Doctor { get; set; }
        public decimal? RVU_Nurse { get; set; }
        public decimal? RVU_Allied { get; set; }
        public decimal? RVU_Cost { get; set; }

    }

    public class CptMasterResponse
    {
        public string? flag { get; set; }
        public string? message { get; set; }

        public List<CptMaster>? data { get; set; } // Add this property
    }

    public class subDepartment
    {
        public int? ID { get; set; }
        public string? DESCRIPTION { get; set; }
    }

    public class subDepartmentResponse
    {
        public string? flag { get; set; }
        public string? message { get; set; }
        public List<subDepartment>? data { get; set; }
    }

 

}
