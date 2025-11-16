using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InnerHealth.Api.Migrations
{
    /// <summary>
    /// Migration inicial responsável por criar todas as tabelas do esquema da API InnerHealth.
    /// Esta migração foi criada manualmente pois o utilitário dotnet-ef não estava disponível no ambiente
    /// de build. Ela cria as tabelas, chaves primárias, índices e relacionamentos de chave estrangeira
    /// conforme definidos no <see cref="Data.ApplicationDbContext"/>.
    /// </summary>
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Cria a tabela de perfis de usuário
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    SleepQuality = table.Column<int>(type: "INTEGER", nullable: false),
                    SleepHours = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            // Cria tabela de ingestões de água
            migrationBuilder.CreateTable(
                name: "WaterIntakes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    AmountMl = table.Column<int>(type: "INTEGER", nullable: false),
                    UserProfileId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterIntakes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WaterIntakes_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Cria tabela de sessões de exposição ao sol
            migrationBuilder.CreateTable(
                name: "SunlightSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Minutes = table.Column<int>(type: "INTEGER", nullable: false),
                    UserProfileId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SunlightSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SunlightSessions_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Cria tabela de sessões de meditação
            migrationBuilder.CreateTable(
                name: "MeditationSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Minutes = table.Column<int>(type: "INTEGER", nullable: false),
                    UserProfileId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeditationSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeditationSessions_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Cria tabela de registros de sono
            migrationBuilder.CreateTable(
                name: "SleepRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Hours = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Quality = table.Column<int>(type: "INTEGER", nullable: false),
                    UserProfileId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SleepRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SleepRecords_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Cria tabela de atividades físicas
            migrationBuilder.CreateTable(
                name: "PhysicalActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Modality = table.Column<string>(type: "TEXT", nullable: true),
                    DurationMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    UserProfileId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalActivities_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Cria tabela de tarefas diárias
            migrationBuilder.CreateTable(
                name: "TaskItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    IsComplete = table.Column<bool>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: true),
                    UserProfileId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskItems_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Cria índices de chave estrangeira para melhorar consultas
            migrationBuilder.CreateIndex(
                name: "IX_WaterIntakes_UserProfileId",
                table: "WaterIntakes",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SunlightSessions_UserProfileId",
                table: "SunlightSessions",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MeditationSessions_UserProfileId",
                table: "MeditationSessions",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SleepRecords_UserProfileId",
                table: "SleepRecords",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalActivities_UserProfileId",
                table: "PhysicalActivities",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_UserProfileId",
                table: "TaskItems",
                column: "UserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Na ordem inversa para respeitar dependências
            migrationBuilder.DropTable(name: "MeditationSessions");
            migrationBuilder.DropTable(name: "PhysicalActivities");
            migrationBuilder.DropTable(name: "SleepRecords");
            migrationBuilder.DropTable(name: "SunlightSessions");
            migrationBuilder.DropTable(name: "TaskItems");
            migrationBuilder.DropTable(name: "WaterIntakes");
            migrationBuilder.DropTable(name: "UserProfiles");
        }
    }
}