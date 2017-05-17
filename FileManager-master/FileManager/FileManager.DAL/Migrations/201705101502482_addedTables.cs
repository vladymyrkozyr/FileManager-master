namespace FileManager.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedTables : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.User", name: "UserId", newName: "Id");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.User", name: "Id", newName: "UserId");
        }
    }
}
