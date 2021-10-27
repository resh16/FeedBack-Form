using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataAccessLayer.Model;

#nullable disable

namespace DataAccessLayer.Data
{
    public partial class FeedBackDBContext : DbContext
    {
        public FeedBackDBContext()
        {
        }

        public FeedBackDBContext(DbContextOptions<FeedBackDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<FeedbackDetail> FeedbackDetails { get; set; }
        public virtual DbSet<FeedbackDetailType> FeedbackDetailTypes { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<User> Users { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.Comment).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.MobileNumber).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedback__Sessio__2C3393D0");
            });

            modelBuilder.Entity<FeedbackDetail>(entity =>
            {
                entity.HasOne(d => d.Fdt)
                    .WithMany()
                    .HasForeignKey(d => d.FdtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FeedbackD__FdtId__2F10007B");

                entity.HasOne(d => d.Feedback)
                    .WithMany()
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FeedbackD__Feedb__2E1BDC42");
            });

            modelBuilder.Entity<FeedbackDetailType>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.Property(e => e.SessionName).IsUnicode(false);

                entity.HasOne(d => d.Conductor)
                    .WithMany(p => p.SessionConductors)
                    .HasForeignKey(d => d.ConductorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__Conduct__267ABA7A");

                entity.HasOne(d => d.Speaker)
                    .WithMany(p => p.SessionSpeakers)
                    .HasForeignKey(d => d.SpeakerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__Speaker__276EDEB3");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
