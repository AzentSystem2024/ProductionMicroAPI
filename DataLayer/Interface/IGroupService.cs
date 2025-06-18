using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IGroupService
    {
        //IEnumerable<Group> GetAllGroupsOrdered();
        GroupResponse GetLogList(int? id = null);
    }
}
