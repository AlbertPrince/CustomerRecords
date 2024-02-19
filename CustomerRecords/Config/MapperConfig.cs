using AutoMapper;
using CustomerRecords.Api.Dto;
using CustomerRecords.Api.Models;
using CustomerRecords.Api.Request;

namespace CustomerRecords.Api.Config
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<CreateTransactionRequest, Transaction>();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<TransactionReport, TransactionReportDto>().ReverseMap();
        }
    }
}
