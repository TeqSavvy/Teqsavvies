namespace Physiotech.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Verification1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clinic", "UniqueId", c => c.String());
            DropColumn("dbo.Clinic", "VerificationCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clinic", "VerificationCode", c => c.String());
            DropColumn("dbo.Clinic", "UniqueId");
        }
    }
}
