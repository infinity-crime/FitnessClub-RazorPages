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

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(x => x.Id);

                u.OwnsOne(u => u.Email);
                u.OwnsOne(u => u.PhoneNumber);
                u.OwnsOne(u => u.FullName);
                u.OwnsOne(u => u.PasswordHash);

                u.OwnsOne(x => x.Email, em =>
                {
                    em.Property(y => y.Value)
                        .HasColumnName("Email")
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
                        .IsRequired();

                    fn.Property(y => y.Surname)
                        .HasColumnName("Surname")
                        .IsRequired();

                    fn.Property(y => y.Patronymic)
                        .HasColumnName("Patronymic")
                        .IsRequired();
                });

                u.OwnsOne(x => x.PhoneNumber, pn =>
                {
                    pn.Property(y => y.Value)
                        .HasColumnName("PhoneNumber")
                        .IsRequired();
                });
            });
        }
    }
}
