using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Data_Access.Migrations
{
    public partial class sp_stop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE covertytre
                        @name varchar(50)
                         AS
                         insert Covertypes values (@name)");

            migrationBuilder.Sql(@"CREATE PROCEDURE updateCoverType
                        @id int,
                    @name varchar(50)

                         AS
                       update Covertypes set name=@name where id=@id");
            migrationBuilder.Sql(@"CREATE PROCEDURE  getCoverType
                       AS
                       select * from Covertypes");
            migrationBuilder.Sql(@"CREATE PROCEDURE  getCoverTypes
  

                      @id int
                       AS
                       select * from Covertypes where id=@id");

            migrationBuilder.Sql(@"CREATE PROCEDURE  DeleteCoverTypes
                      @id int
                       AS
                       delete  from Covertypes where id=@id");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
