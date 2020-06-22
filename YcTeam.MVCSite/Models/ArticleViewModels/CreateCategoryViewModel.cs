using System.ComponentModel.DataAnnotations;

namespace YcTeam.MVCSite.Models.ArticleViewModels
{
    public class CreateCategoryViewModel
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [Required]
        [StringLength(maximumLength:200,MinimumLength = 2)]
        [Display(Name = "类型名称")]
        public string  CategoryName { get; set; }

    }
}