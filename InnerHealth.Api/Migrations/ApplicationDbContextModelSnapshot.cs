using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Metadata;
using InnerHealth.Api.Data;
using InnerHealth.Api.Models;

namespace InnerHealth.Api.Migrations
{
    /// <summary>
    /// Snapshot do modelo EF Core correspondente à migração inicial. Ele descreve o estado das
    /// entidades do <see cref="ApplicationDbContext"/> após aplicar a migração <c>InitialCreate</c>.
    /// Sem essa snapshot, o comando de migrations do EF Core não consegue calcular diferenças
    /// entre a versão atual do modelo e o banco de dados existente.
    /// </summary>
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        /// <inheritdoc />
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0");

            // Configura tabela de perfis de usuário
            modelBuilder.Entity("InnerHealth.Api.Models.UserProfile", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<int>("Age")
                    .HasColumnType("INTEGER");

                b.Property<decimal>("Height")
                    .HasColumnType("decimal(10,2)");

                b.Property<decimal>("SleepHours")
                    .HasColumnType("decimal(5,2)");

                b.Property<int>("SleepQuality")
                    .HasColumnType("INTEGER");

                b.Property<decimal>("Weight")
                    .HasColumnType("decimal(10,2)");

                b.HasKey("Id");

                b.ToTable("UserProfiles");
            });

            // Tabela de ingestão de água
            modelBuilder.Entity("InnerHealth.Api.Models.WaterIntake", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<int>("AmountMl")
                    .HasColumnType("INTEGER");

                b.Property<DateOnly>("Date")
                    .HasColumnType("date");

                b.Property<int>("UserProfileId")
                    .HasColumnType("INTEGER");

                b.HasKey("Id");
                b.HasIndex("UserProfileId");
                b.ToTable("WaterIntakes");
            });

            // Tabela de sessões de sol
            modelBuilder.Entity("InnerHealth.Api.Models.SunlightSession", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<DateOnly>("Date")
                    .HasColumnType("date");

                b.Property<int>("Minutes")
                    .HasColumnType("INTEGER");

                b.Property<int>("UserProfileId")
                    .HasColumnType("INTEGER");

                b.HasKey("Id");
                b.HasIndex("UserProfileId");
                b.ToTable("SunlightSessions");
            });

            // Tabela de sessões de meditação
            modelBuilder.Entity("InnerHealth.Api.Models.MeditationSession", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<DateOnly>("Date")
                    .HasColumnType("date");

                b.Property<int>("Minutes")
                    .HasColumnType("INTEGER");

                b.Property<int>("UserProfileId")
                    .HasColumnType("INTEGER");

                b.HasKey("Id");
                b.HasIndex("UserProfileId");
                b.ToTable("MeditationSessions");
            });

            // Tabela de registros de sono
            modelBuilder.Entity("InnerHealth.Api.Models.SleepRecord", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<DateOnly>("Date")
                    .HasColumnType("date");

                b.Property<decimal>("Hours")
                    .HasColumnType("decimal(5,2)");

                b.Property<int>("Quality")
                    .HasColumnType("INTEGER");

                b.Property<int>("UserProfileId")
                    .HasColumnType("INTEGER");

                b.HasKey("Id");
                b.HasIndex("UserProfileId");
                b.ToTable("SleepRecords");
            });

            // Tabela de atividades físicas
            modelBuilder.Entity("InnerHealth.Api.Models.PhysicalActivity", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<DateOnly>("Date")
                    .HasColumnType("date");

                b.Property<int>("DurationMinutes")
                    .HasColumnType("INTEGER");

                b.Property<string>("Modality")
                    .HasColumnType("TEXT");

                b.Property<int>("UserProfileId")
                    .HasColumnType("INTEGER");

                b.HasKey("Id");
                b.HasIndex("UserProfileId");
                b.ToTable("PhysicalActivities");
            });

            // Tabela de tarefas
            modelBuilder.Entity("InnerHealth.Api.Models.TaskItem", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<DateOnly>("Date")
                    .HasColumnType("date");

                b.Property<string>("Description")
                    .HasColumnType("TEXT");

                b.Property<bool>("IsComplete")
                    .HasColumnType("INTEGER");

                b.Property<int?>("Priority")
                    .HasColumnType("INTEGER");

                b.Property<string>("Title")
                    .HasColumnType("TEXT");

                b.Property<int>("UserProfileId")
                    .HasColumnType("INTEGER");

                b.HasKey("Id");
                b.HasIndex("UserProfileId");
                b.ToTable("TaskItems");
            });

            // Notas: O snapshot não define explicitamente os relacionamentos; eles são inferidos pelo EF Core
        }
    }
}