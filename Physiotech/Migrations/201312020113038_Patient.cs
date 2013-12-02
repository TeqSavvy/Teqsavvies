namespace Physiotech.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Patient : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clinic", "Patient_Id", "dbo.Patient");
            DropIndex("dbo.Clinic", new[] { "Patient_Id" });
            DropColumn("dbo.Clinic", "Patient_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clinic", "Patient_Id", c => c.Int());
            CreateIndex("dbo.Clinic", "Patient_Id");
            AddForeignKey("dbo.Clinic", "Patient_Id", "dbo.Patient", "Id");
        }
    }
}
