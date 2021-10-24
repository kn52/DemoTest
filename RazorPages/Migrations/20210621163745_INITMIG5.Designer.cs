﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RazorPages.DBContext;

namespace RazorPages.Migrations
{
    [DbContext(typeof(RazorDBContext))]
    [Migration("20210621163745_INITMIG5")]
    partial class INITMIG5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("RazorPages.Entity.CustomerMngt.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CustomerId");

                    b.Property<string>("CustomerContact")
                        .HasColumnType("longtext")
                        .HasColumnName("CustomerContact");

                    b.Property<string>("CustomerEmail")
                        .HasColumnType("longtext")
                        .HasColumnName("CustomerEmail");

                    b.Property<string>("CustomerName")
                        .HasColumnType("longtext")
                        .HasColumnName("CustomerName");

                    b.Property<string>("CustomerPhone")
                        .HasColumnType("longtext")
                        .HasColumnName("CustomerPhone");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("RazorPages.Entity.CustomerMngt.PDescription", b =>
                {
                    b.Property<int>("PDescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PDescriptionId");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("Description");

                    b.Property<string>("PCode")
                        .HasColumnType("longtext")
                        .HasColumnName("PCode");

                    b.HasKey("PDescriptionId");

                    b.ToTable("PDescriptions");
                });

            modelBuilder.Entity("RazorPages.Entity.CustomerMngt.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProjectId");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerId");

                    b.Property<int>("PD")
                        .HasColumnType("int");

                    b.Property<string>("ProjectName")
                        .HasColumnType("longtext")
                        .HasColumnName("ProjectName");

                    b.HasKey("ProjectId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("RazorPages.Entity.CustomerMngt.ProjectSkill", b =>
                {
                    b.Property<int>("ProjectSkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProjectSkillId");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("ProjectId");

                    b.Property<int>("SD")
                        .HasColumnType("int");

                    b.HasKey("ProjectSkillId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Projectskills");
                });

            modelBuilder.Entity("RazorPages.Entity.CustomerMngt.SDescription", b =>
                {
                    b.Property<int>("SDescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SDescriptionId");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("Description");

                    b.Property<string>("SCode")
                        .HasColumnType("longtext")
                        .HasColumnName("SCode");

                    b.HasKey("SDescriptionId");

                    b.ToTable("SDescriptions");
                });

            modelBuilder.Entity("RazorPages.Entity.LoginCredentials", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("UserId");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)")
                        .HasColumnName("Password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("varchar(7)")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.ToTable("UserCred");
                });

            modelBuilder.Entity("RazorPages.Entity.Product.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("RazorPages.Entity.Product.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("RazorPages.Entity.UserDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.Property<string>("Address")
                        .HasColumnType("longtext")
                        .HasColumnName("Address");

                    b.Property<string>("City")
                        .HasColumnType("longtext")
                        .HasColumnName("City");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("UserDetail");
                });

            modelBuilder.Entity("RazorPages.Entity.CustomerMngt.Project", b =>
                {
                    b.HasOne("RazorPages.Entity.CustomerMngt.Customer", "Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("RazorPages.Entity.CustomerMngt.ProjectSkill", b =>
                {
                    b.HasOne("RazorPages.Entity.CustomerMngt.Project", "Project")
                        .WithMany("ProjectSkills")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("RazorPages.Entity.Product.Product", b =>
                {
                    b.HasOne("RazorPages.Entity.Product.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("RazorPages.Entity.CustomerMngt.Customer", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("RazorPages.Entity.CustomerMngt.Project", b =>
                {
                    b.Navigation("ProjectSkills");
                });

            modelBuilder.Entity("RazorPages.Entity.Product.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
