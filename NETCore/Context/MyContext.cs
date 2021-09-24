using Microsoft.EntityFrameworkCore;
using NETCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Context
{
    public class MyContext:DbContext //Gateway dengan database
    {
        public MyContext(DbContextOptions<MyContext>options):base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profilling> Profillings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // one to one
            modelBuilder.Entity<Person>()
                .HasOne(a => a.Account)
                .WithOne(p => p.Person)
                .HasForeignKey<Account>(a => a.NIK);

            //one to one
            modelBuilder.Entity<Account>()
                .HasOne(pr => pr.Profilling)
                .WithOne(a => a.Account)
                .HasForeignKey<Profilling>(pr => pr.NIK);

            // one to many
            modelBuilder.Entity<Profilling>()
                .HasOne(e => e.Education)
                .WithMany(pr => pr.Profillings)
                .OnDelete(DeleteBehavior.SetNull);
            // modelBuilder.Entity<Education>()
               // .HasMany(pr => pr.Profillings)
               // .WithOne(e => e.Education)
               // .OnDelete(DeleteBehavior.SetNull);

            // many to one
            modelBuilder.Entity<University>()
                .HasMany(e => e.Educations)
                .WithOne(u => u.University)
                .OnDelete(DeleteBehavior.SetNull);
            //many to many antara account dan role, dijembatani oleh accountrole
            modelBuilder.Entity<AccountRole>()
                .HasKey(ar => new { ar.AccountNIK, ar.RoleId });
            // one to many
            modelBuilder.Entity<AccountRole>()
                .HasOne(a => a.Account)
                .WithMany(ar => ar.AccountRoles)
                .HasForeignKey(a => a.AccountNIK);
            // one to many
            modelBuilder.Entity<AccountRole>()
                .HasOne(r => r.Role)
                .WithMany(ar => ar.AccountRoles)
                .HasForeignKey(r => r.RoleId);
        }
    }


}
