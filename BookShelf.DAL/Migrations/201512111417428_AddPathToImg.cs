namespace BookShelf.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPathToImg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "PathToBookImg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "PathToBookImg");
        }
    }
}
