namespace MobilebACKEND.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Foods", newName: "Food");
            RenameTable(name: "dbo.PFCs", newName: "Pfc");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Pfc", newName: "PFCs");
            RenameTable(name: "dbo.Food", newName: "Foods");
        }
    }
}
