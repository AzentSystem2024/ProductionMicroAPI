using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IImportChartOfAccountsService
    {
        public bool Import(ImportAccountsInput Vinput);
        public ImportAccountsResponse List(ImportAccountsInput Vinput);
        public viewImportAccountsDataResponse ViewDetails(viewImportAccountsInput vInput);
    }
}
