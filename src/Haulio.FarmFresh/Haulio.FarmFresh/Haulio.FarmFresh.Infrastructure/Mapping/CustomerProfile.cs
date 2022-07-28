using AutoMapper;
using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Infrastructure.ViewModel;

namespace Haulio.FarmFresh.Infrastructure.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerModel, Customer>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.CustomerId))
                .ReverseMap();
        }
    }
}
