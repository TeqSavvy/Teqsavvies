namespace Physiotech.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoPatient : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patient", "Clinic_Id", "dbo.Clinic");
            DropForeignKey("dbo.Diagnosis", "Patient_Id", "dbo.Patient");
            DropForeignKey("dbo.Appointments", "Staff_Id", "dbo.Staff");
            DropForeignKey("dbo.Appointments", "Patient_Id", "dbo.Patient");
            DropIndex("dbo.Patient", new[] { "Clinic_Id" });
            DropIndex("dbo.Diagnosis", new[] { "Patient_Id" });
            DropIndex("dbo.Appointments", new[] { "Staff_Id" });
            DropIndex("dbo.Appointments", new[] { "Patient_Id" });
            DropTable("dbo.Patient");
            DropTable("dbo.Diagnosis");
            DropTable("dbo.Appointments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        Staff_Id = c.Int(),
                        Patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        Patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.String(),
                        NextofKinMobile = c.String(),
                        Fullname = c.String(),
                        Phonenumber = c.String(),
                        EmailAddress = c.String(),
                        Address = c.String(),
                        Dob = c.String(),
                        Clinic_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Appointments", "Patient_Id");
            CreateIndex("dbo.Appointments", "Staff_Id");
            CreateIndex("dbo.Diagnosis", "Patient_Id");
            CreateIndex("dbo.Patient", "Clinic_Id");
            AddForeignKey("dbo.Appointments", "Patient_Id", "dbo.Patient", "Id");
            AddForeignKey("dbo.Appointments", "Staff_Id", "dbo.Staff", "Id");
            AddForeignKey("dbo.Diagnosis", "Patient_Id", "dbo.Patient", "Id");
            AddForeignKey("dbo.Patient", "Clinic_Id", "dbo.Clinic", "Id");
        }
    }
}
