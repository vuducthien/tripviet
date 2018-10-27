﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TripViet.Commons;
using TripViet.Data;

namespace TripViet.Migrations.TripViet
{
    [DbContext(typeof(TripVietContext))]
    partial class TripVietContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TripViet.Domains.Blog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlogType");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<Guid>("CreatedById");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<Guid>("UpdatedById");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("TripViet.Domains.Place", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApiId");

                    b.Property<Guid>("BlogId");

                    b.Property<Guid>("CreatedById");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("HtmlAddress");

                    b.Property<string>("Name");

                    b.Property<string>("NonHtmlAddress");

                    b.Property<string>("Reference");

                    b.Property<Guid>("UpdatedById");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("TripViet.Domains.Place", b =>
                {
                    b.HasOne("TripViet.Domains.Blog")
                        .WithMany("Places")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
