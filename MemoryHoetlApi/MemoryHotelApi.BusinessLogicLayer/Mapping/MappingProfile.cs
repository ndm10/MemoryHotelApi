using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AuthenticationDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AuthenticationDto;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace ThomVietApi.BusinessLogicLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region UserMapping
            CreateMap<User, ResponseLoginDto>().ReverseMap();
            CreateMap<User, RequestRegisterDto>().ReverseMap();
            #endregion

            #region BannerMapping
            CreateMap<Banner, UploadBannerDto>().ReverseMap();
            #endregion
        }
    }
}
