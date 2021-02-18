using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class CharacterRepository : BaseRepository<Character, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public CharacterRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        //public AboutMe GetAll()
        //{
        //    return _context.Character.GetAll(c => c.IsDeleted == false);
        //}

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
