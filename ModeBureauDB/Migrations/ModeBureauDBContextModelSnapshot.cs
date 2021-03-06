﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModeBureauDB.Models;

namespace ModeBureauDB.Migrations
{
    [DbContext(typeof(ModeBureauDBContext))]
    partial class ModeBureauDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ModeBureauDB.Models.IncomingTask", b =>
                {
                    b.Property<int>("IncomingTaskId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments");

                    b.Property<string>("Costumer");

                    b.Property<string>("Location");

                    b.Property<int>("NumberOfDays");

                    b.Property<int>("NumberOfModels");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("IncomingTaskId");

                    b.ToTable("IncomingTask");
                });

            modelBuilder.Entity("ModeBureauDB.Models.Model", b =>
                {
                    b.Property<int>("modelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Comment");

                    b.Property<string>("HairColour");

                    b.Property<int>("Height");

                    b.Property<string>("Name");

                    b.Property<int>("PhoneNumber");

                    b.Property<int>("Weight");

                    b.HasKey("modelId");

                    b.ToTable("Model");
                });

            modelBuilder.Entity("ModeBureauDB.Models.ModelIncomingTask", b =>
                {
                    b.Property<int>("IncomingTaskId");

                    b.Property<int>("ModelId");

                    b.HasKey("IncomingTaskId", "ModelId");

                    b.HasIndex("ModelId");

                    b.ToTable("ModelIncomingTask");
                });

            modelBuilder.Entity("ModeBureauDB.Models.ModelIncomingTask", b =>
                {
                    b.HasOne("ModeBureauDB.Models.IncomingTask", "IncomingTask")
                        .WithMany("ModelIncomingTasks")
                        .HasForeignKey("IncomingTaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ModeBureauDB.Models.Model", "Model")
                        .WithMany("ModelIncomingTasks")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
