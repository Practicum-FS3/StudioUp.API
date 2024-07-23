﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           
        }

        
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<PaymentOption> PaymentOptions { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<TrainingType> TrainingTypes { get; set; }
        public DbSet<HMO> HMOs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<AvailableTraining> AvailableTraining { get; set; }
        public DbSet<TrainingCustomer> TrainingCustomers { get; set; }
        public DbSet<ContentType> ContentTypes { get; set; }
        public DbSet<TrainingCustomerType> TrainingCustomersTypes { get;set; }
        public DbSet<ContentSection> ContentSections { get; set; }

    }
}

