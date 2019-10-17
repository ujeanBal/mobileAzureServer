namespace MobilebACKEND.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Foods", "Proteins", c => c.Int(nullable: false));
            AddColumn("dbo.Foods", "Fats", c => c.Int(nullable: false));
            AddColumn("dbo.Foods", "Carbohydrates", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Foods", "Carbohydrates");
            DropColumn("dbo.Foods", "Fats");
            DropColumn("dbo.Foods", "Proteins");
        }
    }
}
