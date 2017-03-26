namespace testDMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DonorManagementDatabaseEntities2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Firstname", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "NewRole", c => c.String());
        }
        
        public override void Down()
        {
        }
    }
}
