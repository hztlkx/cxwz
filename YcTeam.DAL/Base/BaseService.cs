using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YcTeam.Models;
using YcTeam.IDAL;


namespace YcTeam.DAL
{
    public class BaseService<T> : IBaseService<T> where T :BaseEntity,new()
    {
        public readonly YcContext Db;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="db"></param>
        public BaseService(YcContext db)
        {
            Db = db;
        }

        #region 常用方法
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <param name="saved"></param>
        /// <returns></returns>
        public async Task CreateAsync(T model, bool saved = true)
        {
            Db.Set<T>().Add(model);
            if (saved)
            {
                try
                {
                    await Db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="saved"></param>
        /// <returns></returns>
        public async Task<int> EditAsync(T model, bool saved = true)
        {
            Db.Configuration.ValidateOnSaveEnabled = false;
            Db.Entry(model).State = EntityState.Modified;
            if (saved)
            {
                try
                {
                    var result = await Db.SaveChangesAsync();
                    Db.Configuration.ValidateOnSaveEnabled = true;
                    return result;
                }
                catch (Exception ex)
                {
                    string exp = ex.Message.ToString();
                    return -1;
                }
            }

            return -1;
        }

        /// <summary>
        /// 删除（根据实体编号）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saved"></param>
        /// <returns></returns>
        public async Task<int> RemoveAsync(Guid id, bool saved = true)
        {
            //去除T的所有属性的校验
            Db.Configuration.ValidateOnSaveEnabled = false;
            //只执行一次数据库操作，不需要查询，体现了EF修改的本质（通过实体对象的状态进行修改）。
            var t = new T(){Id = id};
            //t的状态没有修改
            Db.Entry(t).State = EntityState.Unchanged;//欺骗数据库与数据库同步，持久态
            t.IsRemoved = true;//数据表的IsRemoved属性转变为删除，没有真正从数据库删除记录
            if (saved)
            {
                try
                {
                    var result = await Db.SaveChangesAsync();
                    Db.Configuration.ValidateOnSaveEnabled = true;//恢复校验
                    return result;
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }

            return -1;
        }

        /// <summary>
        /// 删除（根据实体类型）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="saved"></param>
        /// <returns></returns>
        public async Task<int> RemoveAsync(T model, bool saved = true)
        {
            try
            {
                return await RemoveAsync(model.Id, saved);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 查询（根据编号）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saved"></param>
        /// <returns></returns>
        public async Task<T> GetOneByIdAsync(Guid id, bool saved = true)
        {
            return await GetAllAsync().FirstAsync(m=>m.Id==id && !m.IsRemoved);
        }

        /// <summary>
        /// 保存所有操作（真实执行数据库操作）
        /// </summary>
        /// <returns></returns>
        public async Task Save()
        {
            await Db.SaveChangesAsync();
            Db.Configuration.ValidateOnSaveEnabled = true;
        }

        /// <summary>
        /// 返回所有未被删除的数据（没有真的执行）
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAllAsync()
        {
            return Db.Set<T>().Where(m => !m.IsRemoved).AsNoTracking();
        }

        /// <summary>
        /// 获取所有分页数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllByPageAsync(int pageSize = 10, int pageIndex = 0)
        {
            return GetAllAsync().Skip(pageSize * pageIndex).Take(pageSize);
        }

        /// <summary>
        /// 获取所有排序的数据
        /// </summary>
        /// <param name="asc"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllOrderAsync(bool asc = true)
        {
            var dataResult = GetAllAsync().OrderBy(m => m.CreateTime);
            dataResult = asc ? dataResult.OrderBy(m => m.CreateTime) : dataResult.OrderByDescending(m => m.CreateTime);
            return dataResult;
        }

        /// <summary>
        /// 获取所有分页的数据（排序的数据）
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllByPageOrderAsync(int pageIndex = 0,int pageSize = 10, bool asc = true)
        {
            return GetAllOrderAsync(asc).Skip(pageSize * pageIndex).Take(pageSize);
        }
        #endregion

        #region Lambda表达式
        /// <summary>
        /// 获取实体对象（默认单个）
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public T GetEntity(Func<T, bool> exp)
        {
            return Db.Set<T>().Where(exp).SingleOrDefault();
        }

        /// <summary>
        /// 获取实体对象（组）
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IQueryable<T> GetEntities(Func<T, bool> exp)
        {
            return Db.Set<T>().Where(exp) as IQueryable<T>;
        }

        /// <summary>
        /// 获取实体对象个数
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public int GetEntitiesCount(Func<T, bool> exp)
        {
            return Db.Set<T>().Where(exp).Count();
        }

        /// <summary>
        /// 获取所有页面（lambda表达式）
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllByPageAsync(int pageSize, int pageIndex, Func<T, bool> exp)
        {
            return GetAllAsync().AsEnumerable().Where(exp).Skip(pageSize * pageIndex).Take(pageSize) as IQueryable<T>;
        }
        #endregion

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            Db.Dispose();
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="orderExp">排序条件</param>
        /// <param name="expression">查询条件</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="includes">关联表</param>
        /// <returns></returns>
        public async Task<List<T>> GetList(
            Expression<Func<T, dynamic>> orderExp = null, 
            Expression<Func<T, bool>> expression = null,
            string orderBy = "desc",
            Expression<Func<T, object>>[] includes = null)
        {
            try
            {
                IQueryable<T> query = Db.Set<T>().Where(m => !m.IsRemoved).AsNoTracking().AsQueryable();
                if (includes != null && includes.Any())
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                if (expression != null)
                {
                    query = query.Where(expression);
                }

                if (orderExp != null)
                {
                    return orderBy == "desc"
                        ? await query.OrderByDescending(orderExp).ToListAsync()
                        : await query.OrderBy(orderExp).ToListAsync();
                }
                ;
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
