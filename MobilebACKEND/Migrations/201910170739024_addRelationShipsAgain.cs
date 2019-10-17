namespace MobilebACKEND.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRelationShipsAgain : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Foods", newName: "Food");
            DropIndex("dbo.Food", new[] { "CreatedAt" });
            DropPrimaryKey("dbo.Food");
            AlterColumn("dbo.Food", "Id", c => c.String(nullable: false, maxLength: 36));
            AddPrimaryKey("dbo.Food", "Id");
            CreateIndex("dbo.Food", "Id");
            CreateIndex("dbo.Food", "CreatedAt");
            DropColumn("dbo.Food", "Description_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Food", "Description_Id", c => c.String());
            DropIndex("dbo.Food", new[] { "CreatedAt" });
            DropIndex("dbo.Food", new[] { "Id" });
            DropPrimaryKey("dbo.Food");
            AlterColumn("dbo.Food", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Food", "Id");
            CreateIndex("dbo.Food", "CreatedAt", clustered: true);
            RenameTable(name: "dbo.Food", newName: "Foods");
        }
    }
}
