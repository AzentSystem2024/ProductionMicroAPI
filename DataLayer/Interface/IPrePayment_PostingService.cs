using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPrePayment_PostingService
    {
        PrePayment_RequestResponse GetPrePaymentPendingList(PrePayment_PostingRequest request);
        PrepaymentPostingResponse Save(PrePayment_Posting model);
        PrePayment_PostingListResponse GetPrePaymentList();


    }
}
