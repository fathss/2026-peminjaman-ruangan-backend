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
        }
    }
}