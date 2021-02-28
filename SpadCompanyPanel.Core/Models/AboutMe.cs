using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace SpadCompanyPanel.Core.Models
{
    public class AboutMe : IBaseEntity
    {
        public int Id { get; set; }

        [Display(Name = "تصویر")]
        public string Image { get; set; }

        [Display(Name = "عنوان تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageTitle { get; set; }

        [MaxLength(10000)]
        [Display(Name = "درباره من")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Biography { get; set; }

        public string InsertUser { get; set; }

        public DateTime? InsertDate { get; set; }

        public string UpdateUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsDeleted { get; set; }

        ////[Display(Name = "توضیح")]
        ////[DataType(DataType.MultilineText)]
        ////[AllowHtml]
        ////public string Description { get; set; }

        ////public int ArticleId { get; set; }
        ////public Article Article { get; set; }
        ///

    }
}
