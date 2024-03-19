﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PostingAPI.Data;

#nullable disable

namespace PostingAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PostingAPI.Models.Dto.PostingDetails", b =>
                {
                    b.Property<int>("PostingDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostingDetailsId"));

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LikeStatus")
                        .HasColumnType("int");

                    b.Property<int>("PostingId")
                        .HasColumnType("int");

                    b.Property<string>("ShareStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostingDetailsId");

                    b.HasIndex("PostingId");

                    b.ToTable("PostingDetails");
                });

            modelBuilder.Entity("PostingAPI.Models.Posting", b =>
                {
                    b.Property<int>("PostingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostingId"));

                    b.Property<int?>("DeletionFlag")
                        .HasColumnType("int");

                    b.Property<string>("PostContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PostingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostingId");

                    b.ToTable("Posting");
                });

            modelBuilder.Entity("PostingAPI.Models.Dto.PostingDetails", b =>
                {
                    b.HasOne("PostingAPI.Models.Posting", "Posting")
                        .WithMany("PostDetails")
                        .HasForeignKey("PostingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Posting");
                });

            modelBuilder.Entity("PostingAPI.Models.Posting", b =>
                {
                    b.Navigation("PostDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
