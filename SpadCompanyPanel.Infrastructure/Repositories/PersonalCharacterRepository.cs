using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class PersonalCharacterRepository : BaseRepository<PersonalCharacter, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public PersonalCharacterRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public PersonalCharacter GetFirstPersonalCharacter()
        {
            return _context.PersonalCharacter.FirstOrDefault();
        }

        public PersonalCharacter GetSecondPersonalCharacter()
        {
            return _context.PersonalCharacter.OrderBy(c => c.Id).Skip(1).FirstOrDefault();
        }

        public PersonalCharacter GetThirdPersonalCharacter()
        {
            return _context.PersonalCharacter.OrderBy(c => c.Id).Skip(2).FirstOrDefault();
        }

        public List<PersonalCharacter> GetAllPersonalCharacters()
        {
            return _context.PersonalCharacter.Where(ch => ch.IsDeleted == false).OrderBy(ch => ch.Id).ToList();
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
