using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace SpadCompanyPanel.Core.Models
{
    public class Category : IBaseEntity
    {
        public int Id { get; set; }

        [MaxLength(700)]
        [Display(Name = "دسته بندی")]
        public string CategoryName { get; set; }

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
