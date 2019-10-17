namespace MobilebACKEND.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Foods", "Weight", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Foods", "Weight", c => c.String());
        }
    }
}
