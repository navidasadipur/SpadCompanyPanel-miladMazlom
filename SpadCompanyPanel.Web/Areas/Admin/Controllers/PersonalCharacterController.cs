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

        public ActionResult Index()
        {
            var personalCharacters = _repo.GetAll().OrderBy(ch => ch.Id);

            if (personalCharacters.Count() == 0)
            {
                return View();
            }

            var personalCharacterPanelView = new PersonalCharacterPanelView
            {
                TitleOne = personalCharacters.OrderBy(ch => ch.Id).FirstOrDefault().Title,
                TitleTwo = personalCharacters.OrderBy(ch => ch.Id).Skip(1).FirstOrDefault().Title,
                TitleThree = personalCharacters.OrderBy(ch => ch.Id).Skip(2).FirstOrDefault().Title,

                ShortDescriptionOne = personalCharacters.OrderBy(ch => ch.Id).FirstOrDefault().ShortDescription,
                ShortDescriptionTwo = personalCharacters.OrderBy(ch => ch.Id).Skip(1).FirstOrDefault().ShortDescription,
                ShortDescriptionThree = personalCharacters.OrderBy(ch => ch.Id).Skip(2).FirstOrDefault().ShortDescription,

                ImageOne = personalCharacters.OrderBy(ch => ch.Id).FirstOrDefault().Image,
                ImageTwo = personalCharacters.OrderBy(ch => ch.Id).Skip(1).FirstOrDefault().Image,
                ImageThree = personalCharacters.OrderBy(ch => ch.Id).Skip(2).FirstOrDefault().Image,

                ImageTitleOne = personalCharacters.OrderBy(ch => ch.Id).FirstOrDefault().ImageTitle,
                ImageTitleTwo = personalCharacters.OrderBy(ch => ch.Id).Skip(1).FirstOrDefault().ImageTitle,
                ImageTitleThree = personalCharacters.OrderBy(ch => ch.Id).Skip(2).FirstOrDefault().ImageTitle,
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

            return RedirectToAction("Index");
        }

        private void Update_AddPersonalCharInDb(string title, string shortDescription, PersonalCharacter personalChar)
        {
            if (personalChar == null)
            {
                personalChar = new PersonalCharacter()
                {
                    Title = title,
                    ShortDescription = shortDescription,
                };

                _repo.Add(personalChar);
            }
            else
            {
                personalChar.Title = title;
                personalChar.ShortDescription = shortDescription;

                _repo.Update(personalChar);
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
    }
}