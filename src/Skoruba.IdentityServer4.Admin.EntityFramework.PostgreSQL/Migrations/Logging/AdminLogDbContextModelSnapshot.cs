﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.DbContexts;

namespace Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL.Migrations.Logging
{
    [DbContext(typeof(AdminLogDbContext))]
    partial class AdminLogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:citext", ",,")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Skoruba.IdentityServer4.Admin.EntityFramework.Entities.Groups.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("citext");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Skoruba.IdentityServer4.Admin.EntityFramework.Entities.Groups.GroupChildGroup", b =>
                {
                    b.Property<Guid>("ParentGroupId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChildGroupId")
                        .HasColumnType("uuid");

                    b.HasKey("ParentGroupId", "ChildGroupId");

                    b.HasIndex("ChildGroupId");

                    b.ToTable("GroupChildGroups");
                });

            modelBuilder.Entity("Skoruba.IdentityServer4.Admin.EntityFramework.Entities.Groups.GroupUser", b =>
                {
                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("GroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupUsers");
                });

            modelBuilder.Entity("Skoruba.IdentityServer4.Admin.EntityFramework.Entities.Log", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Exception")
                        .HasColumnType("text");

                    b.Property<string>("Level")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("LogEvent")
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("MessageTemplate")
                        .HasColumnType("text");

                    b.Property<string>("Properties")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("Skoruba.IdentityServer4.Admin.EntityFramework.Entities.UserEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EventName")
                        .HasColumnType("text");

                    b.Property<string>("EventValues")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserEvents");
                });

            modelBuilder.Entity("Skoruba.IdentityServer4.Admin.EntityFramework.Entities.Groups.GroupChildGroup", b =>
                {
                    b.HasOne("Skoruba.IdentityServer4.Admin.EntityFramework.Entities.Groups.Group", "ChildGroup")
                        .WithMany("ParentGroups")
                        .HasForeignKey("ChildGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skoruba.IdentityServer4.Admin.EntityFramework.Entities.Groups.Group", "ParentGroup")
                        .WithMany("ChildGroups")
                        .HasForeignKey("ParentGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Skoruba.IdentityServer4.Admin.EntityFramework.Entities.Groups.GroupUser", b =>
                {
                    b.HasOne("Skoruba.IdentityServer4.Admin.EntityFramework.Entities.Groups.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
