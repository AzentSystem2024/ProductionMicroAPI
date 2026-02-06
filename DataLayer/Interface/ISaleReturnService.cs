using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISaleReturnService
    {
        SaleReturnResponse Insert(SaleReturn model);
        SaleReturnListResponse GetSaleReturnList(SaleReturnListRequest request);
        SaleInvoiceDetailResponse GetSalesInvoiceDetail(InvoieRequest request);
        public Int32 InsertSaleReturn(SaleReturnInsertRequest model);
        public Int32 UpdateSaleReturn(SaleReturnInsertRequest model);
        public Int32 CommitSaleReturn(SaleReturnInsertRequest model);
        SaleReturnViewResponse GetSaleReturnById(int id);
        public bool Delete(int id);
    }
}
