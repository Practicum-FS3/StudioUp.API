﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudioUp.Models;

#nullable disable

namespace StudioUp.Models.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240903103444_migrations")]
    partial class migrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudioUp.Models.AvailableTraining", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ParticipantsCount")
                        .HasColumnType("int");

                    b.Property<int>("TrainingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrainingId");

                    b.ToTable("T_AvailableTrainings");
                });

            modelBuilder.Entity("StudioUp.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsWatch")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("T_Contacts");
                });

            modelBuilder.Entity("StudioUp.Models.ContentSection", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ContentTypeID")
                        .HasColumnType("int");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Section1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Section2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Section3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Section4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ViewInHP")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("ContentTypeID");

                    b.ToTable("ContentSections");
                });

            modelBuilder.Entity("StudioUp.Models.ContentType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Link2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkHP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title4")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("ContentTypes");
                });

            modelBuilder.Entity("StudioUp.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("CustomerTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("HMOId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("PaymentOptionId")
                        .HasColumnType("int");

                    b.Property<int?>("SubscriptionTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Tz")
                        .IsRequired()
                        .HasColumnType("nvarchar(9)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerTypeId");

                    b.HasIndex("HMOId");

                    b.HasIndex("PaymentOptionId");

                    b.HasIndex("SubscriptionTypeId");

                    b.ToTable("T_Customers");
                });

            modelBuilder.Entity("StudioUp.Models.CustomerFixedTraining", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("TrainingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TrainingId");

                    b.ToTable("T_CustomerFixedTrainings");
                });

            modelBuilder.Entity("StudioUp.Models.CustomerHMOS", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int?>("FiledId")
                        .HasColumnType("int");

                    b.Property<string>("FreeFitId")
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("HMOID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("HMOID");

                    b.ToTable("T_CustomerHMOS");
                });

            modelBuilder.Entity("StudioUp.Models.CustomerSubscription", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SubscriptionTypeId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("SubscriptionTypeId");

                    b.ToTable("T_CustomerSubscription");
                });

            modelBuilder.Entity("StudioUp.Models.CustomerType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("T_CustomerTypes");
                });

            modelBuilder.Entity("StudioUp.Models.FileUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("StudioUp.Models.HMO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("ArrangementName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<double?>("MaximumAge")
                        .HasColumnType("float");

                    b.Property<double?>("MinimumAge")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TrainingDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("TrainingPrice")
                        .HasColumnType("float");

                    b.Property<int?>("TrainingsPerMonth")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("T_HMOs");
                });

            modelBuilder.Entity("StudioUp.Models.InternalHomeLinks", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsExternal")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("InternalHomeLinks");
                });

            modelBuilder.Entity("StudioUp.Models.LeumitCommimentTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("T_LeumitCommimentTypes");
                });

            modelBuilder.Entity("StudioUp.Models.LeumitCommitments", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<int?>("CommitmentTypeId")
                        .HasColumnType("int");

                    b.Property<string>("CommitmentTz")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("FileUploadId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateOnly>("Validity")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CommitmentTypeId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FileUploadId");

                    b.ToTable("T_LeumitCommitments");
                });

            modelBuilder.Entity("StudioUp.Models.LoginModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("StudioUp.Models.PaymentOption", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("T_PaymentOptions");
                });

            modelBuilder.Entity("StudioUp.Models.SubscriptionType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("NumberOfTrainingPerWeek")
                        .HasColumnType("int");

                    b.Property<float?>("PriceForTraining")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("TotalTraining")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("T_SubscriptionTypes");
                });

            modelBuilder.Entity("StudioUp.Models.Trainer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.ToTable("T_Trainers");
                });

            modelBuilder.Entity("StudioUp.Models.Training", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<int>("Hour")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("Minute")
                        .HasColumnType("int");

                    b.Property<int?>("ParticipantsCount")
                        .HasColumnType("int");

                    b.Property<int?>("TrainerID")
                        .HasColumnType("int");

                    b.Property<int?>("TrainingCustomerTypeId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TrainerID");

                    b.HasIndex("TrainingCustomerTypeId");

                    b.ToTable("T_Trainings");
                });

            modelBuilder.Entity("StudioUp.Models.TrainingCustomer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool>("Attended")
                        .HasColumnType("bit");

                    b.Property<int?>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int>("CustomerSubscriptionId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("TrainingID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("CustomerSubscriptionId");

                    b.HasIndex("TrainingID");

                    b.ToTable("T_TrainingsCustomers");
                });

            modelBuilder.Entity("StudioUp.Models.TrainingCustomerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerTypeID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("TrainingTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerTypeID");

                    b.HasIndex("TrainingTypeId");

                    b.ToTable("T_TrainingCustomerTypes");
                });

            modelBuilder.Entity("StudioUp.Models.TrainingType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("T_TrainigTypes");
                });

            modelBuilder.Entity("StudioUp.Models.AvailableTraining", b =>
                {
                    b.HasOne("StudioUp.Models.Training", "Training")
                        .WithMany()
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Training");
                });

            modelBuilder.Entity("StudioUp.Models.ContentSection", b =>
                {
                    b.HasOne("StudioUp.Models.ContentType", "ContentType")
                        .WithMany("ContentSections")
                        .HasForeignKey("ContentTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContentType");
                });

            modelBuilder.Entity("StudioUp.Models.Customer", b =>
                {
                    b.HasOne("StudioUp.Models.CustomerType", "CustomerType")
                        .WithMany()
                        .HasForeignKey("CustomerTypeId");

                    b.HasOne("StudioUp.Models.HMO", "HMO")
                        .WithMany()
                        .HasForeignKey("HMOId");

                    b.HasOne("StudioUp.Models.PaymentOption", "PaymentOption")
                        .WithMany()
                        .HasForeignKey("PaymentOptionId");

                    b.HasOne("StudioUp.Models.SubscriptionType", "SubscriptionType")
                        .WithMany()
                        .HasForeignKey("SubscriptionTypeId");

                    b.Navigation("CustomerType");

                    b.Navigation("HMO");

                    b.Navigation("PaymentOption");

                    b.Navigation("SubscriptionType");
                });

            modelBuilder.Entity("StudioUp.Models.CustomerFixedTraining", b =>
                {
                    b.HasOne("StudioUp.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("StudioUp.Models.Training", "Training")
                        .WithMany()
                        .HasForeignKey("TrainingId");

                    b.Navigation("Customer");

                    b.Navigation("Training");
                });

            modelBuilder.Entity("StudioUp.Models.CustomerHMOS", b =>
                {
                    b.HasOne("StudioUp.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID");

                    b.HasOne("StudioUp.Models.HMO", "HMOs")
                        .WithMany()
                        .HasForeignKey("HMOID");

                    b.Navigation("Customer");

                    b.Navigation("HMOs");
                });

            modelBuilder.Entity("StudioUp.Models.CustomerSubscription", b =>
                {
                    b.HasOne("StudioUp.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudioUp.Models.SubscriptionType", "SubscriptionType")
                        .WithMany()
                        .HasForeignKey("SubscriptionTypeId");

                    b.Navigation("Customer");

                    b.Navigation("SubscriptionType");
                });

            modelBuilder.Entity("StudioUp.Models.LeumitCommitments", b =>
                {
                    b.HasOne("StudioUp.Models.LeumitCommimentTypes", "LeumitCommimentTypes")
                        .WithMany()
                        .HasForeignKey("CommitmentTypeId");

                    b.HasOne("StudioUp.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("StudioUp.Models.FileUpload", "FileUpload")
                        .WithMany()
                        .HasForeignKey("FileUploadId");

                    b.Navigation("Customer");

                    b.Navigation("FileUpload");

                    b.Navigation("LeumitCommimentTypes");
                });

            modelBuilder.Entity("StudioUp.Models.Training", b =>
                {
                    b.HasOne("StudioUp.Models.Trainer", "Trainer")
                        .WithMany()
                        .HasForeignKey("TrainerID");

                    b.HasOne("StudioUp.Models.TrainingCustomerType", "TrainingCustomerType")
                        .WithMany()
                        .HasForeignKey("TrainingCustomerTypeId");

                    b.Navigation("Trainer");

                    b.Navigation("TrainingCustomerType");
                });

            modelBuilder.Entity("StudioUp.Models.TrainingCustomer", b =>
                {
                    b.HasOne("StudioUp.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID");

                    b.HasOne("StudioUp.Models.CustomerSubscription", "Subscription")
                        .WithMany()
                        .HasForeignKey("CustomerSubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudioUp.Models.AvailableTraining", "Training")
                        .WithMany()
                        .HasForeignKey("TrainingID");

                    b.Navigation("Customer");

                    b.Navigation("Subscription");

                    b.Navigation("Training");
                });

            modelBuilder.Entity("StudioUp.Models.TrainingCustomerType", b =>
                {
                    b.HasOne("StudioUp.Models.CustomerType", "CustomerType")
                        .WithMany()
                        .HasForeignKey("CustomerTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudioUp.Models.TrainingType", "TrainingType")
                        .WithMany()
                        .HasForeignKey("TrainingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerType");

                    b.Navigation("TrainingType");
                });

            modelBuilder.Entity("StudioUp.Models.ContentType", b =>
                {
                    b.Navigation("ContentSections");
                });
#pragma warning restore 612, 618
        }
    }
}
