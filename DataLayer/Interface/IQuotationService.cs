using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IQuotationService
    {
        QuotationListResponse GetAllQuotations();
        QuotationDetailSelectResponse GetQuotation(int qtnId);
        int SaveData(Quotation quotation);
        QuotationResponse EditData(QuotationUpdate quotation);
        bool DeleteQuotation(int qtnId);
        ItemListResponse GetQuotationItems(QuotationRequest request);
        QuotationResponse ApproveQuotation(QuotationUpdate request);
    }
}
