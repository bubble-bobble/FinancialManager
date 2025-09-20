using AutoMapper;
using FinancialManager.Web.Models.Entities;
using FinancialManager.Web.Models.ViewModels;

namespace FinancialManager.Web.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateAccountTypeViewModel, AccountType>();
        CreateMap<AccountType, EditAccountTypeViewModel>().ReverseMap();
    }
}