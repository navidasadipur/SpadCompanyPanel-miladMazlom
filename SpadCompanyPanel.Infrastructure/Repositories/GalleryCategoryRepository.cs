using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class GalleryCategoryRepository : BaseRepository<GalleryCategory, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public GalleryCategoryRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<GalleryCategory> GetAllGalleryCategories()
        {
            var allCategories = _context.GalleryCategories.Where(a => a.IsDeleted == false).OrderByDescending(a => a.InsertDate).ToList();

            foreach (var category in allCategories)
            {
                var categoryGalleies = _context.Galleries.Where(p => p.GalleryCategoryId == category.Id & p.IsDeleted == false).ToList();
                category.Galleries.Concat(categoryGalleies);
            }

            return allCategories;
        }

        //public List<GalleryCategory> GetGalleryCategories()
        //{
        //    return _context. /* Galleries.Where(a => a.IsDeleted == false).ToList();*/
        //}


        //public Cover GetFirstCover()
        //{
        //    return _context.Covers.FirstOrDefault(c => c.IsDeleted == false);
        //}

        //public string GetArticleName(int articleId)
        //{
        //    return _context.Articles.Find(articleId).Title;
        //}
    }
}
