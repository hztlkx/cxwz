using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.Models;

namespace YcTeam.IDAL
{
    public interface IBaseService<T>:IDisposable where T : BaseEntity
    {
        #region 常用方法
        Task CreateAsync(T model, bool saved = true);
        Task<int> EditAsync(T model, bool saved = true);

        Task<int> RemoveAsync(Guid id, bool saved = true);

        Task<int> RemoveAsync(T model, bool saved = true);

        Task<T> GetOneByIdAsync(Guid id, bool saved = true);

        Task Save();

        IQueryable<T> GetAllAsync();

        IQueryable<T> GetAllByPageAsync(int pageSize = 10, int pageIndex = 0);

        IQueryable<T> GetAllOrderAsync(bool asc = true);

        IQueryable<T> GetAllByPageOrderAsync(int pageSize = 10, int pageIndex = 0, bool asc = true);
        #endregion

        #region Lambda表达式
        T GetEntity(Func<T, bool> exp);

        IQueryable<T> GetEntities(Func<T, bool> exp);

        int GetEntitiesCount(Func<T, bool> exp);

        IQueryable<T> GetAllByPageAsync(int pageSize, int pageIndex, Func<T, bool> exp);
        #endregion
    }
}
