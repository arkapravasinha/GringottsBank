

using GringottBank.DataAccess.EF.DataModels;
using System.Threading.Tasks;

namespace GringottBank.DataAccess.Service.Abstractions
{
    public interface IAccountRepository:IGenericRepository<Account>
    {
        Task<Account> FindById(int id);
    }
}
