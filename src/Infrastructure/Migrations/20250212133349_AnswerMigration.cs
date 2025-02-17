using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oig_assessment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AnswerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    QuestionAnswer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AnsweredBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                       name: "FK_Answers_Surveys_SurveyId",
                       column: x => x.SurveyId,
                       principalTable: "Surveys",
                       principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SurveyId",
                table: "Surveys",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Answers");
        }
    }
}
