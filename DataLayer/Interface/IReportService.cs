using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IReportService
    {
        public ReportListData GetReportInitData();
        public SaveReportResponse Insert(Report report);
        public Report GetReportById(int id);
        public SaveReportResponse Update(Report report);

    }
}
