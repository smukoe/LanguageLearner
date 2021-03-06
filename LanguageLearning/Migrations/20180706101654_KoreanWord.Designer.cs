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
    [Migration("20180706101654_KoreanWord")]
    partial class KoreanWord
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("LanguageLearning.Models.KoreanWord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Definition");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("PartsOfSpeech");

                    b.Property<string>("SoundChange");

                    b.HasKey("ID");

                    b.ToTable("KoreanWord");
                });

            modelBuilder.Entity("LanguageLearning.Models.UserData", b =>
                {
                    b.Property<int>("ID")
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
