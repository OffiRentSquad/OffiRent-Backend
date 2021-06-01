using System;
using Microsoft.EntityFrameworkCore;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Models.Geographic;
using OffiRent.Domain.Models.Identity;
using OffiRent.Persistence.Extensions;

namespace OffiRent.Persistence
{
    public class OffiRentDbContext : DbContext
    {
        public DbSet<BookingIntent> BookingIntents { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        
        public DbSet<User> Users { get; set; }

        public OffiRentDbContext(DbContextOptions<OffiRentDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BookingIntent>().HasKey(bi => bi.Id);
            builder.Entity<BookingIntent>().Property(bi => bi.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Entity<BookingIntent>().HasOne(bi => bi.Reservation)
                .WithOne(r => r.BookingIntent)
                .HasForeignKey<Reservation>(r => r.BookingIntentId);
            
            builder.Entity<BookingIntent>().HasData(new BookingIntent
            {
                Id = 1,
                IntentDate = DateTime.Now,
                PostId = 1,
                ReservationId = 1,
                UserId = 2,
                ReservationProposedStartDate = DateTime.Now.Add(TimeSpan.FromDays(12)),
                ReservationProposedEndDate = DateTime.Now.Add(TimeSpan.FromDays(30)),
                BookingIntentState = BookingIntentState.Accepted
            });
            
            builder.Entity<Office>().HasKey(o => o.Id);
            builder.Entity<Office>().Property(o => o.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Entity<Office>().HasData(
                new Office
                {
                    Id = 1,
                    UserId = 1,
                    Latitude = 38.8951m,
                    Longitude = -77.0364m,
                    Name = "Lorem Ipsum",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras gravida blandit pretium. Interdum et malesuada fames ac ante ipsum primis in faucibus. Praesent non tortor tincidunt, pellentesque mi quis, maximus metus. Duis venenatis justo suscipit finibus lobortis. Aliquam non pulvinar orci, eget auctor augue. Aliquam euismod lacus quam, id pharetra massa aliquet non. Interdum et malesuada fames ac ante ipsum primis in faucibus. Suspendisse id dui odio. Morbi sit amet elementum purus. Maecenas suscipit pellentesque augue. Nunc accumsan sem tempor, pulvinar mauris vitae, laoreet quam.",
                    Busy = true,
                });
            
            builder.Entity<Post>().HasKey(p => p.Id);
            builder.Entity<Post>().Property(u => u.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Entity<Post>().HasData(new Post
            {
                Id = 1,
                OfficeId = 1,
                UserId = 1,
                StartDate = DateTime.Now.Subtract(TimeSpan.FromDays(5)),
                EndDate = DateTime.Now,
                Title = "Vivamus ullamcorper",
                MonthlyPrice = 100,
                PostState = PostState.Finished
            });
            
            builder.Entity<Reservation>().HasKey(r => r.Id);
            builder.Entity<Reservation>().Property(r => r.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    BookingIntentId = 1,
                    OfficeId = 1,
                    UserId = 2,
                    StartTime = DateTime.Now.Add(TimeSpan.FromDays(12)),
                    EndTime = DateTime.Now.Add(TimeSpan.FromDays(30)),
                    ReservationState = ReservationState.Active
                });
            
            builder.Entity<Country>().HasKey(c => c.Id);
            builder.Entity<Country>().Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            builder.Entity<District>().HasKey(d => d.Id);
            builder.Entity<District>().Property(d => d.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            builder.Entity<User>().HasKey(u => u.Id);
            builder.Entity<User>().Property(u => u.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Entity<User>().HasData(new User
            {
                Id = 1,
                IsPremium = false,
                FirstName = "Pedro",
                Surname = "Castillo",
                FullName = "Pedro Castillo",
                Email = "pedro.castillo@gmail.com",
                Password = "PeruLibre100",
                PhoneNumber = "980178493",
                BirthDay = new DateTime(1969, 10, 19),
                LastOnline = DateTime.Now
            }, new User
            {
                Id = 2,
                IsPremium = false,
                FirstName = "Keiko",
                Surname = "Fujimori",
                FullName = "Keiko Fujimori",
                Email = "keiko.fujimori@gmail.com",
                Password = "FuerzaPopular100",
                PhoneNumber = "982749873",
                BirthDay = new DateTime(1975, 5, 25),
                LastOnline = DateTime.Now
            });

            builder.ApplySnakeCaseNamingConvention();
        }
    }
}