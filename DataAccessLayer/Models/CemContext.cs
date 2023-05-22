using System;
using System.Collections.Generic;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

public partial class CemContext : DbContext
{
    

    public CemContext(DbContextOptions<CemContext> options)
        : base(options)
    {
        
    }


    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<ContactAddress> ContactAddresses { get; set; }

    public virtual DbSet<ContactInfo> ContactInfos { get; set; }

    public virtual DbSet<CounterParty> CounterParties { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventApproval> EventApprovals { get; set; }

    public virtual DbSet<EventAttachment> EventAttachments { get; set; }

    public virtual DbSet<EventInvitee> EventInvitees { get; set; }

    public virtual DbSet<Host> Hosts { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TimeZone> TimeZones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-90Q2I2G;Database=CEM;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contact");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Prefix).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_Employee");
            entity.HasOne(d => d.CounterParty).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.CounterPartyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_CounterParty");

        });

        

        modelBuilder.Entity<ContactAddress>(entity =>
        {
            entity.ToTable("ContactAddress");

            entity.Property(e => e.AddressType).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasColumnType("numeric(18, 0)");

            entity.HasOne(d => d.Contact).WithMany(p => p.ContactAddresses)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContactAddress_Contact");

            entity.HasOne(d => d.Country).WithMany(p => p.ContactAddresses)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContactAddress_Country");

            entity.HasOne(d => d.State).WithMany(p => p.ContactAddresses)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContactAddress_State");
        });

        modelBuilder.Entity<ContactInfo>(entity =>
        {
            entity.ToTable("ContactInfo");

            entity.Property(e => e.ContactContent).HasMaxLength(50);
            entity.Property(e => e.ContactType).HasMaxLength(50);

            entity.HasOne(d => d.Contact).WithMany(p => p.ContactInfos)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContactInfo_Contact");
        });

        modelBuilder.Entity<CounterParty>(entity =>
        {
            entity.ToTable("CounterParty");

            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.IsApproved).HasMaxLength(50);
            entity.Property(e => e.ApprovedBy).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.CounterParties)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CounterParty_Employee");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.Name).HasMaxLength(50);
        });
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);

        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event");

            entity.Property(e => e.Agenda).HasMaxLength(50);
            entity.Property(e => e.CostPerAttendies).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.EstimateCost).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PostalCode).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.Events)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_Country");
        });

        modelBuilder.Entity<EventApproval>(entity =>
        {
            entity.ToTable("EventApproval");

            entity.Property(e => e.ChiefStatus).HasMaxLength(50);
            entity.Property(e => e.CommentByChief).HasMaxLength(50);
            entity.Property(e => e.CommentByCompliance).HasMaxLength(50);
            entity.Property(e => e.ComplianceTeamStatus).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.ComplianceEmail).HasMaxLength(50);
            entity.Property(e => e.ChiefEmail).HasMaxLength(50);

            entity.HasOne(d => d.Event).WithMany(p => p.EventApprovals)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventApproval_Event");
        });

        modelBuilder.Entity<EventAttachment>(entity =>
        {
            entity.ToTable("EventAttachment");

            entity.Property(e => e.AttachmentName).HasMaxLength(50);
            entity.Property(e => e.AttachmentPath).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Event).WithMany(p => p.EventAttachments)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventAttachment_Event");
        });

        modelBuilder.Entity<EventInvitee>(entity =>
        {
            entity.ToTable("EventInvitee");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StatusBy).HasMaxLength(50);

            entity.HasOne(d => d.Contact).WithMany(p => p.EventInvitees)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventInvitee_Contact");

            entity.HasOne(d => d.Event).WithMany(p => p.EventInvitees)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventInvitee_Event");
        });

        modelBuilder.Entity<Host>(entity =>
        {
            entity.ToTable("Host");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.HostType).HasMaxLength(50);

            entity.HasOne(d => d.Event).WithMany(p => p.Hosts)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Host_Event");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("State");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_State_Country");
        });

        modelBuilder.Entity<TimeZone>(entity =>
        {
            entity.ToTable("TimeZone");

            entity.HasOne(d => d.Country).WithMany(p => p.TimeZones)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TimeZone_Country");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
