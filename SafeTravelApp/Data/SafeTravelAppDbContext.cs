using Microsoft.EntityFrameworkCore;
using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.Data
{
    public class SafeTravelAppDbContext : DbContext
    {
        
        public SafeTravelAppDbContext()
        {
        }

        public SafeTravelAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<UserDetails>? Details { get; set; }
        public virtual DbSet<Citizen>? Citizens { get; set; }
        public virtual DbSet<Agent>? Agents { get; set; }
        public virtual DbSet<Destination>? Destinations { get; set; }
        public virtual DbSet<CitizenDestination>? CitizenDestinations { get; set; }
        public virtual DbSet<Category>? Categories { get; set; }
        public virtual DbSet<Recommendation>? Recommendations { get; set; }
        public virtual DbSet<Certification>? Certifications { get; set; }
        public virtual DbSet<Language>? Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(e => e.UserRole).HasConversion<string>().IsRequired().HasDefaultValue(UserRole.Citizen);

                entity.Property(e => e.InsertedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();
                entity.HasIndex(e => e.Username, "IX_Users_Username").IsUnique();
                entity.HasIndex(e => e.Lastname, "IX_Users_Lastname");

            });
          
            modelBuilder.Entity<UserDetails>(entity =>
            {
                entity.ToTable("Details");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.PhoneNumber).HasMaxLength(10).IsRequired();

                entity.Property(e => e.InsertedAt)
               .ValueGeneratedOnAdd()
               .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => e.PhoneNumber, "IX_Details_PhoneNumber").IsUnique();
                entity.HasIndex(e => e.UserId, "IX_Details_UserId").IsUnique();
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("Agents");

                entity.Property(e => e.VatNumber).HasMaxLength(10).IsRequired();

                entity.Property(e => e.InsertedAt)
               .ValueGeneratedOnAdd()
               .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");


                entity.HasIndex(e => e.CompanyName, "IX_Agents_CompanyName");
                entity.HasIndex(e => e.VatNumber, "IX_Agents_VatNumber").IsUnique();
                entity.HasIndex(e => e.UserId, "IX_Agents_UserId").IsUnique();
            });
        
            modelBuilder.Entity<Certification>(entity =>
            {
                entity.ToTable("Certifications");

                entity.HasIndex(e => e.Title, "IX_Certifications_Title");
                entity.HasIndex(e => e.AgentId, "IX_Certifications_AgentId");
            });
       
            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Languages");

                entity.HasMany(d => d.Agents).WithMany(p => p.Languages)
                     .UsingEntity<Dictionary<string, object>>(
                         "AgentLanguages",
                         r => r.HasOne<Agent>().WithMany()
                             .HasForeignKey("AgentId")
                             .OnDelete(DeleteBehavior.ClientSetNull)
                             .HasConstraintName("FK_AgentLanguages_AgentId"),
                         l => l.HasOne<Language>().WithMany()
                             .HasForeignKey("LanguageId")
                             .OnDelete(DeleteBehavior.ClientSetNull)
                             .HasConstraintName("FK_AgentLanguages_LanguageId"),
                         j =>
                         {
                             j.HasKey("LanguageId", "AgentId");
                             j.ToTable("AgentLanguages");
                             j.HasIndex(new[] { "AgentId" }, "IX_AgentLanguages_AgentId");
                             j.HasIndex(new[] { "LanguageId" }, "IX_AgentLanguages_LanguageId");
                         });


                entity.HasIndex(e => e.Level, "IX_Languages_Level");
                entity.HasIndex(e => e.LanguageName, "IX_Languages_LanguageName"); 
            });
        
            modelBuilder.Entity<Citizen>(entity =>
            {
                entity.ToTable("Citizens");

                entity.Property(e => e.Gender).HasConversion<string>().HasDefaultValue(Gender.None);

                entity.Property(e => e.InsertedAt)
               .ValueGeneratedOnAdd()
               .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => e.UserId, "IX_Citizens_UserId").IsUnique();
            });
        
            modelBuilder.Entity<CitizenDestination>(entity =>
            {
                entity.ToTable("CitizenDestinations");

                entity.Property(e => e.CitizenRole).HasConversion<string>().IsRequired().HasDefaultValue(CitizenRole.Local);

                entity.Property(e => e.InsertedAt)
               .ValueGeneratedOnAdd()
               .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.ModifiedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");

                entity.HasKey(e => new { e.CitizenId, e.DestinationId });

                entity.HasIndex(e => e.CitizenId, "IX_CitizenDestinations_CitizenId");
                entity.HasIndex(e => e.DestinationId, "IX_CitizenDestinations_DestinationId");

            });
        
            modelBuilder.Entity<Destination>(entity =>
            {
                entity.ToTable("Destinations");

                entity.Property(e => e.Type).HasConversion<string>().IsRequired().HasDefaultValue(DestinationType.City);

                entity.HasIndex(e => e.Country, "IX_Destination_Country");
                entity.HasIndex(e => e.City, "IX_Destination_City");
                entity.HasIndex(e => new { e.Country, e.City }).HasDatabaseName("IX_Destinations_Country_City");   //Composite Index


                entity.HasMany(d => d.Agents).WithMany(p => p.Destinations)
                     .UsingEntity<Dictionary<string, object>>(
                         "AgentDestinations",
                         r => r.HasOne<Agent>().WithMany()
                             .HasForeignKey("AgentId")
                             .OnDelete(DeleteBehavior.ClientSetNull)
                             .HasConstraintName("FK_AgentDestinations_AgentId"),
                         l => l.HasOne<Destination>().WithMany()
                             .HasForeignKey("DestinationId")
                             .OnDelete(DeleteBehavior.ClientSetNull)
                             .HasConstraintName("FK_AgentDestinations_DestinationId"),
                         j =>
                         {
                             j.HasKey("DestinationId", "AgentId");
                             j.ToTable("AgentDestinations");
                             j.HasIndex(new[] { "AgentId" }, "IX_AgentDestinations_AgentId");
                             j.HasIndex(new[] { "DestinationId" }, "IX_AgentDestinations_DestinationId");
                         });

                
            });
        
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");

                entity.Property(e => e.DestinationCategory).HasConversion<string>().IsRequired().HasDefaultValue(DestinationCategory.Attractions);

                entity.HasMany(d => d.Destinations).WithMany(p => p.Categories)
                     .UsingEntity<Dictionary<string, object>>(
                         "DestinationCategories",
                         r => r.HasOne<Destination>().WithMany()
                             .HasForeignKey("DestinationId")
                             .OnDelete(DeleteBehavior.ClientSetNull)
                             .HasConstraintName("FK_DestinationCategories_DestinationId"),
                         l => l.HasOne<Category>().WithMany()
                             .HasForeignKey("CategoryId")
                             .OnDelete(DeleteBehavior.ClientSetNull)
                             .HasConstraintName("FK_DestinationCategories_CategoryId"),
                         j =>
                         {
                             j.HasKey("CategoryId", "DestinationId");
                             j.ToTable("DestinationCategories");
                             j.HasIndex(new[] { "DestinationId" }, "IX_DestinationCategories_DestinationId");
                             j.HasIndex(new[] { "CategoryId" }, "IX_DestinationCategories_CategoryId");
                         });

            });
        
            modelBuilder.Entity<Recommendation>(entity =>
            {
                entity.ToTable("Recommendations");

                entity.Property(e => e.ContributorRole).HasConversion<string>().IsRequired().HasDefaultValue(ContributorRole.Local);

                entity.HasOne(d => d.User).WithMany(p => p.Recommendations)
                    .HasForeignKey(d => d.ContributorId)
                    .HasConstraintName("FK_Recommendations_Users");

                entity.HasIndex(e => e.Title, "IX_Recommendations_Title");
                entity.HasIndex(e => e.ContributorId, "IX_Recommendations_ContributorId");
                entity.HasIndex(e => e.CategoryId, "IX_Recommendations_CategoryId");

            });
        }
    }
}
