namespace FileManager.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedFileTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.File",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(maxLength: 128),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.User", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.File", "OwnerId", "dbo.User");
            DropIndex("dbo.File", new[] { "OwnerId" });
            DropTable("dbo.File");
        }
    }
}
