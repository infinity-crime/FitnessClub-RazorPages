using FitnessClub.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Subscription> Subscriptions { get; set; } = default!;
        public DbSet<MembershipPlan> Plans { get; set; } = default!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(x => x.Id);

                u.OwnsOne(x => x.Email, em =>
                {
                    em.Property(y => y.Value)
                        .HasColumnName("Email")
                        .HasMaxLength(50)
                        .IsRequired();
                });

                u.OwnsOne(x => x.PasswordHash, pwd =>
                {
                    pwd.Property(y => y.Hash)
                        .HasColumnName("PasswordHash")
                        .IsRequired();

                    pwd.Property(y => y.Salt)
                        .HasColumnName("PasswordSalt")
                        .IsRequired();
                });

                u.OwnsOne(x => x.FullName, fn =>
                {
                    fn.Property(y => y.Name)
                        .HasColumnName("Name")
                        .HasMaxLength(50)
                        .IsRequired();

                    fn.Property(y => y.Surname)
                        .HasColumnName("Surname")
                        .HasMaxLength(50)
                        .IsRequired();

                    fn.Property(y => y.Patronymic)
                        .HasColumnName("Patronymic")
                        .HasMaxLength(50)
                        .IsRequired();
                });

                u.OwnsOne(x => x.PhoneNumber, pn =>
                {
                    pn.Property(y => y.Value)
                        .HasColumnName("PhoneNumber")
                        .IsRequired();
                });

                u.HasMany(u => u.Subscriptions)
                 .WithOne(s => s.User)
                 .HasForeignKey(s => s.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MembershipPlan>(builder =>
            {
                builder.HasKey(p => p.Id);

                builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

                builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);

                builder.Property(p => p.DurationInMonths)
                .IsRequired();

                builder.OwnsOne(p => p.Price, prc =>
                {
                    prc.Property(y => y.Amount)
                    .HasColumnName("PriceAmount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                    prc.Property(y => y.Currency)
                    .HasColumnName("PriceCurrency")
                    .HasMaxLength(3)
                    .IsRequired();
                });
            });

            modelBuilder.Entity<Subscription>(builder =>
            {
                builder.HasKey(s => s.Id);

                builder.HasOne(s => s.MembershipPlan)
                .WithMany()
                .HasForeignKey(s => s.MembershipPlanId)
                .OnDelete(DeleteBehavior.Restrict); // нельзя удалить план, если на него есть подписки
            });
        }
    }
}
