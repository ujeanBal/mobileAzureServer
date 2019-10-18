namespace MobilebACKEND.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addByteArrField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Food", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Food", "Image");
        }
    }
}
