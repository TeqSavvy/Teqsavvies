namespace Physiotech.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Clinic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Clinic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        VerificationCode = c.String(),
                        Staff_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Staff", t => t.Staff_Id)
                .Index(t => t.Staff_Id);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.String(),
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
                .ForeignKey("dbo.Patient", t => t.Patient_Id)
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
                .ForeignKey("dbo.Patient", t => t.Patient_Id)
                .Index(t => t.Staff_Id)
                .Index(t => t.Patient_Id);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                        ClinicId = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Staff", new[] { "Clinic_Id" });
            DropIndex("dbo.Appointments", new[] { "Patient_Id" });
            DropIndex("dbo.Appointments", new[] { "Staff_Id" });
            DropIndex("dbo.Diagnosis", new[] { "Patient_Id" });
            DropIndex("dbo.Patient", new[] { "Clinic_Id" });
            DropIndex("dbo.Clinic", new[] { "Staff_Id" });
            DropForeignKey("dbo.Staff", "Clinic_Id", "dbo.Clinic");
            DropForeignKey("dbo.Appointments", "Patient_Id", "dbo.Patient");
            DropForeignKey("dbo.Appointments", "Staff_Id", "dbo.Staff");
            DropForeignKey("dbo.Diagnosis", "Patient_Id", "dbo.Patient");
            DropForeignKey("dbo.Patient", "Clinic_Id", "dbo.Clinic");
            DropForeignKey("dbo.Clinic", "Staff_Id", "dbo.Staff");
            DropTable("dbo.Staff");
            DropTable("dbo.Appointments");
            DropTable("dbo.Diagnosis");
            DropTable("dbo.Patient");
            DropTable("dbo.Clinic");
            DropTable("dbo.UserProfile");
        }
    }
}
