using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IAccountService
    {

        AccountResponse Insert(Account account);
        AccountResponse Update(AccountUpdate account);
        AccountResponse GetAccountById(int id);
        AccountListResponse GetLogList(int? id = null);
        AccountResponse DeleteAccountData(int id);
    }
}
