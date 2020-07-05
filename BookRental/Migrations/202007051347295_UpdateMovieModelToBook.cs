namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovieModelToBook : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Movies", newName: "Books");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Books", newName: "Movies");
        }
    }
}
