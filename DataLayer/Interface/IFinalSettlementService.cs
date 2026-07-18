using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IFinalSettlementService
    {
        FinalSettlementResponse GetFinalSettlement(FinalSettlementRequest request);

    }
}
