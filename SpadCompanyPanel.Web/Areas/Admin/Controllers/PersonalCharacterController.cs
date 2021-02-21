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
using SpadCompanyPanel.Web.Areas.Admin.Models;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class PersonalCharacterController : Controller
    {
        private readonly PersonalCharacterRepository _repo;
        public PersonalCharacterController(PersonalCharacterRepository repo)
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
            var personalCharacters = _repo.GetAll().OrderBy(ch => ch.Id);

            var personalCharacterPanelView = new PersonalCharacterPanelView
            {
                TitleOne = personalCharacters.OrderBy(ch => ch.Id).FirstOrDefault().Title,
                TitleTwo = personalCharacters.OrderBy(ch => ch.Id).Skip(1).FirstOrDefault().Title,
                TitleThree = personalCharacters.OrderBy(ch => ch.Id).Skip(2).FirstOrDefault().Title,

                ShortDescriptionOne = personalCharacters.OrderBy(ch => ch.Id).FirstOrDefault().ShortDescription,
                ShortDescriptionTwo = personalCharacters.OrderBy(ch => ch.Id).Skip(1).FirstOrDefault().ShortDescription,
                ShortDescriptionThree = personalCharacters.OrderBy(ch => ch.Id).Skip(2).FirstOrDefault().ShortDescription,
            };

            return View(personalCharacterPanelView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PersonalCharacterPanelView model)
        {
            var firstPersonalChar = _repo.GetFirstPersonalCharacter();
            var secondPersonalChar = _repo.GetSecondPersonalCharacter();
            var thirdPersonalChar = _repo.GetThirdPersonalCharacter();

            Update_AddPersonalCharInDb(model.TitleOne, model.ShortDescriptionOne, firstPersonalChar);
            Update_AddPersonalCharInDb(model.TitleTwo, model.ShortDescriptionTwo, secondPersonalChar);
            Update_AddPersonalCharInDb(model.TitleThree, model.ShortDescriptionThree, thirdPersonalChar);

            //if (model.TitleOne != null || model.)
            //{
            //    var firstPersonalChar = _repo.GetFirstPersonalCharacter();

            //    Update_AddPersonalCharInDb(model, firstPersonalChar);
            //}
            //else if (btn.Equals("ثبت ویژگی 2"))
            //{
            //    var secondPersonalChar = _repo.GetSecondPersonalCharacter();

            //    Update_AddPersonalCharInDb(model, secondPersonalChar);
            //}
            //else if (btn.Equals("ثبت ویژگی 3"))
            //{
            //    var thirdPersonalChar = _repo.GetThirdPersonalCharacter();

            //    Update_AddPersonalCharInDb(model, thirdPersonalChar);
            //}


            ////this table has one row
            //var allExistingPersonalCharModel = _repo.GetAll();

            ////if (allExistingPersonalModel == null)
            ////{
            ////    _repo.Add(model);

            ////    return View(model);
            ////}
            //foreach (var personalModel in allExistingPersonalCharModel)
            //{
            //    personalModel.Title = model.Title;
            //    personalModel.ShortDescription = model.ShortDescription;

            //    _repo.Update(personalModel);
            //}

            //return View(allExistingPersonalCharModel);

            return RedirectToAction("Index");
        }

        private void Update_AddPersonalCharInDb(string title, string shortDescription, PersonalCharacter PersonalChar)
        {
            PersonalChar.Title = title;
            PersonalChar.ShortDescription = shortDescription;

            if (PersonalChar == null)
            {
                _repo.Add(PersonalChar);
            }
            else
            {
                _repo.Update(PersonalChar);
            }
        }

        public ActionResult Create1()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1(PersonalCharacter personalCharacterImage, HttpPostedFileBase GalleryImage)
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
                    personalCharacterImage.Image = newFileName;
                }
                #endregion
                
                //saving image and imageTitle to the database
                var firstPersonalCharacter = _repo.GetFirstPersonalCharacter();

                if (firstPersonalCharacter == null)
                {
                    _repo.Add(personalCharacterImage);
                }
                else
                {
                    firstPersonalCharacter.Image = personalCharacterImage.Image;
                    firstPersonalCharacter.ImageTitle = personalCharacterImage.ImageTitle;

                    _repo.Update(firstPersonalCharacter);
                }

                return RedirectToAction("Index");
            }

                ////////////ToDo
                return RedirectToAction("Index")/*View(coverImage)*/;
        }

        public ActionResult Create2()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(PersonalCharacter personalCharacterImage, HttpPostedFileBase GalleryImage)
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
                    personalCharacterImage.Image = newFileName;
                }
                #endregion

                //saving image and imageTitle to the database
                var secondPersonalCharacter = _repo.GetSecondPersonalCharacter();

                if (secondPersonalCharacter == null)
                {
                    _repo.Add(personalCharacterImage);
                }
                else
                {
                    secondPersonalCharacter.Image = personalCharacterImage.Image;
                    secondPersonalCharacter.ImageTitle = personalCharacterImage.ImageTitle;

                    _repo.Update(secondPersonalCharacter);
                }

                return RedirectToAction("Index");
            }

            ////////////ToDo
            return RedirectToAction("Index")/*View(coverImage)*/;
        }

        public ActionResult Create3()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create3(PersonalCharacter personalCharacterImage, HttpPostedFileBase GalleryImage)
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
                    personalCharacterImage.Image = newFileName;
                }
                #endregion

                //saving image and imageTitle to the database
                var thirdPersonalCharacter = _repo.GetThirdPersonalCharacter();

                if (thirdPersonalCharacter == null)
                {
                    _repo.Add(personalCharacterImage);
                }
                else
                {
                    thirdPersonalCharacter.Image = personalCharacterImage.Image;
                    thirdPersonalCharacter.ImageTitle = personalCharacterImage.ImageTitle;

                    _repo.Update(thirdPersonalCharacter);
                }

                return RedirectToAction("Index");
            }

            ////////////ToDo
            return RedirectToAction("Index")/*View(coverImage)*/;
        }

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AboutMe aboutMeImage = _repo.Get(id.Value);
        //    if (aboutMeImage == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(aboutMeImage);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(AboutMe aboutMe, HttpPostedFileBase GalleryImage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        #region Upload Image
        //        if (GalleryImage != null)
        //        {
        //            if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + aboutMe.Image)))
        //                System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + aboutMe.Image));

        //            if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/Thumb/" + aboutMe.Image)))
        //                System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Thumb/" + aboutMe.Image));

        //            // Saving Temp Image
        //            var newFileName = Guid.NewGuid() + Path.GetExtension(GalleryImage.FileName);
        //            GalleryImage.SaveAs(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));

        //            // Resizing Image
        //            ImageResizer imageCut = new ImageResizer(1200, 1200, true);

        //            imageCut.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
        //                Server.MapPath("/Files/GalleryImages/" + newFileName));

        //            ImageResizer thumb = new ImageResizer(600, 600, true);

        //            thumb.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
        //                Server.MapPath("/Files/GalleryImages/Thumb/" + newFileName));

        //            // Deleting Temp Image
        //            System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));
        //            aboutMe.Image = newFileName;
        //        }
        //        #endregion

        //        _repo.Update(aboutMe);
        //        return RedirectToAction("Index");
        //    }
        //    return View(aboutMe);
        //}
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AboutMe aboutMeImage = _repo.Get(id.Value);
        //    if (aboutMeImage == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(aboutMeImage);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var image = _repo.Get(id);

        //    //#region Delete Image
        //    //if (image.Image != null)
        //    //{
        //    //    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + image.Image)))
        //    //        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + image.Image));

        //    //    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + image.Image)))
        //    //        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + image.Image));
        //    //}
        //    //#endregion

        //    _repo.Delete(id);
        //    return RedirectToAction("Index");
        //}

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