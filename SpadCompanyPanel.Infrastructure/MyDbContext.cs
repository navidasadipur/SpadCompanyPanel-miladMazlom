using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using SpadCompanyPanel.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SpadCompanyPanel.Infrastructure
{
    public class MyDbContext : IdentityDbContext<User>
    {
        //public DbSet<Article> Articles { get; set; }
        //public DbSet<ArticleCategory> ArticleCategories { get; set; }
        //public DbSet<ArticleComment> ArticleComments { get; set; }

        //public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<StaticContentType> StaticContentTypes { get; set; }
        public DbSet<StaticContentDetail> StaticContentDetails { get; set; }
        //public DbSet<OurTeam> OurTeams { get; set; }
        //public DbSet<Testimonial> Testimonials { get; set; }
        //public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceInclude> ServiceIncludes { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        //public DbSet<Faq> Faqs { get; set; }
        public DbSet<Log> Logs { get; set; }
        //public DbSet<Food> Foods { get; set; }
        //public DbSet<FoodType> FoodTypes { get; set; }
        //public DbSet<FoodGallery> FoodGalleries { get; set; }
        public DbSet<GalleryVideo> GalleryVideos { get; set; }

        public DbSet<Cover> Covers { get; set; }

        public DbSet<AboutMe> AboutMes { get; set; }

        public DbSet<GalleryCategory> GalleryCategories { get; set; }

        public DbSet<PersonalCharacter> PersonalCharacters { get; set; }

        public DbSet<MyContactInfo> MyContactInfos { get; set; }
    }
}
