using System;
using System.Net;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Repositories;
using System.Web;
using System.IO;
using SpadCompanyPanel.Infrastructure.Helpers;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly GalleriesRepository _repo;
        private readonly GalleryCategoryRepository _categoryRepo;
        public GalleryController(GalleriesRepository repo, GalleryCategoryRepository categoryRepo)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
        }
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "GalleryCategory");
            }
            var allCategories = _repo.getGalleriesByCategoryId(id.Value);

            ViewBag.CategoryTitle = _categoryRepo.Get(id.Value).Title;
            ViewBag.CategoryId = id;

            return View(allCategories);
        }

        public ActionResult Create(int? id)
        {
            //ViewBag.GalleryCategoryId = new SelectList(_categoryRepo.GetAll(), "Id", "Title");
            ViewBag.CategoryTitle = _categoryRepo.Get(id.Value).Title;

            var model = new Gallery
            {
                GalleryCategoryId = id,
                GalleryCategory = _categoryRepo.Get(id.Value)
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gallery image,HttpPostedFileBase GalleryImage)
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
                    ImageResizer imageCut = new ImageResizer(1200,1200,true);

                    imageCut.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
                        Server.MapPath("/Files/GalleryImages/" + newFileName));

                    ImageResizer thumb = new ImageResizer(600, 600, true);

                    thumb.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
                        Server.MapPath("/Files/GalleryImages/Thumb/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));
                    image.Image = newFileName;
                }
                #endregion

                _repo.Add(image);
                return RedirectToAction("Index", new { id = image.GalleryCategoryId });
            }

            return View(image);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery image = _repo.Get(id.Value);
            if (image == null)
            {
                return HttpNotFound();
            }

            //ViewBag.GalleryCategoryId = new SelectList(_categoryRepo.GetAll(), "Id", "Title");
            ViewBag.CategoryTitle = _categoryRepo.Get(image.GalleryCategoryId.Value).Title;

            return PartialView(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gallery gallery, HttpPostedFileBase GalleryImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (GalleryImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + gallery.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + gallery.Image));

                    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/Thumb/" + gallery.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Thumb/" + gallery.Image));

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
                    gallery.Image = newFileName;
                }
                #endregion

                _repo.Update(gallery);
                return RedirectToAction("Index", new { id = gallery.GalleryCategoryId });
            }
            return View(gallery);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery image = _repo.Get(id.Value);
            if (image == null)
            {
                return HttpNotFound();
            }
            return PartialView(image);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var image = _repo.Get(id);

            _repo.Delete(id);

            return RedirectToAction("Index", new { id = image.GalleryCategoryId });
        }
    }
}