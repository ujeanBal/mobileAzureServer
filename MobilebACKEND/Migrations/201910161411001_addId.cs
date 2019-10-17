namespace MobilebACKEND.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Foods", "Description_Id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Foods", "Description_Id");
        }
    }
}
