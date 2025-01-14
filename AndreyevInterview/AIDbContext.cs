﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AndreyevInterview
{
    public class AIDbContext : DbContext
    {
        public AIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Invoice
            modelBuilder.Entity<Invoice>().Property(b => b.Description).IsRequired();

            // Line item
            modelBuilder.Entity<LineItem>().Property(b => b.InvoiceId).IsRequired();
            modelBuilder.Entity<LineItem>().Property(b => b.Quantity).IsRequired();
            modelBuilder.Entity<LineItem>().Property(b => b.Cost).IsRequired();
            modelBuilder.Entity<LineItem>().Property(b => b.isBillable).IsRequired();

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Contact)
                .WithMany(c => c.Invoices);
                //.HasForeignKey(i => i.ContactId);

            modelBuilder.Entity<LineItem>()
                .HasOne(i => i.Invoice)
                .WithMany(c => c.LineItems);
                //.HasForeignKey(i => i.InvoiceId);
        }
    }
    public class Contact
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }
        public string EmerName { get; set; }
        public string EmerPhoneNum { get; set; }
        // public string PaymentInfo { get; set; }
        // public string IDNum { get; set; }
        public DateTime DOB { get; set; }
        public string LegalRep { get; set; }
        public string AdditionalNote { get; set; }


        public virtual ICollection<Invoice> Invoices { get; set; }

    }
    public class Invoice
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ContactId { get; set; }



        public virtual Contact Contact { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }

    }

    public class LineItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Description { get; set; }

        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public bool isBillable { get; set; }


        public virtual Invoice Invoice { get; set; }
    }
    
}