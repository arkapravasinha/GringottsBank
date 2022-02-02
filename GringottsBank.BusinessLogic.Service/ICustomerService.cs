using GringottBank.DataAccess.EF.DataModels;
using System.Threading.Tasks;

namespace GringottsBank.BusinessLogic.Service
{
    public interface ICustomerService
    {
        Task<int> CreateCustomer(Customer customer, string userId = "testUserId");
        Task<Customer> GetCustomerById(int customerid);
    }
}