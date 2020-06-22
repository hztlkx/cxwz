namespace YcTeam.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x3 : DbMigration
    {
        public override void Up()
        {
            
            //CreateTable(
            //    "dbo.SysUsers",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            UserName = c.String(unicode: false),
            //            Password = c.String(nullable: false, unicode: false),
            //            RealName = c.String(nullable: false, unicode: false),
            //            SysDepartId = c.Guid(nullable: false),
            //            CreateTime = c.DateTime(nullable: false, precision: 0),
            //            IsRemoved = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.SysDeparts", t => t.SysDepartId)
            //    .Index(t => t.SysDepartId);
                  
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanMaterials", "PlanId", "dbo.Plans");
            DropForeignKey("dbo.PlanMaterials", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.InStorageTasks", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.InStorageTasks", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.InStorageTasks", "InStorageId", "dbo.InStorages");
            DropForeignKey("dbo.InStorageReceives", "UserId", "dbo.SysUsers");
            DropForeignKey("dbo.InStorageReceives", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.InStorageReceives", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.FlowRecords", "FlowUserId", "dbo.SysUsers");
            DropForeignKey("dbo.FlowRecords", "FlowInstanceId", "dbo.FlowInstances");
            DropForeignKey("dbo.FlowInstances", "ToDoUserId", "dbo.SysUsers");
            DropForeignKey("dbo.FlowInstances", "StartUserId", "dbo.SysUsers");
            DropForeignKey("dbo.FlowInstances", "PlanId", "dbo.Plans");
            DropForeignKey("dbo.Plans", "SysDepartId", "dbo.SysDeparts");
            DropForeignKey("dbo.Plans", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "SysDepartId", "dbo.SysDeparts");
            DropForeignKey("dbo.FlowInstances", "OperatingUserId", "dbo.SysUsers");
            DropForeignKey("dbo.FlowInstances", "OperatedUserId", "dbo.SysUsers");
            DropForeignKey("dbo.FlowInstances", "NodeNumber", "dbo.FlowNodes");
            DropForeignKey("dbo.FlowNodes", "SysRoleId", "dbo.SysRoles");
            DropForeignKey("dbo.SysUserRoles", "SysUserId", "dbo.SysUsers");
            DropForeignKey("dbo.SysUsers", "SysDepartId", "dbo.SysDeparts");
            DropForeignKey("dbo.SysUserRoles", "SysRoleId", "dbo.SysRoles");
            DropForeignKey("dbo.SysRolePermissions", "RoleId", "dbo.SysRoles");
            DropForeignKey("dbo.SysRolePermissions", "PermissionId", "dbo.SysPermissions");
            DropForeignKey("dbo.SysNavRoles", "RoleId", "dbo.SysRoles");
            DropForeignKey("dbo.SysNavRoles", "NavItemId", "dbo.SysNavItems");
            DropForeignKey("dbo.SysNavItems", "NavId", "dbo.SysNavs");
            DropIndex("dbo.PlanMaterials", new[] { "MaterialId" });
            DropIndex("dbo.PlanMaterials", new[] { "PlanId" });
            DropIndex("dbo.InStorageTasks", new[] { "ProviderId" });
            DropIndex("dbo.InStorageTasks", new[] { "InStorageId" });
            DropIndex("dbo.InStorageTasks", new[] { "MaterialId" });
            DropIndex("dbo.InStorageReceives", new[] { "UserId" });
            DropIndex("dbo.InStorageReceives", new[] { "MaterialId" });
            DropIndex("dbo.InStorageReceives", new[] { "ProviderId" });
            DropIndex("dbo.FlowRecords", new[] { "FlowUserId" });
            DropIndex("dbo.FlowRecords", new[] { "FlowInstanceId" });
            DropIndex("dbo.Projects", new[] { "SysDepartId" });
            DropIndex("dbo.Plans", new[] { "SysDepartId" });
            DropIndex("dbo.Plans", new[] { "ProjectId" });
            DropIndex("dbo.SysUsers", new[] { "SysDepartId" });
            DropIndex("dbo.SysUserRoles", new[] { "SysRoleId" });
            DropIndex("dbo.SysUserRoles", new[] { "SysUserId" });
            DropIndex("dbo.SysRolePermissions", new[] { "PermissionId" });
            DropIndex("dbo.SysRolePermissions", new[] { "RoleId" });
            DropIndex("dbo.SysNavItems", new[] { "NavId" });
            DropIndex("dbo.SysNavRoles", new[] { "NavItemId" });
            DropIndex("dbo.SysNavRoles", new[] { "RoleId" });
            DropIndex("dbo.FlowNodes", new[] { "SysRoleId" });
            DropIndex("dbo.FlowInstances", new[] { "PlanId" });
            DropIndex("dbo.FlowInstances", new[] { "OperatedUserId" });
            DropIndex("dbo.FlowInstances", new[] { "ToDoUserId" });
            DropIndex("dbo.FlowInstances", new[] { "OperatingUserId" });
            DropIndex("dbo.FlowInstances", new[] { "StartUserId" });
            DropIndex("dbo.FlowInstances", new[] { "NodeNumber" });
            DropTable("dbo.PlanMaterials");
            DropTable("dbo.InStorageTasks");
            DropTable("dbo.Providers");
            DropTable("dbo.Materials");
            DropTable("dbo.InStorageReceives");
            DropTable("dbo.InStorages");
            DropTable("dbo.FlowRecords");
            DropTable("dbo.Projects");
            DropTable("dbo.Plans");
            DropTable("dbo.SysDeparts");
            DropTable("dbo.SysUsers");
            DropTable("dbo.SysUserRoles");
            DropTable("dbo.SysPermissions");
            DropTable("dbo.SysRolePermissions");
            DropTable("dbo.SysNavs");
            DropTable("dbo.SysNavItems");
            DropTable("dbo.SysNavRoles");
            DropTable("dbo.SysRoles");
            DropTable("dbo.FlowNodes");
            DropTable("dbo.FlowInstances");
        }
    }
}
