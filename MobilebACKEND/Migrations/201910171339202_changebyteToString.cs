namespace MobilebACKEND.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changebyteToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Food", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Food", "Image", c => c.Binary());
        }
    }
}
