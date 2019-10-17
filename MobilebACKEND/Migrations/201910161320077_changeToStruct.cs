namespace MobilebACKEND.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeToStruct : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Food", newName: "Foods");
            DropColumn("dbo.Foods", "Proteins");
            DropColumn("dbo.Foods", "Fats");
            DropColumn("dbo.Foods", "Carbohydrates");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "Carbohydrates", c => c.Int(nullable: false));
            AddColumn("dbo.Foods", "Fats", c => c.Int(nullable: false));
            AddColumn("dbo.Foods", "Proteins", c => c.Int(nullable: false));
            RenameTable(name: "dbo.Foods", newName: "Food");
        }
    }
}
