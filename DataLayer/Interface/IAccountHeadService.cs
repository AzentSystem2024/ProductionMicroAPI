using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IAccountHeadService
    {
        AccountHeadResponse Insert(AccountHead accountHead);
        AccountHeadResponse Update(AccountHeadUpdate accountHead);
        AccountHeadResponse GetAccountHeadById(int id);
        AccountHeadListResponse GetLogList(int? id = null);
        AccountHeadResponse DeleteAccountHeadData(int id);
    }
}
