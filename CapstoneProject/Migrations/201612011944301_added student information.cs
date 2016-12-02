namespace CapstoneProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstudentinformation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AlternateFirstName = c.String(),
                        AlternateLastName = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zipcode = c.String(),
                        AlternateStreet = c.String(),
                        AlternateCity = c.String(),
                        AlternateState = c.String(),
                        AlternateZipcode = c.String(),
                        EmailAddress = c.String(),
                        DateAdmitted = c.DateTime(nullable: false),
                        Track = c.String(),
                        GraduationDate = c.DateTime(),
                        Graduated = c.Boolean(),
                        Withdrawn = c.Boolean(),
                        Dismissed = c.Boolean(),
                        CreditsNeeded = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditsCompleted = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentInformations", "StudentId", "dbo.AspNetUsers");
            DropIndex("dbo.StudentInformations", new[] { "StudentId" });
            DropTable("dbo.StudentInformations");
        }
    }
}
