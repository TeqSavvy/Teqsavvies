
namespace Physiotech.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Code : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinic", t => t.Clinic_Id)
                .Index(t => t.Clinic_Id);
            
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        Patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id)
                .Index(t => t.Patient_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Staff", t => t.Staff_Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id)
                .Index(t => t.Staff_Id)
                .Index(t => t.Patient_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Appointments", new[] { "Patient_Id" });
            DropIndex("dbo.Appointments", new[] { "Staff_Id" });
            DropIndex("dbo.Diagnosis", new[] { "Patient_Id" });
            DropIndex("dbo.Patients", new[] { "Clinic_Id" });
            DropForeignKey("dbo.Appointments", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "Staff_Id", "dbo.Staff");
            DropForeignKey("dbo.Diagnosis", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.Patients", "Clinic_Id", "dbo.Clinic");
            DropTable("dbo.Appointments");
            DropTable("dbo.Diagnosis");
            DropTable("dbo.Patients");
        }
    }
}
