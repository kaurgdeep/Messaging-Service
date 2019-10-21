﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingAPI.Models
{
    public class MessagingContext : DbContext
    {

        public MessagingContext(DbContextOptions<MessagingContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);// had to add this because was showing 
            // error:"The entity type 'IdentityUserLogin<string>' requires a primary key to be defined".

            // Note: Without this, we will get an error during 'update-database'
            // Note: Understand the ramifications of this completely.
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {

                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Note: This is the only way to add unique indexes in EF Core 2.x 
            // (In previous versions of EF, we were able to use a data annotation to denote uniqueness)
            // Use Fluent API
            modelBuilder.Entity<User>()
                .HasIndex(u => u.EmailAddress)
                .IsUnique();


            modelBuilder.Entity<UserFriend>()
               .HasIndex(u => new { u.UserId, u.FriendId })
               .IsUnique();

        }
    }
}
