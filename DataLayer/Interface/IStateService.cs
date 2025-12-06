using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IStateService
    {
        public List<State> GetAllState();
        public Int32 SaveData(State company);
        public State GetItems(int id);
        public bool DeleteState(int id);
    }
}
