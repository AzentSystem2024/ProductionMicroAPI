using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IGateIssueService
    {
        GateIssueResponse InsertGateIssue(GateIssue model);
    }
}
