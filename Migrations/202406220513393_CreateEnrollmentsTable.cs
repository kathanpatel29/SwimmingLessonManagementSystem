using System.Data.Entity.Migrations;

public partial class CreateEnrollmentsTable : DbMigration
{
    public override void Up()
    {
        CreateTable(
            "dbo.Enrollments",
            c => new
            {
                EnrollmentID = c.Int(nullable: false, identity: true),
                EnrollmentDate = c.DateTime(nullable: false),
                LessonID = c.Int(nullable: false),
                StudentID = c.Int(nullable: false),
                Progress = c.String(),
            })
            .PrimaryKey(t => t.EnrollmentID)
            .ForeignKey("dbo.Lessons", t => t.LessonID, cascadeDelete: true)
            .ForeignKey("dbo.Users", t => t.StudentID, cascadeDelete: true)
            .Index(t => t.LessonID)
            .Index(t => t.StudentID);
    }

    public override void Down()
    {
        DropForeignKey("dbo.Enrollments", "StudentID", "dbo.Users");
        DropForeignKey("dbo.Enrollments", "LessonID", "dbo.Lessons");
        DropIndex("dbo.Enrollments", new[] { "StudentID" });
        DropIndex("dbo.Enrollments", new[] { "LessonID" });
        DropTable("dbo.Enrollments");
    }
}
