﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RecipeHub.ClassLib.Database.EfStructures;

#nullable disable

namespace RecipeHub.ClassLib.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220912175345_RecipeIngredientCascadeRevert")]
    partial class RecipeIngredientCascadeRevert
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ArticleId")
                        .HasColumnType("uuid");

                    b.Property<long>("Rating")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CaloriesPerUnit")
                        .HasColumnType("integer");

                    b.Property<int>("MeasureUnit")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Article", b =>
                {
                    b.HasOne("RecipeHub.ClassLib.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Comment", b =>
                {
                    b.HasOne("RecipeHub.ClassLib.Model.Article", null)
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId");

                    b.HasOne("RecipeHub.ClassLib.Model.Recipe", null)
                        .WithMany("Comments")
                        .HasForeignKey("RecipeId");

                    b.HasOne("RecipeHub.ClassLib.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Recipe", b =>
                {
                    b.HasOne("RecipeHub.ClassLib.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("RecipeHub.ClassLib.Model.RecipeIngredient", "RecipeIngredients", b1 =>
                        {
                            b1.Property<Guid>("RecipeId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<Guid>("IngredientId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Quantity")
                                .HasColumnType("integer");

                            b1.HasKey("RecipeId", "Id");

                            b1.HasIndex("IngredientId");

                            b1.ToTable("RecipeIngredient");

                            b1.HasOne("RecipeHub.ClassLib.Model.Ingredient", "Ingredient")
                                .WithMany()
                                .HasForeignKey("IngredientId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("RecipeId");

                            b1.Navigation("Ingredient");
                        });

                    b.Navigation("RecipeIngredients");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.User", b =>
                {
                    b.HasOne("RecipeHub.ClassLib.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Article", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Recipe", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}