namespace Physiotech.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Crap : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Patients", newName: "Patient");
            AddColumn("dbo.Clinic", "Patient_Id", c => c.Int());
            AddForeignKey("dbo.Clinic", "Patient_Id", "dbo.Patient", "Id");
            CreateIndex("dbo.Clinic", "Patient_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Clinic", new[] { "Patient_Id" });
            DropForeignKey("dbo.Clinic", "Patient_Id", "dbo.Patient");
            DropColumn("dbo.Clinic", "Patient_Id");
            RenameTable(name: "dbo.Patient", newName: "Patients");
        }
    }
}
