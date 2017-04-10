using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TNW.Models;
using TNW.ViewModels;

namespace TNW.App_Start
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountType, AccountTypeViewModel>().ReverseMap();
            CreateMap<AssetType, AssetTypeViewModel>().ReverseMap();
            CreateMap<CurrencyType, CurrencyTypeViewModel>().ReverseMap();
        }
    }
}
