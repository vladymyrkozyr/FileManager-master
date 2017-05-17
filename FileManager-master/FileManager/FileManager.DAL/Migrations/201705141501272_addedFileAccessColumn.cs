namespace FileManager.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedFileAccessColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.File", "FileAccess", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.File", "FileAccess");
        }
    }
}
