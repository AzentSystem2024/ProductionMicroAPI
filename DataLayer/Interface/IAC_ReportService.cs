using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IAC_ReportService
    {
        LedgerReportInitData GetInitData(int companyId);
        LedgerStatementResponse GetLedgerStatement(AC_Report request);
        ArticleProductionResponse GetArticleProductionReport(ArticleProductionFilter request);
        BoxProductionResponse GetBoxProductionReport(BoxProductionFilter request);
        CashBookResponse GetCashBookReport(CashBookFilter request);
        BalanceSheetResponse GetBalanceSheetReport(BalanceSheetFilter request);
        ProfitLossReportResponse GetProfitLossReport(ProfitLossReportRequest request);
        SupplierStatReportResponse GetSupplierStateReports(SupplierStatReportRequest request);
        SupplierStatDetailReportResponse GetSupplierStateDetailReports(SupplierStatReportRequest request);
        AgedPayableReportResponse GetAgedPayableReports(AgedPayableReportRequest request);
        CustomerStatementResponse GetCustomerStatement(Customer_Statement_Request request);
        CustomerAgingResult GetCustomerAging(Customer_Aging request);
        CustomerStatementDetailResponse GetCustomerStatementDetail(CustomerStatementRequest request);
        InputVatWorksheetReportResponse GetInputVatWorksheetReport(InputVatWorksheetReportRequest request);
        OutputVatWorksheetReportResponse GetOutputVatWorksheetReport(OutputVatWorksheetReportRequest request);
        VatReturnReportResponse GetVatReturnReport(VatReturnReportRequest request);
        

    }
}
