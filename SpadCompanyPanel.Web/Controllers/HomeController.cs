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

    }
}