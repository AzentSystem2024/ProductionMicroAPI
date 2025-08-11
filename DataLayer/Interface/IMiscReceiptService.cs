using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IMiscReceiptService
    {
        MiscReceiptResponse Insert(MiscReceipt model);
        MiscReceiptResponse Update(MiscReceiptUpdate model);
        MiscReceiptListResponse GetReceiptList();
        MiscReceiptResponse GetMiscReceiptById(int id);
        LasrVoucherResponse GetLastVoucherNo();
        MiscReceiptResponse Delete(int id);
        MiscReceiptResponse commit(MiscReceiptUpdate model);

    }
}
