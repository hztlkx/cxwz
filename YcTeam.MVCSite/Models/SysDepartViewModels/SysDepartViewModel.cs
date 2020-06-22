using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcTeam.MVCSite.Models.SysDepartViewModels
{
    public class SysDepartViewModel
    {
       public Guid Id { get; set; }

       [Display(Name = "部门名称")]
       public string DepartName { get; set; }

        [Display(Name = "部门类型")]
        public int DepartType { get; set; }

        [Display(Name = "市级单位名称")] 
        public string RegionCity { get; set; }

        [Display(Name = "县级单位名称")] 
        public string RegionCounty { get; set; }
    }
}