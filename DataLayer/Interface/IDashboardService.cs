using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDashboardService
    {
        DashboardResponse GetDashboardSummary(DashboardRequest request);
        DashboarddataResponse GetDashboard(DashboardRequest request);
    }
}
