namespace FileManager.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Last : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.File", "OwnerId", "dbo.User");
            DropIndex("dbo.File", new[] { "OwnerId" });
            RenameColumn(table: "dbo.File", name: "OwnerId", newName: "UserId");
            AlterColumn("dbo.File", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.File", "UserId");
            AddForeignKey("dbo.File", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.File", "UserId", "dbo.User");
            DropIndex("dbo.File", new[] { "UserId" });
            AlterColumn("dbo.File", "UserId", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.File", name: "UserId", newName: "OwnerId");
            CreateIndex("dbo.File", "OwnerId");
            AddForeignKey("dbo.File", "OwnerId", "dbo.User", "UserId");
        }
    }
}
