using AutoMapper;
using WM.Application.Bodies;
using WM.Domain.Entities;

namespace WM.Application.Mapper_Profiles;

public class MainProfile : Profile
{
    public MainProfile()
    {
        CreateMap<UnitBody, UnitEntity>();
        CreateMap<StateBody, ShippingDocEntity>();
        CreateMap<ResourceBody, ResourceEntity>();
        CreateMap<ClientBody, ClientEntity>();
        CreateMap<BalanceBody, BalanceEntity>();
        CreateMap<AdmissionDocBody, AdmissionDocEntity>();
        CreateMap<AdmissionResBody, AdmissionResEntity>();
        CreateMap<ShippingDocBody, ShippingDocEntity>();
        CreateMap<ShippingResBody, ShippingResEntity>();
        // Если нужно обратно:
        // CreateMap<UnitEntity, UnitBody>();
    }
}
