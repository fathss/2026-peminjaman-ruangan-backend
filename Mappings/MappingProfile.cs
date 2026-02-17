using AutoMapper;
using PeminjamanRuanganAPI.Constants;
using PeminjamanRuanganAPI.Models;
using PeminjamanRuanganAPI.DTO;

namespace PeminjamanRuanganAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ------- Room Mappings -------
            CreateMap<Room, RoomResponseDto>();
            CreateMap<CreateRoomDto, Room>();
            CreateMap<UpdateRoomDto, Room>();

            // ------- RoomBooking Mappings -------
            CreateMap<RoomBooking, RoomBookingResponseDto>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.RoomDescription, opt => opt.MapFrom(src => src.Room.Description))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToLocalTime()))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToLocalTime()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToLocalTime()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToLocalTime() : (DateTime?)null));

            CreateMap<CreateRoomBookingDto, RoomBooking>();

            CreateMap<UpdateRoomBookingDto, RoomBooking>();

            // ------- BookingStatusHistory Mappings -------
            CreateMap<BookingStatusHistory, StatusHistoryDto>()
                .ForMember(dest => dest.ChangedBy, opt => opt.MapFrom(src => 
                    src.ChangedByUser != null 
                        ? (src.ChangedByUser.Role == "Admin" ? "Admin" : src.ChangedByUser.Username)
                        : "System"
                ))
                .ForMember(dest => dest.ChangedAt, opt => opt.MapFrom(src => src.ChangedAt.ToLocalTime()));
        }
    }
}