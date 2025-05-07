using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AuthenticationDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AccountDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AuthenticationDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region UserMapping
            CreateMap<User, ResponseLoginDto>().ReverseMap();
            CreateMap<User, RequestRegisterDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, ResponseGetProfileDto>();
            CreateMap<RequestUploadAdminAccountDto, User>();
            #endregion

            #region BannerMapping
            CreateMap<Banner, RequestUploadBannerDto>().ReverseMap();
            CreateMap<Banner, GetBannerDto>().ReverseMap();
            CreateMap<Banner, ResponseGetBannersExploreDto>().ReverseMap();
            #endregion

            #region StoryMapping
            CreateMap<Story, GetStoryDto>().ReverseMap();
            CreateMap<Story, RequestUploadStoryDto>().ReverseMap();
            CreateMap<Story, StoryExploreDto>();
            #endregion

            #region CityMapping
            CreateMap<City, GetCityDto>().ReverseMap();
            CreateMap<City, RequestUploadCityDto>().ReverseMap();
            CreateMap<City, CityExploreDto>().ReverseMap();
            #endregion

            #region ImageMapping
            CreateMap<Image, ResponseGetImageDtoCommon>().ReverseMap();
            #endregion

            #region TourMapping
            CreateMap<GetTourDto, Tour>().ReverseMap().ForMember(dest => dest.Images, otp => otp.MapFrom(src => src.Images.Select(img => img.Url).ToList()));
            CreateMap<Tour, TourExploreDto>().ForMember(dest => dest.Images, otp => otp.MapFrom(src => src.Images.Select(img => img.Url).ToList()));
            CreateMap<Tour, RequestUploadTourDto>().ReverseMap();
            CreateMap<Tour, TourDetailDto>().ReverseMap();
            #endregion

            #region SubTourMapping
            CreateMap<GetSubTourDto, SubTour>().ReverseMap().ForMember(dest => dest.Images, otp => otp.MapFrom(src => src.Images.Select(img => img.Url).ToList()));
            CreateMap<SubTour, SubTourExploreDto>().ForMember(dest => dest.Images, otp => otp.MapFrom(src => src.Images.Select(img => img.Url).ToList()));
            CreateMap<SubTour, RequestUploadSubTourDto>().ReverseMap();
            #endregion

            #region CityMapping
            CreateMap<City, ResponseGetCityDtoCommon>().ReverseMap();
            #endregion

            #region ConvenienceMapping
            CreateMap<Convenience, ConvenienceDto>().ReverseMap();
            CreateMap<Convenience, GetConvenienceDtoCommon>().ReverseMap();
            CreateMap<Convenience, RequestUploadConvenienceDto>().ReverseMap();
            #endregion

            #region BranchMapping
            CreateMap<BranchDto, Branch>().ReverseMap().ForMember(dest => dest.Images, otp => otp.MapFrom(src => src.BranchImages.Select(img => img.Image.Url).ToList()));
            CreateMap<Branch, RequestUploadBranchDto>().ReverseMap();
            CreateMap<Branch, RoomBranchDto>();
            #endregion

            #region LocationExploreMapping
            CreateMap<LocationExplore, UploadLocationExploreDto>().ReverseMap();
            CreateMap<LocationExplore, ResponseGetLocationExploreDtoCommon>().ReverseMap();
            CreateMap<GetBranchesExploreDto, Branch>().ReverseMap().ForMember(dest => dest.Images, otp => otp.MapFrom(src => src.BranchImages.Select(img => img.Image.Url).ToList()));
            #endregion

            #region RoomCategoryMapping
            CreateMap<RoomCategory, RoomCategoryDto>().ReverseMap();
            CreateMap<RoomCategory, RequestUploadRoomCategoryDto>().ReverseMap();
            CreateMap<RoomCategory, RoomCategoryExploreDto>().ReverseMap();
            #endregion

            #region MembershipTierMapping
            CreateMap<MembershipTier, MembershipTierDto>()
                .ForMember(dest => dest.Benefits, opt => opt.MapFrom(src => src.Benefits));
            CreateMap<MembershipTier, MembershipTierDtoCommon>();
            CreateMap<RequestUploadMembershipTierDto, MembershipTier>().ForMember(dest => dest.Benefits, otp => otp.Ignore());
            #endregion

            #region MembershipTierBenefitMapping
            CreateMap<MembershipTierBenefit, MembershipTierBenefitDto>().ReverseMap();
            CreateMap<MembershipTierBenefit, RequestUploadMembershipTierBenefitDto>().ReverseMap();
            #endregion

            #region MembershipTierMembershipTierBenefitMapping
            CreateMap<MembershipTierMembershipTierBenefit, MembershipTierMembershipTierBenefitDto>()
                .ForMember(dest => dest.Id, otp => otp.MapFrom(src => src.MembershipTierBenefit.Id))
                .ForMember(dest => dest.Benefit, otp => otp.MapFrom(src => src.MembershipTierBenefit.Benefit))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<MembershipTierMembershipTierBenefit, MembershipTierMembershipTierBenefitDtoCommon>()
                .ForMember(dest => dest.Id, otp => otp.MapFrom(src => src.MembershipTierBenefit.Id))
                .ForMember(dest => dest.Benefit, otp => otp.MapFrom(src => src.MembershipTierBenefit.Benefit))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));
            #endregion

            #region RoomMapping
            CreateMap<RequestUploadRoomDto, Room>();
            CreateMap<Room, RoomExploreDto>().ForMember(dest => dest.Images, otp => otp.MapFrom(src => src.Images.Select(img => img.Url).ToList()));
            CreateMap<Room, RoomDto>().ForMember(dest => dest.Images, otp => otp.MapFrom(src => src.Images.Select(img => img.Url).ToList()));
            #endregion
        }
    }
}
