namespace MobilebACKEND.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCasCadeOnDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pfc", "Owner_Id", "dbo.Food");
            AddForeignKey("dbo.Pfc", "Owner_Id", "dbo.Food", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pfc", "Owner_Id", "dbo.Food");
            AddForeignKey("dbo.Pfc", "Owner_Id", "dbo.Food", "Id");
        }
    }
}
