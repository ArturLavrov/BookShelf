namespace BookShelf.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBoolFlags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Books", "IsFavorite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "IsFavorite");
            DropColumn("dbo.Books", "IsRead");
        }
    }
}
