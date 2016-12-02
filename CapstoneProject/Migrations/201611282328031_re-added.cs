namespace CapstoneProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class readded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                        Credits = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CourseSubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseSubjects", t => t.CourseSubjectId, cascadeDelete: true)
                .Index(t => t.CourseSubjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CourseSubjectId", "dbo.CourseSubjects");
            DropIndex("dbo.Courses", new[] { "CourseSubjectId" });
            DropTable("dbo.Courses");
        }
    }
}
