namespace CapstoneProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedappointments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ReasonForAppointment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StudentId = c.String(maxLength: 128),
                        AdvisorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AdvisorId)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.AdvisorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointments", "AdvisorId", "dbo.AspNetUsers");
            DropIndex("dbo.Appointments", new[] { "AdvisorId" });
            DropIndex("dbo.Appointments", new[] { "StudentId" });
            DropTable("dbo.Appointments");
        }
    }
}
