namespace MobilebACKEND.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeInToLong : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Food", "Kkal", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Food", "Kkal", c => c.Int(nullable: false));
        }
    }
}
