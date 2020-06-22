using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcTeam.MVCSite.Models.ArticleViewModels
{
    public class CreateArticleViewModel
    {
        [Required(ErrorMessage = "标题不能为空")]
        [Display(Name = "文章标题")]
        public string Title { get; set; }

        [Required(ErrorMessage = "内容不能为空")]
        [Display(Name = "文章内容")]
        public string Content { get; set; }

        [Display(Name = "用户文章分类")]
        public Guid[] CategoryIds { get; set; }
    }
}