using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using dotnetVue.Helpers;
using static TeamworksInterrogationTool.api.Services.TicketController;

namespace dotnetVue.Models
{
    public partial class TeamworksArchiveContext : DbContext
    {
        public TeamworksArchiveContext()
        {
        }

        public TeamworksArchiveContext(DbContextOptions<TeamworksArchiveContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TicketComment> TicketComment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = ConfigurationHelper.GetConfiguration(System.IO.Directory.GetCurrentDirectory());

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("default"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.TicketReference);

                entity.ToTable("CSTicket");

                entity.Property(e => e.TicketReference).ValueGeneratedNever();

                entity.Property(e => e.AssignedTo).HasMaxLength(150);

                entity.Property(e => e.CategoryDescription).HasColumnName("CategoryDescription");

                entity.Property(e => e.CentreName).HasMaxLength(150);

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.ClosureCodeId).HasColumnName("ClosureCodeID");

                entity.Property(e => e.ContactEmail).HasMaxLength(150);

                entity.Property(e => e.ContactName).HasMaxLength(150);

                entity.Property(e => e.ContactTelephone).HasMaxLength(100);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RequesterName).HasMaxLength(150);

                entity.Property(e => e.ResolvedDate).HasColumnType("datetime");

                entity.Property(e => e.SourceSystemId).HasColumnName("sourceSystemId");

                entity.Property(e => e.SubCategoryDescription).HasColumnName("SubCategoryDescription");

                entity.Property(e => e.TicketStatus)
                    .IsRequired()
                    .HasColumnName("ticketStatus")
                    .HasMaxLength(100);

                entity.Property(e => e.TitanBwRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName);
            });

            modelBuilder.Entity<TicketComment>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("CSTicketComment");

                entity.Property(e => e.CommentId)
                    .HasColumnName("CommentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment).IsRequired();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TicketStatusId).HasColumnName("TicketStatusID");

                entity.Property(e => e.UserBrowser).HasMaxLength(50);
            });
        }
    }
}
