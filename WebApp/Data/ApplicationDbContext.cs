using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NFCSystem.Models;

namespace NFCSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
                builder.Entity<ApplicationUser>(entity => { 
                entity.Property(m => m.Email).HasMaxLength(127); 
                entity.Property(m => m.NormalizedEmail).HasMaxLength(127); 
                entity.Property(m => m.NormalizedUserName).HasMaxLength(127); 
                entity.Property(m => m.UserName).HasMaxLength(127);
                });
            
                builder.Entity<IdentityRole>(entity => { 
                    entity.Property(m => m.Name).HasMaxLength(127); 
                    entity.Property(m => m.NormalizedName).HasMaxLength(127); 
                }); 
                builder.Entity<IdentityUserLogin<string>>(entity => 
                { 
                    entity.Property(m => m.LoginProvider).HasMaxLength(127); 
                    entity.Property(m => m.ProviderKey).HasMaxLength(127); 
                }); 
                builder.Entity<IdentityUserRole<string>>(entity => 
                { 
                    entity.Property(m => m.UserId).HasMaxLength(127); 
                    entity.Property(m => m.RoleId).HasMaxLength(127); 
                }); 
                builder.Entity<IdentityUserToken<string>>(entity => 
                { 
                    entity.Property(m => m.UserId).HasMaxLength(127); 
                    entity.Property(m => m.LoginProvider).HasMaxLength(127); 
                    entity.Property(m => m.Name).HasMaxLength(127); 
                
                }); 
                base.OnModelCreating(builder);
        }
    }
}