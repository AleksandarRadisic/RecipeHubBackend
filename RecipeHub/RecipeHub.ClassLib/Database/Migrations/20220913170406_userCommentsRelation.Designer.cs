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
    [Migration("20220913170406_userCommentsRelation")]
    partial class userCommentsRelation
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

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Picture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ArticleId")
                        .HasColumnType("uuid");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Picture");
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

            modelBuilder.Entity("RecipeHub.ClassLib.Model.RecipeIngredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeIngredient");
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

                    b.Property<bool>("Banned")
                        .HasColumnType("boolean");

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
                    b.HasOne("RecipeHub.ClassLib.Model.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId");

                    b.HasOne("RecipeHub.ClassLib.Model.Recipe", "Recipe")
                        .WithMany("Comments")
                        .HasForeignKey("RecipeId");

                    b.HasOne("RecipeHub.ClassLib.Model.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("RecipeHub.ClassLib.Model.Report", "Report", b1 =>
                        {
                            b1.Property<Guid>("CommentId")
                                .HasColumnType("uuid");

                            b1.Property<bool>("AdminConfirmed")
                                .HasColumnType("boolean");

                            b1.Property<bool>("BlockApproved")
                                .HasColumnType("boolean");

                            b1.HasKey("CommentId");

                            b1.ToTable("Comments");

                            b1.WithOwner()
                                .HasForeignKey("CommentId");
                        });

                    b.Navigation("Article");

                    b.Navigation("Recipe");

                    b.Navigation("Report");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Picture", b =>
                {
                    b.HasOne("RecipeHub.ClassLib.Model.Article", null)
                        .WithMany("Pictures")
                        .HasForeignKey("ArticleId");

                    b.HasOne("RecipeHub.ClassLib.Model.Recipe", null)
                        .WithMany("Pictures")
                        .HasForeignKey("RecipeId");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Recipe", b =>
                {
                    b.HasOne("RecipeHub.ClassLib.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.RecipeIngredient", b =>
                {
                    b.HasOne("RecipeHub.ClassLib.Model.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeHub.ClassLib.Model.Recipe", null)
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId");

                    b.Navigation("Ingredient");
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

                    b.Navigation("Pictures");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.Recipe", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Pictures");

                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("RecipeHub.ClassLib.Model.User", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
