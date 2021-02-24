using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpadCompanyPanel.Web.Areas.Admin.Models
{
    public class PersonalCharacterPanelView
    {
        [Display(Name = "نام ویژگی یک")]
        [MaxLength(400, ErrorMessage = "نام ویژگی باید از 400 کارکتر کمتر باشد")]
        public string TitleOne { get; set; }

        

        [Display(Name = "نام ویژگی دو")]
        [MaxLength(400, ErrorMessage = "نام ویژگی باید از 400 کارکتر کمتر باشد")]
        public string TitleTwo { get; set; }

        [Display(Name = "نام ویژگی سه")]
        [MaxLength(400, ErrorMessage = "نام ویژگی باید از 400 کارکتر کمتر باشد")]
        public string TitleThree { get; set; }


        [Display(Name = "توضیح کوتاه ویژگی یک")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ShortDescriptionOne { get; set; }

        [Display(Name = "توضیح کوتاه ویژگی دو")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ShortDescriptionTwo { get; set; }

        
        [Display(Name = "توضیح کوتاه ویژگی سه")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ShortDescriptionThree { get; set; }

        [Display(Name = "تصویر یک")]
        public string ImageOne { get; set; }

        [Display(Name = "عنوان تصویر یک")]
        public string ImageTitleOne { get; set; }

        [Display(Name = "تصویر دو")]
        public string ImageTwo { get; set; }

        [Display(Name = "عنوان تصویر دو")]
        public string ImageTitleTwo { get; set; }

        [Display(Name = "تصویر سه")]
        public string ImageThree { get; set; }

        [Display(Name = "عنوان تصویر سه")]
        public string ImageTitleThree { get; set; }
    }
}