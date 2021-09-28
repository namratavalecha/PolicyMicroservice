﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PolicyMicroservice.Data;

namespace PolicyMicroservice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210928090434_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PolicyMicroservice.Models.ConsumerPolicy", b =>
                {
                    b.Property<string>("ConsumerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AcceptanceStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AcceptedQuotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolicyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolicyStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConsumerId");

                    b.ToTable("consumerPolicies");
                });

            modelBuilder.Entity("PolicyMicroservice.Models.PolicyMaster", b =>
                {
                    b.Property<string>("PolicyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AssuredSum")
                        .HasColumnType("int");

                    b.Property<string>("BaseLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BusinessValue")
                        .HasColumnType("int");

                    b.Property<string>("ConsumerType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyValue")
                        .HasColumnType("int");

                    b.Property<int>("Tenure")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PolicyId");

                    b.ToTable("policyMasters");
                });
#pragma warning restore 612, 618
        }
    }
}
