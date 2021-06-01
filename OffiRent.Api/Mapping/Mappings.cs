using AutoMapper;
using OffiRent.Api.DataObjects.BookingIntents;
using OffiRent.Api.DataObjects.Office;
using OffiRent.Api.DataObjects.Posts;
using OffiRent.Api.DataObjects.Reservations;
using OffiRent.Api.DataObjects.User;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Models.Identity;

namespace OffiRent.Api.Mapping
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<RegisterUser, User>();
            CreateMap<User, BasicUserView>();
            CreateMap<BasicUserView, User>();

            CreateMap<NewOffice, Office>();
            CreateMap<Office, NewOffice>();

            CreateMap<Post, NewPost>();
            CreateMap<NewPost, Post>();

            CreateMap<NewBookingIntent, BookingIntent>();
            CreateMap<BookingIntent, NewBookingIntent>();

            CreateMap<Reservation, ReservationSimpleView>();

        }
        

    }
}