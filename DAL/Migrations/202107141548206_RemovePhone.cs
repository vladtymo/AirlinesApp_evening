namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePhone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.Clients", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Phone", c => c.String());
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
