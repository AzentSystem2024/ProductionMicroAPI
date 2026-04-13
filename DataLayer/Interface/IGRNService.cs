using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IGRNService
    {
        public List<PO> GetPendingPo(Poinput input);
        public GRNResponse GetPoList(PODetailsInput input);
        public GRNResponse GetPoPendingItems(PendingPODetailsInput input);
        public List<GRN> GetGRNList(GRNListRequest request);
        public Int32 Insert(GRN company);
        public Int32 Update(GRN company);
        public Int32 Verify(GRN company);
        public Int32 Approve(GRN company);
        public GRN GetGRN(int id);
        public bool Delete(int id);
    }
}
