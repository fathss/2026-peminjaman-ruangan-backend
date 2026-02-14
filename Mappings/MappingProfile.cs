using AutoMapper;
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
                .ForMember(dest => dest.RoomName,
                    opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User.Username));

            CreateMap<CreateRoomBookingDto, RoomBooking>();

            CreateMap<UpdateRoomBookingDto, RoomBooking>();
        }
    }
}