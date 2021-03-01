using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Core.Utility;
using SpadCompanyPanel.Infrastructure.Repositories;
using SpadCompanyPanel.Web.ViewModels;

namespace SpadCompanyPanel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly StaticContentDetailsRepository _contentRepo;
        private readonly GalleriesRepository _galleryRepo;
        private readonly GalleryVideosRepository _galleryVideosRepo;
        private readonly TestimonialsRepository _testimonialRepo;
        private readonly ContactFormsRepository _contactFormRepo;
        //private readonly OurTeamRepository _ourTeamRepo;
        //private readonly CertificatesRepository _certificatesRepo;
        //private readonly FoodGalleriesRepository _foodGalleriesRepo;
        private readonly CoverRepository _coverRepo;
        private readonly AboutMeRepository _aboutMeRepo;
        private readonly GalleryCategoryRepository _galleryCategoryRepo;
        private readonly PersonalCharacterRepository _personalCharacterRepo;
        private readonly MyContactInfoRepository _myContactInfoRepo;

        public HomeController( StaticContentDetailsRepository contentRepo, 
            GalleriesRepository galleryRepo,
            TestimonialsRepository testimonialRepo,
            ContactFormsRepository contactFormRepo,
            //OurTeamRepository ourTeamRepo,
            /*CertificatesRepository certificatesRepo,*/ 
            //FoodGalleriesRepository foodGalleriesRepo, 
            GalleryVideosRepository galleryVideosRepo, 
            CoverRepository coverRepo, 
            AboutMeRepository aboutMeRepo,
            GalleryCategoryRepository galleryCategoryRepo,
            PersonalCharacterRepository personalCharacterRepo,
            MyContactInfoRepository myContactInfoRepo
            )
        {
            _contentRepo = contentRepo;
            _galleryRepo = galleryRepo;
            _testimonialRepo = testimonialRepo;
            _contactFormRepo = contactFormRepo;
            //_ourTeamRepo = ourTeamRepo;
            //_certificatesRepo = certificatesRepo;
            //_foodGalleriesRepo = foodGalleriesRepo;
            _galleryVideosRepo = galleryVideosRepo;
            this._coverRepo = coverRepo;
            this._aboutMeRepo = aboutMeRepo;
            this._galleryCategoryRepo = galleryCategoryRepo;
            this._personalCharacterRepo = personalCharacterRepo;
            this._myContactInfoRepo = myContactInfoRepo;
        }
        public ActionResult Index()
        {
            //return Redirect("/Admin/Dashboard");
            
            if (_coverRepo.GetCount() == 0)
            {
                return View();
            }

            //cover repository has one row
            ViewBag.CoverTitle = _coverRepo.GetFirstCover().Title;
            ViewBag.CoverSubTitle = _coverRepo.GetFirstCover().SubTitle;
            ViewBag.CoverImage = _coverRepo.GetFirstCover().Image;

            if (_aboutMeRepo.GetCount() == 0)
            {
                return View();
            }
            //AboutMe repository has one row
            ViewBag.Biography = _aboutMeRepo.GetFirstAboutMe().Biography;
            ViewBag.BiographyImage = _aboutMeRepo.GetFirstAboutMe().Image;

            var categories = _galleryCategoryRepo.GetAllGalleryCategories();

            foreach (var category in categories)
            {
                var test = category.Galleries.Count;
            }

            //categories
            ViewBag.Categories = _galleryCategoryRepo.GetAllGalleryCategories();

            //Personal character
            ViewBag.PersonalCharacters = _personalCharacterRepo.GetAllPersonalCharacters();

            //video gallery
            ViewBag.Videos = _galleryVideosRepo.GetAll();

            //my contact info 
            ViewBag.MyContactInfo = _myContactInfoRepo.GetFirstMyContactInfo();
            
            ////filling the galleries of categories with reletive images
            //var galleries = _galleryRepo.GetGalleries();

            //foreach (var category in categoreis)
            //{
            //    foreach (var gallery in galleries)
            //    {
            //        if (gallery.GalleryCategoryId == category.Id)
            //        {
            //            category.Galleries.Add(gallery);
            //        }
            //    }
            //}

            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactForm contactForm)
        {

            if (ModelState.IsValid)
            {
                _contactFormRepo.Add(contactForm);

                return Json(new {success = true });
            }

            return Json(new { success = false });
        }

        public ActionResult ContactUsSummary()
        {
            //return Redirect("/Admin/Dashboard");


            if (_coverRepo.GetCount() == 0)
            {
                return View();
            }

            //cover repository has one row
            ViewBag.CoverTitle = _coverRepo.GetFirstCover().Title;
            ViewBag.CoverSubTitle = _coverRepo.GetFirstCover().SubTitle;
            ViewBag.CoverImage = _coverRepo.GetFirstCover().Image;

            if (_aboutMeRepo.GetCount() == 0)
            {
                return View();
            }
            //AboutMe repository has one row
            ViewBag.Biography = _aboutMeRepo.GetFirstAboutMe().Biography;
            ViewBag.BiographyImage = _aboutMeRepo.GetFirstAboutMe().Image;

            var categories = _galleryCategoryRepo.GetAllGalleryCategories();

            foreach (var category in categories)
            {
                var test = category.Galleries.Count;
            }

            //categories
            ViewBag.Categories = _galleryCategoryRepo.GetAllGalleryCategories();

            //Personal character
            ViewBag.PersonalCharacters = _personalCharacterRepo.GetAllPersonalCharacters();

            //video gallery

            ViewBag.Videos = _galleryVideosRepo.GetAll();

            return View();
        }


        public ActionResult Navbar()
        {
            ViewBag.Phone = _contentRepo.GetStaticContentDetail((int) StaticContents.Phone).ShortDescription;
            return PartialView();
        }
        public ActionResult HomeSlider()
        {
            var sliderContent = _contentRepo.GetContentByTypeId((int)StaticContentTypes.Slider);
            return PartialView(sliderContent);
        }
        public ActionResult Gallery()
        {
            var galleryContent = _galleryRepo.GetAll();
            return PartialView(galleryContent);
        }
        public ActionResult CompanyHistory()
        {
            var content = _contentRepo.GetContentByTypeId((int)StaticContentTypes.CompanyHistory).FirstOrDefault();
            return PartialView(content);
        }
        public ActionResult Testimonials()
        {
            var content = _testimonialRepo.GetAll();
            return PartialView(content);
        }
        public ActionResult GallerySlider()
        {
            var galleryContent = _galleryRepo.GetAll();
            return PartialView(galleryContent);
        }
        //public ActionResult ContactUsForm()
        //{
        //    return PartialView();
        //}
        [HttpPost]
        public ActionResult ContactUsForm(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                _contactFormRepo.Add(contactForm);
                return RedirectToAction("ContactUsSummary");
            }
            return View(contactForm);
        }

        //public ActionResult ContactUsSummary()
        //{
        //    return View();
        //}

        //public ActionResult OurTeamSection()
        //{
        //    var ourTeam = _ourTeamRepo.GetAll();
        //    return PartialView(ourTeam);
        //}
        //[Route("OurTeam")]
        //public ActionResult OurTeamPage()
        //{
        //    var ourTeam = _ourTeamRepo.GetAll();
        //    return View(ourTeam);
        //}
        public ActionResult Footer(bool isContactUsPage)
        {
            if (isContactUsPage)
                ViewBag.ContactUsPage = true;

            var footerContent = new FooterViewModel();
            footerContent.Map = _contentRepo.Get((int) StaticContents.Map);
            footerContent.Email = _contentRepo.Get((int) StaticContents.Email);
            footerContent.Address = _contentRepo.Get((int) StaticContents.Address);
            footerContent.Phone = _contentRepo.Get((int) StaticContents.Phone);
            footerContent.Youtube = _contentRepo.Get((int) StaticContents.Youtube);
            footerContent.Instagram = _contentRepo.Get((int) StaticContents.Instagram);
            footerContent.Twitter = _contentRepo.Get((int) StaticContents.Twitter);
            footerContent.Pinterest = _contentRepo.Get((int) StaticContents.Pinterest);
            footerContent.Facebook = _contentRepo.Get((int) StaticContents.Facebook);
            return PartialView(footerContent);
        }
        [Route("Gallery")]
        public ActionResult GalleryPage()
        {
            var images = _galleryRepo.GetAll();
            var videos = _galleryVideosRepo.GetAll();
            var vm = new GalleryPageViewModel()
            {
                Images = images,
                Videos = videos
            };
            return View(vm);
        }
        //[Route("Certificates")]
        //public ActionResult Certificates()
        //{
        //    var certificates = _certificatesRepo.GetAll();
        //    return View(certificates);
        //}
        //[Route("Foods")]
        //public ActionResult Foods()
        //{
        //    var foodGallery = _foodGalleriesRepo.GetAll();
        //    return View(foodGallery);
        //}
        [Route("AboutUs")]
        public ActionResult About()
        {
            return View();
        }

        [Route("ContactUs")]
        public ActionResult Contact()
        {
            ViewBag.ContactUsPage = true;
            var contactUsContent = new ContactUsViewModel();
            contactUsContent.ContactInfo = _contentRepo.Get((int)StaticContents.ContactInfo);
            contactUsContent.Email = _contentRepo.Get((int)StaticContents.Email);
            contactUsContent.Address = _contentRepo.Get((int)StaticContents.Address);
            contactUsContent.Phone = _contentRepo.Get((int)StaticContents.Phone);
            contactUsContent.Youtube = _contentRepo.Get((int)StaticContents.Youtube);
            contactUsContent.Instagram = _contentRepo.Get((int)StaticContents.Instagram);
            contactUsContent.Twitter = _contentRepo.Get((int)StaticContents.Twitter);
            contactUsContent.Pinterest = _contentRepo.Get((int)StaticContents.Pinterest);
            contactUsContent.Facebook = _contentRepo.Get((int)StaticContents.Facebook);
            return View(contactUsContent);
        }
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string vImagePath = String.Empty;
            string vMessage = String.Empty;
            string vFilePath = String.Empty;
            string vOutput = String.Empty;
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var vFileName = DateTime.Now.ToString("yyyyMMdd-HHMMssff") +
                                    Path.GetExtension(upload.FileName).ToLower();
                    var vFolderPath = Server.MapPath("/Upload/");
                    if (!Directory.Exists(vFolderPath))
                    {
                        Directory.CreateDirectory(vFolderPath);
                    }
                    vFilePath = Path.Combine(vFolderPath, vFileName);
                    upload.SaveAs(vFilePath);
                    vImagePath = Url.Content("/Upload/" + vFileName);
                    vMessage = "Image was saved correctly";
                }
            }
            catch
            {
                vMessage = "There was an issue uploading";
            }
            vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";
            return Content(vOutput);
        }
    }
}