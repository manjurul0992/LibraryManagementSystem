#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.BackendApi.Migrations
{
    public partial class storeprocedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE DEFINER=`root`@`localhost` PROCEDURE `getbookdetails`()
                                    BEGIN
                                        SELECT BookId,
                                               Title,
                                               AuthorName,
                                               PublishedDate,
                                               ISBN,
                                               CASE WHEN AvailableCopies > 0 THEN 'Y' ELSE 'N' END AS AvailableCopies
                                        FROM books b
                                        INNER JOIN authors a
                                        WHERE b.AuthorID = a.AuthorId;
                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS `getbookdetails`;");
        }
    }
}
