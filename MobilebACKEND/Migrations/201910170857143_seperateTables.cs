namespace MobilebACKEND.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seperateTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Food", newName: "Foods");
            CreateTable(
                "dbo.PFCs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Proteins = c.Int(nullable: false),
                        Fats = c.Int(nullable: false),
                        Carbohydrates = c.Int(nullable: false),
                        Owner_Id = c.String(nullable: false, maxLength: 36),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Foods", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            DropColumn("dbo.Foods", "Proteins");
            DropColumn("dbo.Foods", "Fats");
            DropColumn("dbo.Foods", "Carbohydrates");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "Carbohydrates", c => c.Int(nullable: false));
            AddColumn("dbo.Foods", "Fats", c => c.Int(nullable: false));
            AddColumn("dbo.Foods", "Proteins", c => c.Int(nullable: false));
            DropForeignKey("dbo.PFCs", "Owner_Id", "dbo.Foods");
            DropIndex("dbo.PFCs", new[] { "Owner_Id" });
            DropTable("dbo.PFCs");
            RenameTable(name: "dbo.Foods", newName: "Food");
        }
    }
}
