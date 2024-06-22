using System.Data.Entity.Migrations;

public partial class DropEnrollmentsTable : DbMigration
{
    public override void Up()
    {
        DropTable("dbo.Enrollments");
    }

    public override void Down()
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
}
