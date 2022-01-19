using Microsoft.EntityFrameworkCore.Migrations;

namespace AlexBankExam.Persistence.Migrations
{
    public partial class spCreateTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[CreateTransaction]
                    @Id uniqueidentifier, @Amount decimal (18,2), @Description varchar(250), 
                    @FromAccount nvarchar(20), @ToAccount nvarchar(20), @TransactionDate datetime2, 
                    @OwnerId uniqueidentifier
                AS
                BEGIN
                    INSERT INTO Transactions(Id, [FromAccount], [ToAccount], [Description], [Amount], [TransactionDate], [OwnerId]) 
                    VALUES (@Id, @FromAccount, @ToAccount, @Description, @Amount, @TransactionDate, @OwnerId)
                   
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
