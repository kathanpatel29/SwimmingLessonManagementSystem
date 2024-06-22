namespace SwimmingLessonManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollments", "UserID_UserID", "dbo.Users");
            DropIndex("dbo.Enrollments", new[] { "UserID_UserID" });
            DropColumn("dbo.Enrollments", "UserID_UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enrollments", "UserID_UserID", c => c.Int());
            CreateIndex("dbo.Enrollments", "UserID_UserID");
            AddForeignKey("dbo.Enrollments", "UserID_UserID", "dbo.Users", "UserID");
        }
    }
}
