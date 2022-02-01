using GringottBank.DataAccess.EF.DataModels;

namespace GringottBank.DataAccess.Service.Abstractions
{
    public interface ITransactionRepository: IGenericRepository<Transaction>
    {
    }
}