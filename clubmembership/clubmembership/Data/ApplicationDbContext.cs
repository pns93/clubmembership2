﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using clubmembership.Models.DBObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace clubmembership.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Announcement> Announcements { get; set; } = null!;
        //public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        //public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<CodeSnippet> CodeSnippets { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Membership> Memberships { get; set; } = null!;
        public virtual DbSet<MembershipType> MembershipTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.HasKey(e => e.Idannouncement)
                    .HasName("PK__Announce__452C71ACAA79DE58");

                entity.Property(e => e.Idannouncement)
                    .ValueGeneratedNever()
                    .HasColumnName("IDAnnouncement");

                entity.Property(e => e.EventDateTime).HasColumnType("datetime");

                entity.Property(e => e.Tags)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidTo).HasColumnType("datetime");
            });


            modelBuilder.Entity<CodeSnippet>(entity =>
            {
                entity.HasKey(e => e.IdcodeSnippet)
                    .HasName("PK__CodeSnip__39AC88214848E0E7");

                entity.Property(e => e.IdcodeSnippet)
                    .ValueGeneratedNever()
                    .HasColumnName("IDCodeSnippet");

                entity.Property(e => e.ContentCode).IsUnicode(false);

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.Idmember).HasColumnName("IDMember");

                entity.Property(e => e.IdsnippetPreviousVersion).HasColumnName("IDSnippetPreviousVersion");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdmemberNavigation)
                    .WithMany(p => p.CodeSnippets)
                    .HasForeignKey(d => d.Idmember)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CodeSnippets_Members");

                entity.HasOne(d => d.IdsnippetPreviousVersionNavigation)
                    .WithMany(p => p.InverseIdsnippetPreviousVersionNavigation)
                    .HasForeignKey(d => d.IdsnippetPreviousVersion)
                    .HasConstraintName("FK_CodeSnippets_CodeSnippets");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.Idmember)
                    .HasName("PK__Members__7EB75A638CC7824A");

                entity.Property(e => e.Idmember)
                    .ValueGeneratedNever()
                    .HasColumnName("IDMember");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Resume).IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.HasKey(e => e.Idmembership)
                    .HasName("PK__Membersh__4AB986329E139C20");

                entity.Property(e => e.Idmembership)
                    .ValueGeneratedNever()
                    .HasColumnName("IDMembership");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Idmember).HasColumnName("IDMember");

                entity.Property(e => e.IdmembershipType).HasColumnName("IDMembershipType");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdmemberNavigation)
                    .WithMany(p => p.Memberships)
                    .HasForeignKey(d => d.Idmember)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Memberships_Members");

                entity.HasOne(d => d.IdmembershipTypeNavigation)
                    .WithMany(p => p.Memberships)
                    .HasForeignKey(d => d.IdmembershipType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Memberships_MembershipTypes");
            });

            modelBuilder.Entity<MembershipType>(entity =>
            {
                entity.HasKey(e => e.IdmembershipType);

                entity.Property(e => e.IdmembershipType)
                    .ValueGeneratedNever()
                    .HasColumnName("IDMembershipType");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
