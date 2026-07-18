using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Models
{



    public class FinalSettlementRequest
    {
        public int EmployeeId { get; set; }
    }

    public class FinalSettlementResponse
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string SettlementType { get; set; }

        public DateTime? DateOfJoining { get; set; }
        public DateTime? LastWorkingDay { get; set; }

        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }

        public decimal UnPaidLeaveDays { get; set; }
        public decimal TotalServiceDays { get; set; }

        public decimal IndemnityFirstFiveYears { get; set; }
        public decimal IndemnityAfterFiveYears { get; set; }
        public decimal TotalIndemnityDays { get; set; }

        public decimal EntitledLeaveDays { get; set; }

        public decimal IndemnityAmount { get; set; }
        public decimal LeaveSalary { get; set; }

        public List<FinalSettlementComponent> SalaryComponents { get; set; }

        public decimal TotalAmount { get; set; }
    }

    public class FinalSettlementComponent
    {
        public string HeadName { get; set; }
        public string SalaryMonth { get; set; }
        public decimal Amount { get; set; }
    }

}
