using System;
using System.Net;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Repositories;
using System.Web;
using System.IO;
using System.Threading;
using SpadCompanyPanel.Infrastructure.Helpers;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class GalleryVideosController : Controller
    {
        private readonly GalleryVideosRepository _repo;
        public GalleryVideosController(GalleryVideosRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }
        public ActionResult Create()
        {
            TempData["UploadedFile"] = null;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GalleryVideo video, HttpPostedFileBase GalleryVideo, HttpPostedFileBase GalleryImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Video
                if (GalleryVideo != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(GalleryVideo.FileName);
                    GalleryVideo.SaveAs(Server.MapPath("/Files/GalleryVideos/" + newFileName));
                    video.Video = newFileName;
                }
                #endregion
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

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));
                    video.Image = newFileName;
                }
                #endregion
                _repo.Add(video);
                return RedirectToAction("Index");
            }

            return View(video);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryVideo video = _repo.Get(id.Value);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GalleryVideo gallery, HttpPostedFileBase GalleryVideo, HttpPostedFileBase GalleryImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Video
                if (GalleryVideo != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryVideos/" + gallery.Video)))
                        System.IO.File.Delete(Server.MapPath("/Files/GalleryVideos/" + gallery.Video));

                    var newFileName = Guid.NewGuid() + Path.GetExtension(GalleryVideo.FileName);
                    GalleryVideo.SaveAs(Server.MapPath("/Files/GalleryVideos/" + newFileName));
                    gallery.Video = newFileName;
                }
                #endregion
                #region Upload Image
                if (GalleryImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + gallery.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + gallery.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(GalleryImage.FileName);
                    GalleryImage.SaveAs(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));

                    // Resizing Image
                    ImageResizer imageCut = new ImageResizer(1200, 1200, true);

                    imageCut.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
                        Server.MapPath("/Files/GalleryImages/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));
                    gallery.Image = newFileName;
                }
                #endregion
                _repo.Update(gallery);
                return RedirectToAction("Index");
            }
            return View(gallery);
        }
        [HttpPost]
        public ActionResult FileUpload()
        {
            var files = HttpContext.Request.Files;
            foreach (var fileName in files)
            {
                HttpPostedFileBase file = Request.Files[fileName.ToString()];
                var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("/Files/GalleryVideos/" + newFileName));
                TempData["UploadedFile"] = newFileName;
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryVideo video = _repo.Get(id.Value);
            if (video == null)
            {
                return HttpNotFound();
            }
            return PartialView(video);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var video = _repo.Get(id);

            #region Delete Video
            if (video.Video != null)
            {
                if (System.IO.File.Exists(Server.MapPath("/Files/GalleryVideos/" + video.Video)))
                    System.IO.File.Delete(Server.MapPath("/Files/GalleryVideos/" + video.Video));

                if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + video.Image)))
                    System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + video.Image));
            }
            #endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}