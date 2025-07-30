using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPayTimeEntryService
    {
        public PayTimeResponse Save(PayTimeEntry payData);
    }
}
