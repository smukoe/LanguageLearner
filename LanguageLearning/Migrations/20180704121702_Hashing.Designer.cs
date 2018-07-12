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
    [Migration("20180704121702_Hashing")]
    partial class Hashing
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LanguageLearning.Models.JapaneseAdjective", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Definition");

                    b.Property<string>("Kana");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("TypeOfAdjective");

                    b.HasKey("ID");

                    b.ToTable("JapaneseAdjective");
                });

            modelBuilder.Entity("LanguageLearning.Models.JapaneseVerb", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Definition");

                    b.Property<string>("Kana");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("TypeOfVerb");

                    b.HasKey("ID");

                    b.ToTable("JapaneseVerb");
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

                    b.ToTable("KoreanWord");
                });

            modelBuilder.Entity("LanguageLearning.Models.UserData", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Salt");

                    b.Property<string>("UserName");

                    b.HasKey("ID");

                    b.ToTable("UserData");
                });
#pragma warning restore 612, 618
        }
    }
}
