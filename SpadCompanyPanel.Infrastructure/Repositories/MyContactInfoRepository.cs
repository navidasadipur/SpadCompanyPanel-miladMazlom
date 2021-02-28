using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class MyContactInfoRepository : BaseRepository<MyContactInfo, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public MyContactInfoRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public MyContactInfo GetFirstMyContactInfo()
        {
            return _context.MyContactInfos.FirstOrDefault(c => c.IsDeleted == false);
        }

        //public List<Cover> GetCover(int coverId)
        //{
        //    return _context.Covers.Where(c => c.Id == coverId & c.IsDeleted == false).ToList();
        //}

        //public string GetArticleName(int articleId)
        //{
        //    return _context.Articles.Find(articleId).Title;
        //}
    }
}
