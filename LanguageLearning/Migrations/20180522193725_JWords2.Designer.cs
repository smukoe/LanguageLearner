﻿// <auto-generated />
using LanguageLearning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LanguageLearning.Migrations
{
    [DbContext(typeof(WordContext))]
    [Migration("20180522193725_JWords2")]
    partial class JWords2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LanguageLearning.Models.JVerb", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Definition");

                    b.Property<string>("Kana");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("TypeOfVerb");

                    b.HasKey("ID");

                    b.ToTable("JVerb");
                });

            modelBuilder.Entity("LanguageLearning.Models.JWord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Definition");

                    b.Property<string>("Kana");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("PartsOfSpeech");

                    b.HasKey("ID");

                    b.ToTable("JWord");
                });

            modelBuilder.Entity("LanguageLearning.Models.KWord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Definition");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("PartsOfSpeech");

                    b.HasKey("ID");

                    b.ToTable("KWord");
                });
#pragma warning restore 612, 618
        }
    }
}
