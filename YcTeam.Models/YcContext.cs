using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using YcTeam.Models.FlowInto;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;
using YcTeam.Models.WorkFlow;

namespace YcTeam.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class YcContext : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“YcModel”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“YcTeam.Models.YcModel”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“YcModel”
        //连接字符串。
        public YcContext() : base(nameOrConnectionString: "con")
        {
            //策略一：数据库不存在时重新创建数据库
            Database.SetInitializer<YcContext>(new CreateDatabaseIfNotExists<YcContext>());

            //策略二：每次启动应用程序时创建数据库
            //Database.SetInitializer<YcContext>(new DropCreateDatabaseAlways<YcContext>());

            //策略三：模型更改时重新创建数据库
            //Database.SetInitializer<YcContext>(new DropCreateDatabaseIfModelChanges<YcContext>());

            //策略四：从不创建数据库
            //Database.SetInitializer<YcContext>(strategy: null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Code First模式级联删除是默认打开的,关闭外键关系下的级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        #region 系统管理
        //导航滑块
        public DbSet<SysNav> SysNav { get; set; }

        //导航节点
        public DbSet<SysNavItem> SysNavItem { get; set; }

        //用户
        public DbSet<SysUser> SysUser { get; set; }

        //导航角色中间表
        public DbSet<SysNavRole> SysNavRole { get; set; }

        //用户角色
        public DbSet<SysRole> SysRole { get; set; }

        //用户角色中间表
        public DbSet<SysUserRole> SysUserRole { get; set; }

        //部门
        public DbSet<SysDepart> SysDepart { get; set; }
        #endregion

        #region 主数据
        //供应商
        public DbSet<Provider> Provider { get; set; }

        //项目信息
        public DbSet<Project> Project { get; set; }

        //物料信息
        public DbSet<Material> Material { get; set; }

        public DbSet<InStorage> InStorage { get; set; }

        //权限角色中间表
        public DbSet<SysRolePermission> SysRolePermission { get; set; }

        //权限信息
        public DbSet<SysPermission> SysPermission { get; set; }
        #endregion

        #region 计划管理
        public DbSet<FlowPlan.Plan> Plan { get; set; }

        public DbSet<FlowPlan.PlanMaterial> PlanMaterial { get; set; }
        #endregion

        #region 入库管理
        public DbSet<InStorageTask> InStorageTask { get; set; }

        public DbSet<InStorageReceive> InStorageReceive { get; set; }
        #endregion

        #region 工作流
        //工作流实例
        public DbSet<FlowInstance> FlowInstance { get; set; }

        //工作流记录
        public DbSet<FlowRecord> FlowRecord { get; set; }

        //工作流节点
        public DbSet<FlowNode> FlowNode { get; set; }
        #endregion
    }
}