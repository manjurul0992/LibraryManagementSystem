using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.BackendApi.Migrations
{
    /// <inheritdoc />
    public partial class SecoundstoreProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetMemberBorrowedBooks()
                BEGIN
                    SELECT
                        m.MemberId,
                        m.FirstName,
                        m.LastName,
                        COUNT(bb.BookId) AS TotalBooksBorrowed,
                        SUM(CASE WHEN bb.Status = 'Returned' THEN 1 ELSE 0 END) AS BooksReturned,
                        SUM(CASE WHEN bb.Status = 'Borrowed' THEN 1 ELSE 0 END) AS BooksBorrowed
                    FROM
                        members m
                    LEFT JOIN
                        BorrowedBooks bb ON m.MemberId = bb.MemberId
                    LEFT JOIN
                        Books b ON bb.BookId = b.BookId
                    GROUP BY
                        m.MemberId, m.FirstName, m.LastName;
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetMemberBorrowedBooks;");
        }
    }
}
