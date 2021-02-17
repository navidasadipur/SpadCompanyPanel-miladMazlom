using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Infrastructure.Repositories;
using SpadCompanyPanel.Core.Models;
using System.Net;
using System.IO;
using SpadCompanyPanel.Infrastructure.Helpers;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class AboutMeController : Controller
    {
        private readonly AboutMeRepository _repo;
        public AboutMeController(AboutMeRepository repo)
        {
            _repo = repo;
        }
        //public ActionResult Index(int articleId)
        //{
        //    ViewBag.ArticleName = _repo.GetArticleName(articleId);
        //    ViewBag.ArticleId = articleId;
        //    return View(_repo.GetArticleHeadLines(articleId));
        //}

        public ActionResult Index()
        {
            var model = new AboutMe();

            //this table has one row
            model = _repo.GetFirstAboutMe();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AboutMe model)
        {
            //this table has one row
            var existingAboutMeModel = _repo.GetFirstAboutMe();

            if (existingAboutMeModel == null)
            {
                _repo.Add(model);

                return View(model);
            }

            existingAboutMeModel.Biography = model.Biography;

            _repo.Update(existingAboutMeModel);

            return View(existingAboutMeModel);
        }

        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AboutMe aboutMeImage, HttpPostedFileBase GalleryImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (GalleryImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(GalleryImage.FileName);
                    GalleryImage.SaveAs(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));

                    // Resizing Image
                    ImageResizer imageCut = new ImageResizer(1200, 1200, true);

                    imageCut.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
                        Server.MapPath("/Files/GalleryImages/" + newFileName));

                    ImageResizer thumb = new ImageResizer(600, 600, true);

                    thumb.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
                        Server.MapPath("/Files/GalleryImages/Thumb/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));
                    aboutMeImage.Image = newFileName;
                }
                #endregion

                var aboutMe = _repo.GetFirstAboutMe();

                aboutMe.Image = aboutMeImage.Image;
                aboutMe.ImageTitle = aboutMeImage.ImageTitle;

                _repo.Update(aboutMe);

                //_repo.Add(coverImage);

                return RedirectToAction("Index");
            }

            //ToDo
            return RedirectToAction("Index")/*View(coverImage)*/;
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutMe aboutMeImage = _repo.Get(id.Value);
            if (aboutMeImage == null)
            {
                return HttpNotFound();
            }
            return PartialView(aboutMeImage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AboutMe aboutMe, HttpPostedFileBase GalleryImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (GalleryImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + aboutMe.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + aboutMe.Image));

                    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/Thumb/" + aboutMe.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Thumb/" + aboutMe.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(GalleryImage.FileName);
                    GalleryImage.SaveAs(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));

                    // Resizing Image
                    ImageResizer imageCut = new ImageResizer(1200, 1200, true);

                    imageCut.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
                        Server.MapPath("/Files/GalleryImages/" + newFileName));

                    ImageResizer thumb = new ImageResizer(600, 600, true);

                    thumb.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
                        Server.MapPath("/Files/GalleryImages/Thumb/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));
                    aboutMe.Image = newFileName;
                }
                #endregion

                _repo.Update(aboutMe);
                return RedirectToAction("Index");
            }
            return View(aboutMe);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutMe aboutMeImage = _repo.Get(id.Value);
            if (aboutMeImage == null)
            {
                return HttpNotFound();
            }
            return PartialView(aboutMeImage);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var image = _repo.Get(id);

            //#region Delete Image
            //if (image.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + image.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + image.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + image.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + image.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }

        //public ActionResult Create(int articleId)
        //{
        //    ViewBag.ArticleId = articleId;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Cover headLine)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _repo.Add(headLine);
        //        return RedirectToAction("Index", new { articleId = headLine.ArticleId });
        //    }
        //    ViewBag.ArticleId = headLine.ArticleId;
        //    return View(headLine);
        //}

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Cover headLine = _repo.Get(id.Value);
        //    if (headLine == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(headLine);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Cover headLine)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _repo.Update(headLine);
        //        return RedirectToAction("Index", new { articleId = headLine.ArticleId });
        //    }
        //    return View(headLine);
        //}

        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Cover headLine = _repo.Get(id.Value);
        //    if (headLine == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(headLine);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var articleId = _repo.Get(id).ArticleId;
        //    _repo.Delete(id);
        //    return RedirectToAction("Index", new { articleId });
        //}
    }
}