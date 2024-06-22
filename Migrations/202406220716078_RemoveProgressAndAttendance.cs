namespace SwimmingLessonManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProgressAndAttendance : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendances", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Attendances", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Progresses", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Progresses", "Student_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "LessonId" });
            DropIndex("dbo.Attendances", new[] { "Student_Id" });
            DropIndex("dbo.Progresses", new[] { "LessonId" });
            DropIndex("dbo.Progresses", new[] { "Student_Id" });
            DropTable("dbo.Attendances");
            DropTable("dbo.Progresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Progresses",
                c => new
                    {
                        ProgressId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                        Achievement = c.String(),
                        Feedback = c.String(),
                        DateLogged = c.DateTime(nullable: false),
                        Student_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProgressId);
            
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        AttendanceId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsPresent = c.Boolean(nullable: false),
                        Student_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AttendanceId);
            
            CreateIndex("dbo.Progresses", "Student_Id");
            CreateIndex("dbo.Progresses", "LessonId");
            CreateIndex("dbo.Attendances", "Student_Id");
            CreateIndex("dbo.Attendances", "LessonId");
            AddForeignKey("dbo.Progresses", "Student_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Progresses", "LessonId", "dbo.Lessons", "LessonID", cascadeDelete: true);
            AddForeignKey("dbo.Attendances", "Student_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Attendances", "LessonId", "dbo.Lessons", "LessonID", cascadeDelete: true);
        }
    }
}
