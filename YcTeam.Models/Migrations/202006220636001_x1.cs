namespace YcTeam.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x1 : DbMigration
    {
        public override void Up()
        {
          
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InStorageTasks", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.InStorageReceives", "UserId", "dbo.SysUsers");
            DropForeignKey("dbo.InStorageReceives", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.InStorageReceives", "MaterialId", "dbo.Materials");
            DropIndex("dbo.InStorageTasks", new[] { "MaterialId" });
            DropIndex("dbo.InStorageReceives", new[] { "UserId" });
            DropIndex("dbo.InStorageReceives", new[] { "MaterialId" });
            DropIndex("dbo.InStorageReceives", new[] { "ProviderId" });
            DropTable("dbo.InStorageTasks");
            DropTable("dbo.InStorageReceives");
        }
    }
}
