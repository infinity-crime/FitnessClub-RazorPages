using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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

            // Конфигурация сущности User
            modelBuilder.Entity<User>(u =>
            {
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

            // Конфигурация сущности Subscription
            modelBuilder.Entity<Subscription>(builder =>
            {
                builder.HasOne(s => s.MembershipPlan)
                .WithMany()
                .HasForeignKey(s => s.MembershipPlanId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            // Конфигурация и заполнение данных сущности MembershipPlan
            SeedMembershipPlans(modelBuilder);
        }

        private void SeedMembershipPlans(ModelBuilder modelBuilder)
        {
            var plans = new[]
            {
                new { Id = Guid.Parse("e356bb37-2be5-4ad6-9a66-339e4f10094a"), Name = "Новичок", Description = "Абонемент на 1 месяц, чтобы познакомиться с клубом.", Duration = 1, Amount = 3590m },
                new { Id = Guid.Parse("3c1121e5-7282-4d1f-9d57-680b9e36462b"), Name = "Стандарт 1", Description = "Стандартный абонемент на 1 месяц.", Duration = 1, Amount = 3990m },
                new { Id = Guid.Parse("4bfb3e5e-9d54-40cd-9388-4ad78acdaef4"), Name = "Стандарт 3", Description = "Выгодный абонемент на 3 месяца.", Duration = 3, Amount = 10990m },
                new { Id = Guid.Parse("ac75452c-6ab6-42cc-9d24-b5e120984286"), Name = "Стандарт 6", Description = "Полугодовой абонемент для регулярных занятий.", Duration = 6, Amount = 19990m },
                new { Id = Guid.Parse("9ba1a5e7-35cc-4e91-93be-1fe2e27de326"), Name = "Стандарт 12", Description = "Годовой абонемент с максимальной выгодой.", Duration = 12, Amount = 35990m },
                new { Id = Guid.Parse("d86dec31-d099-43f0-b860-8976f44e50ff"), Name = "Студенческий", Description = "Специальный абонемент для студентов на 3 месяца.", Duration = 3, Amount = 8990m },
                new { Id = Guid.Parse("b5adb578-bc5d-4d48-a7f1-137185eb9661"), Name = "Утренний", Description = "Абонемент для посещений в утренние часы (c 6:00 до 12:00).", Duration = 1, Amount = 2990m },
                new { Id = Guid.Parse("78dcd9cd-2373-4cf0-8f64-f32464961788"), Name = "Дневной", Description = "Абонемент для посещений в дневные часы (c 12:00 до 17:00).", Duration = 1, Amount = 3290m }
            };

            modelBuilder.Entity<MembershipPlan>(builder =>
            {
                // Конфигурация простых свойств
                builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

                builder.Property(p => p.Description)
                .HasMaxLength(500)
                .IsRequired();

                builder.Property(p => p.DurationInMonths)
                .IsRequired();

                // Конфигурация ValueObject свойств 
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

                // Начальное заполнение данных для абонементов (простые свойства)
                builder.HasData(plans.Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    DurationInMonths = p.Duration
                }));

                // Начальное заполнение данных для абонементов (ValueObjects)
                builder.OwnsOne(p => p.Price).HasData(plans.Select(p => new
                {
                    MembershipPlanId = p.Id,
                    p.Amount,
                    Currency = "RUB"
                }));
            });
        }
    }
}
