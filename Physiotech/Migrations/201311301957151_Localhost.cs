namespace Physiotech.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Localhost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patient", "NextofKinMobile", c => c.String());
        }
        
       
        public override void Down()
        {
            DropColumn("dbo.Patient", "NextofKinMobile");
        }
    }
}
