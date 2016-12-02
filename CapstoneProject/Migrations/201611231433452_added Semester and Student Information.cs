namespace CapstoneProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSemesterandStudentInformation : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Courses");
            AlterColumn("dbo.Courses", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Courses", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Courses");
            AlterColumn("dbo.Courses", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Courses", "Id");
        }
    }
}
