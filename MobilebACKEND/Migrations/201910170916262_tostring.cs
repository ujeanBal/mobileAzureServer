namespace MobilebACKEND.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Foods", "Weight", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Foods", "Weight", c => c.Int(nullable: false));
        }
    }
}
