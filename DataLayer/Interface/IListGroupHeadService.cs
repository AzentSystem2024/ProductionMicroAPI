using MicroApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroApi.Models;
namespace MicroApi.DataLayer.Interface
{
    public interface IListGroupHeadService
    {
        //List<ListGroupHead> GetLogList(int id);
        // ListResponse GetLogList(int? id = null);
        ListResponse GetLogList(int? id = null);
    }

}
