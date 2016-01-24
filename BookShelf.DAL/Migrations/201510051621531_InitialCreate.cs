namespace BookShelf.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author = c.String(),
                        Ganre = c.String(),
                        Size = c.Int(nullable: false),
                        FileType = c.String(),
                        Path = c.String(),
                        Category_CategoryId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ReadingLists",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RadingList = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReadingLists", "UserId", "dbo.Users");
            DropForeignKey("dbo.Books", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Books", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.ReadingLists", new[] { "UserId" });
            DropIndex("dbo.Books", new[] { "User_UserId" });
            DropIndex("dbo.Books", new[] { "Category_CategoryId" });
            DropTable("dbo.ReadingLists");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
            DropTable("dbo.Books");
        }
    }
}
