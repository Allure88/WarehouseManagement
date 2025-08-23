using AutoMapper;
using WM.Application.Bodies;
using WM.Domain.Entities;
using WM.Domain.Models;

namespace WM.Application.Mapper_Profiles;

public class MainProfile : Profile
{
    public MainProfile()
    {

        CreateMap<ResourceBody, ResourceEntity>()
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ConvertToState()));

        CreateMap<ResourceEntity, ResourceBody>()
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ConvertToString()));

        CreateMap<ClientBody, ClientEntity>()
           .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ConvertToState()));

        CreateMap<ClientEntity, ClientBody>()
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ConvertToString()));

        CreateMap<UnitBody, UnitEntity>()
          .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ConvertToState()));

        CreateMap<UnitEntity, UnitBody>()
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ConvertToString()));

        CreateMap<BalanceBody, BalanceEntity>();
        CreateMap<BalanceEntity, BalanceBody>();

        CreateMap<AdmissionDocBody, AdmissionDocEntity>()
            .ForMember(dest => dest.AdmissionRes, opt => opt.MapFrom(src => src.ResBody));

        CreateMap<AdmissionDocEntity, AdmissionDocBody>()
           .ForMember(dest => dest.ResBody, opt => opt.MapFrom(src => src.AdmissionRes));

        CreateMap<AdmissionResBody, AdmissionResEntity>();
        CreateMap<AdmissionResEntity, AdmissionResBody>();

        CreateMap<ShippingResBody, ShippingResEntity>();
        CreateMap<ShippingResEntity, ShippingResBody>();

        CreateMap<ShippingDocBody, ShippingDocEntity>()
          .ForMember(dest => dest.ShippingRes, opt => opt.MapFrom(src => src.ResBody))
          .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ConvertToStatus()));

        CreateMap<ShippingDocEntity, ShippingDocBody>()
           .ForMember(dest => dest.ResBody, opt => opt.MapFrom(src => src.ShippingRes))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ConvertToString()));
    }

}


public static class Extensions
{
    public static State ConvertToState(this string stateStr)
    {
        stateStr = stateStr.ToLower();
        if (stateStr == "active")
            return State.Active;
        else if (stateStr == "archived")
            return State.Archived;
        else
            throw new NotImplementedException($"Невозможно преобразовать {stateStr} в состояние");
    }

    public static string ConvertToString(this State state)
    {
        return state.ToString();
    }


    public static DocumentStatus ConvertToStatus(this string stateStr)
    {
        stateStr = stateStr.ToLower();
        if (stateStr == "created")
            return DocumentStatus.Created;
        else if (stateStr == "approved")
            return DocumentStatus.Approved;
        else if (stateStr == "revocated")
            return DocumentStatus.Revocated;
        else
            throw new NotImplementedException($"Невозможно преобразовать {stateStr} в состояние");
    }

    public static string ConvertToString(this DocumentStatus status)
    {
        return status.ToString();
    }
}
