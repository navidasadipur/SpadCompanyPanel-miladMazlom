using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace SpadCompanyPanel.Core.Models
{
    public class GalleryCategory : IBaseEntity
    {
        public int Id { get; set; }

        [Display(Name = "نام دسته")]
        [MaxLength(400, ErrorMessage = "نام دسته باید از 400 کارکتر کمتر باشد")]
        [Required(ErrorMessage = "لطفا نام دسته را وارد کنید")]
        public string Title { get; set; }

        public string InsertUser { get; set; }

        public DateTime? InsertDate { get; set; }

        public string UpdateUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Gallery> Galleries { get; set; }

        ////[Display(Name = "توضیح")]
        ////[DataType(DataType.MultilineText)]
        ////[AllowHtml]
        ////public string Description { get; set; }

        ////public int ArticleId { get; set; }
        ////public Article Article { get; set; }
        ///

    }
}
