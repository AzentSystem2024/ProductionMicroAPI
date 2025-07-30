using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IOpeningBalanceService
    {
        OBResponse GetopeningBalance(OBRequest request);
        OBResponse Insert(AcOpeningBalanceInsertRequest request);
        OBResponse CommitOpeningBalance(OBCommitRequest request);

    }
}
