using System;
using System.Net;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Repositories;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class GalleryCategoryController : Controller
    {
        private readonly GalleryCategoryRepository _repo;
        public GalleryCategoryController(GalleryCategoryRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/GalleryCategory
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: Admin/GalleryCategory/Create
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] GalleryCategory GalleryCategory)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(GalleryCategory);
                return RedirectToAction("Index");
            }

            return View(GalleryCategory);
        }

        // GET: Admin/GalleryCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryCategory GalleryCategory = _repo.Get(id.Value);
            if (GalleryCategory == null)
            {
                return HttpNotFound();
            }
            return PartialView(GalleryCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] GalleryCategory GalleryCategory)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(GalleryCategory);
                return RedirectToAction("Index");
            }
            return View(GalleryCategory);
        }

        // GET: Admin/GalleryCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryCategory GalleryCategory = _repo.Get(id.Value);
            if (GalleryCategory == null)
            {
                return HttpNotFound();
            }
            return PartialView(GalleryCategory);
        }

        // POST: Admin/GalleryCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
