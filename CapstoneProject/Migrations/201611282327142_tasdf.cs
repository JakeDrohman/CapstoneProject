namespace CapstoneProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tasdf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "CourseSubjectId", "dbo.CourseSubjects");
            DropIndex("dbo.Courses", new[] { "CourseSubjectId" });
            DropTable("dbo.Courses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                        credits = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CourseSubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Courses", "CourseSubjectId");
            AddForeignKey("dbo.Courses", "CourseSubjectId", "dbo.CourseSubjects", "Id", cascadeDelete: true);
        }
    }
}
