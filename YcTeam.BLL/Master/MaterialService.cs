using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using YcTeam.DAL.Master;
using YcTeam.IDAL.Master;
using YcTeam.IBLL.Master;
using YcTeam.Models;
using YcTeam.Models.Master;

namespace YcTeam.BLL.Master
{
    public class MaterialService : IMaterialService
    {
        public async Task<DTO.Master.MaterialDto> GetOneMaterialById(Guid materialId)
        {
            using (IDAL.Master.IMaterialDao materialDao = new MaterialDao())
            {
                return await materialDao.GetAllAsync()
                    .Where(m => m.Id == materialId)
                    .Select(m => new DTO.Master.MaterialDto()
                    {
                        Id = m.Id,
                        Code = m.Code,
                        LargeCategory = m.LargeCategory,
                        SmallCategory=m.SmallCategory,
                        Describe=m.Describe,
                        Unit=m.Unit,
                        Note=m.Note,
                        CreateTime = m.CreateTime,
                    }).FirstAsync();
            }
        }
        
        public async Task EditMaterial(Guid materialId, string code, string largyCategory, string smallCategory, string describe, string unit, string note) 
        {
            using (var materialDao = new MaterialDao())
            {
                    var material = await materialDao.GetOneByIdAsync(materialId);
                    material.Code = code;
                    material.LargeCategory = largyCategory;
                    material.SmallCategory = smallCategory;
                    material.Describe = describe;
                    material.Unit = unit;
                    material.Note = note;
                    await materialDao.EditAsync(material);
            }
        }

        public async Task CreateMaterial(string code, string largyCategory,string smallCategory, string describe, string unit, string note)
        {
            using (var materialDao = new MaterialDao())
            {
                await materialDao.CreateAsync(new Material()
                {
                    Code=code,
                    LargeCategory = largyCategory,
                    SmallCategory=smallCategory,
                    Describe= describe,
                    Unit=unit,
                    Note=note
                });
            }
        }

        public async Task<bool> ExistsMaterial(Guid materialId)
        {
            using (IDAL.Master.IMaterialDao materialDao = new MaterialDao())
            {
                return await materialDao.GetAllAsync().AnyAsync(m => m.Id == materialId);
            }
        }

        public async Task<List<DTO.Master.MaterialDto>> GetAllMaterial(int pageIndex = 1, int pageSize = 10, bool asc = true)
        {
            using (var materialDao = new MaterialDao())
            {
                return await materialDao.GetAllByPageOrderAsync(pageIndex - 1, pageSize, asc).Select(m => new DTO.Master.MaterialDto()
                {
                    Id=m.Id,
                    Code = m.Code,
                    LargeCategory = m.LargeCategory,
                    SmallCategory=m.SmallCategory,
                    Describe = m.Describe,

                    Unit = m.Unit,
                    Note = m.Note,
                    CreateTime = m.CreateTime
                }).ToListAsync();
            }
        }

        public async Task<int> GetDataCount()
        {
            using (var materialDao = new MaterialDao())
            {
                return await materialDao.GetAllAsync().CountAsync();
            }         
        }

        public async Task RemoveMaterial(Guid id)
        {
            using (var materialDao = new MaterialDao())
            {
                await materialDao.RemoveAsync(id);
            }
        }

        public async Task<List<Material>> SearchMaterial(string text)
        {
            using (var materialDao = new MaterialDao())
            {
                return await materialDao.GetAllAsync().Where(m => m.Describe.Contains(text)).ToListAsync();
            }
        }
    }
}
