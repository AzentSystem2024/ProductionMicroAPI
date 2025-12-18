using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPDCService
    {
        PDCResponse GetPDCList(PDCListRequest request);
        PDCSaveResponse SaveData(PDCModel pdc);
        PDCSaveResponse UpdateData(PDCModel pdc);
        PDCSelectResponse GetPDCById(int? id = null);
        PDCSaveResponse Delete(int id);
    }
}
