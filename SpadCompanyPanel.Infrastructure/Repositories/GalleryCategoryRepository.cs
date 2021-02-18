using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
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
