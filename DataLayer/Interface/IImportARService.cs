using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IImportARService
    {
        public bool Import(ImportARInput Vinput);
        public ImportARResponse List(ImportARInput Vinput);
        public viewImportARDataResponse ViewDetails(viewImportARInput vInput);
        public ImportArColumnsResponse columnsList();
        public processARResponse processAR(processARInput vinput);
        public ARResponse arList(ARInput input);
    }
}
