using AutoMapper;
using Models.DTOS.Booking;
using Models.DTOS.Reviews;
using Models.DTOS.Room;
using Models.Entities;

namespace HotelServices.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 🟩 Room Mappings
            CreateMap<Room, RoomResponseDto>()
                .ForMember(dest=>dest.Images,opt=> opt.MapFrom(ser=>ser.Images.Select(i=>i.ImageUrl)))
                ;
            CreateMap<CreateRoomDto, Room>().ForMember(dest => dest.Images, opt => opt.Ignore()); ;
            CreateMap<UpdateRoomDto, Room>().ForMember(dest => dest.Images, opt => opt.Ignore()); ;
            CreateMap<Room, CreateRoomDto>();
            CreateMap<Room, UpdateRoomDto>();

            // 🟦 Booking Mappings
            CreateMap<Booking, BookingResponseDto>()
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room)) // ربط الغرفة داخل الحجز
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();
            CreateMap<Booking, BookingResponseByRoomIDDto>();

            CreateMap<CreateBookingDTO, Booking>();
            CreateMap<UpdateBookingDto, Booking>();
            CreateMap<Booking, CreateBookingDTO>();
            CreateMap<Booking, UpdateBookingDto>();

            // Reviews Mappings
            CreateMap<Review, AddReviewsDto>();
            CreateMap<AddReviewsDto, Review>();
            CreateMap<Review, ReviewResponseDto>()
                .ForMember(dest=>dest.UserName,
                opt=>opt.MapFrom(ser=>ser.User.UserName));
            CreateMap<ReviewResponseDto, Review>();



        }
    }
}
