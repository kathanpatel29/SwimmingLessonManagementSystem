namespace SwimmingLessonManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollments", "Student_UserID", "dbo.Users");
            DropIndex("dbo.Enrollments", new[] { "Student_UserID" });
            DropColumn("dbo.Enrollments", "StudentID");
            RenameColumn(table: "dbo.Enrollments", name: "Student_UserID", newName: "StudentID");
            AlterColumn("dbo.Enrollments", "StudentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Enrollments", "StudentID");
            AddForeignKey("dbo.Enrollments", "StudentID", "dbo.Users", "UserID", cascadeDelete: false);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Enrollments", "StudentID", "dbo.Users");
            DropIndex("dbo.Enrollments", new[] { "StudentID" });
            AlterColumn("dbo.Enrollments", "StudentID", c => c.Int());
            RenameColumn(table: "dbo.Enrollments", name: "StudentID", newName: "Student_UserID");
            AddColumn("dbo.Enrollments", "StudentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Enrollments", "Student_UserID");
            AddForeignKey("dbo.Enrollments", "Student_UserID", "dbo.Users", "UserID");
        }
    }
}
