﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20240927112817_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.ConfirmRegistrationCodes", b =>
                {
                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("expire_at")
                        .HasColumnType("timestamp");

                    b.HasKey("email");

                    b.ToTable("confirm_registration_codes");
                });

            modelBuilder.Entity("Entities.Models.CustomMaps", b =>
                {
                    b.Property<string>("map_id")
                        .HasColumnType("text");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("download_url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("player_id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("map_id");

                    b.HasIndex("player_id");

                    b.ToTable("custom_maps");
                });

            modelBuilder.Entity("Entities.Models.Downloads", b =>
                {
                    b.Property<string>("player_id")
                        .HasColumnType("text");

                    b.Property<string>("map_id")
                        .HasColumnType("text");

                    b.HasKey("player_id", "map_id");

                    b.HasIndex("map_id");

                    b.ToTable("downloads");
                });

            modelBuilder.Entity("Entities.Models.PlayerCredentials", b =>
                {
                    b.Property<string>("player_id")
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("refresh_token")
                        .HasColumnType("text");

                    b.HasKey("player_id");

                    b.ToTable("player_credentials");
                });

            modelBuilder.Entity("Entities.Models.PlayerProfiles", b =>
                {
                    b.Property<string>("player_id")
                        .HasColumnType("text");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp");

                    b.Property<int>("gender")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("player_id");

                    b.ToTable("player_profiles");
                });

            modelBuilder.Entity("Entities.Models.Subscriptions", b =>
                {
                    b.Property<string>("player_id")
                        .HasColumnType("text");

                    b.Property<DateTime>("expires_at")
                        .HasColumnType("timestamp");

                    b.HasKey("player_id");

                    b.ToTable("subscriptions");
                });

            modelBuilder.Entity("Entities.Models.CustomMaps", b =>
                {
                    b.HasOne("Entities.Models.PlayerProfiles", "PlayerProfile")
                        .WithMany()
                        .HasForeignKey("player_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerProfile");
                });

            modelBuilder.Entity("Entities.Models.Downloads", b =>
                {
                    b.HasOne("Entities.Models.CustomMaps", "CustomMap")
                        .WithMany()
                        .HasForeignKey("map_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.PlayerProfiles", "PlayerProfile")
                        .WithMany()
                        .HasForeignKey("player_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomMap");

                    b.Navigation("PlayerProfile");
                });

            modelBuilder.Entity("Entities.Models.PlayerProfiles", b =>
                {
                    b.HasOne("Entities.Models.PlayerCredentials", "PlayerCredential")
                        .WithMany()
                        .HasForeignKey("player_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerCredential");
                });

            modelBuilder.Entity("Entities.Models.Subscriptions", b =>
                {
                    b.HasOne("Entities.Models.PlayerProfiles", "PlayerProfile")
                        .WithMany()
                        .HasForeignKey("player_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerProfile");
                });
#pragma warning restore 612, 618
        }
    }
}
