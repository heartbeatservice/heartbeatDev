﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HBS.Data.Entities.TimeTracking.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TimeTrackingEntities : DbContext
    {
        public TimeTrackingEntities()
            : base("name=TimeTrackingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AllowedUserIPAddress> AllowedUserIPAddresses { get; set; }
        public DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public DbSet<aspnet_Users> aspnet_Users { get; set; }
        public DbSet<ExtendedUserProfile> ExtendedUserProfiles { get; set; }
        public DbSet<UserTimeTrackHistory> UserTimeTrackHistories { get; set; }
        public DbSet<WebMenu> WebMenus { get; set; }
    }
}
