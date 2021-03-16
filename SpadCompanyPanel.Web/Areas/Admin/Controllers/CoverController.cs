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
    public class CoverController : Controller
    {
        private readonly CoverRepository _repo;
        public CoverController(CoverRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            var coverModel = new Cover();

            //this table has one row
            coverModel = _repo.GetFirstCover();

            return View(coverModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Cover model)
        {
            //this table has one row
            var existingcoverModel = _repo.GetFirstCover();

            if (existingcoverModel == null)
            {
                _repo.Add(model);

                return View(model);
            }

            existingcoverModel.Title = model.Title;
            existingcoverModel.SubTitle = model.SubTitle;

            _repo.Update(existingcoverModel);

            return View(existingcoverModel);
        }


        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cover coverImage, HttpPostedFileBase GalleryImage)
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
                    ImageResizer imageCut = new ImageResizer(2000, 2000, true);

                    imageCut.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
                        Server.MapPath("/Files/GalleryImages/" + newFileName));

                    ImageResizer thumb = new ImageResizer(600, 600, true);

                    thumb.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
                        Server.MapPath("/Files/GalleryImages/Thumb/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));
                    coverImage.Image = newFileName;
                }
                #endregion

                var cover = _repo.GetFirstCover();

                if (cover == null)
                {
                    cover = new Cover();
                }

                cover.Image = coverImage.Image;
                cover.ImageTitle = coverImage.ImageTitle;

                _repo.Update(cover);

                //_repo.Add(coverImage);

                return RedirectToAction("Index");
            }

            //ToDo
            return RedirectToAction("Index")/*View(coverImage)*/;
        }

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Cover coverImage = _repo.Get(id.Value);
        //    if (coverImage == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(coverImage);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Cover gallery, HttpPostedFileBase GalleryImage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        #region Upload Image
        //        if (GalleryImage != null)
        //        {
        //            if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + gallery.Image)))
        //                System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + gallery.Image));

        //            if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/Thumb/" + gallery.Image)))
        //                System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Thumb/" + gallery.Image));

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
        //            gallery.Image = newFileName;
        //        }
        //        #endregion

        //        _repo.Update(gallery);
        //        return RedirectToAction("Index");
        //    }
        //    return View(gallery);
        //}
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Cover image = _repo.Get(id.Value);
        //    if (image == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(image);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var image = _repo.Get(id);


        //    _repo.Delete(id);
        //    return RedirectToAction("Index");
        //}

    }
}