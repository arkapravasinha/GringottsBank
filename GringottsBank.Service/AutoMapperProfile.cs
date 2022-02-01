using AutoMapper;
using GringottBank.DataAccess.EF.DataModels;
using GringottsBank.Service.DTO;

namespace GringottsBank.Service
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerResponseDTO>();
            CreateMap<Account, AccountResponseDTO>();
            CreateMap<Transaction, TransactionResponseDTO>();
            CreateMap<AccountCreationDTO, Account>();
        }
    }
}
