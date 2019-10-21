namespace MobilebACKEND.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAllTypesToLong : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Food", "Weight", c => c.Long(nullable: false));
            AlterColumn("dbo.Pfc", "Proteins", c => c.Long(nullable: false));
            AlterColumn("dbo.Pfc", "Fats", c => c.Long(nullable: false));
            AlterColumn("dbo.Pfc", "Carbohydrates", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pfc", "Carbohydrates", c => c.Int(nullable: false));
            AlterColumn("dbo.Pfc", "Fats", c => c.Int(nullable: false));
            AlterColumn("dbo.Pfc", "Proteins", c => c.Int(nullable: false));
            AlterColumn("dbo.Food", "Weight", c => c.Int(nullable: false));
        }
    }
}
