namespace testDMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DonorManagementDatabaseEntities : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "NewRole");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "NewRole", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
        }
    }
}
