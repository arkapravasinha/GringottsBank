using GringottBank.DataAccess.EF.DataModels;
using GringottBank.DataAccess.Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottsBank.BusinessLogic.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IGringottBankUnitOfWork _gringottBankUnitofWork;

        public CustomerService(IGringottBankUnitOfWork gringottBankUnitofWork)
        {
            _gringottBankUnitofWork = gringottBankUnitofWork;
        }

        public async Task<int> CreateCustomer(Customer customer, string userId = "testUserId")
        {

            if (customer == null)
                throw new ArgumentNullException("customer can not be null");
            try
            {
                await _gringottBankUnitofWork.Database.BeginTransactionAsync();
                await _gringottBankUnitofWork.CustomerRepository.Add(customer);
                await _gringottBankUnitofWork.SaveChangesAsync(userId);
                await _gringottBankUnitofWork.Database.CommitTransactionAsync();
                return customer.CustomerId;
            }
            catch (Exception)
            {
                await _gringottBankUnitofWork.Database.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<Customer> GetCustomerById(int customerid)
        {
            if (customerid < 100)
                throw new ArgumentOutOfRangeException("customer id should be greater than 100");
            var customers = await _gringottBankUnitofWork.CustomerRepository.Find(x => x.CustomerId == customerid);
            var customer = customers.FirstOrDefault();
            if (customer == null)
                throw new Exception("Customer is not present");
            return customer;
        }
    }
}
