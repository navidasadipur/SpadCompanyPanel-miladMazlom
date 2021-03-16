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
    public class MyContactInfoController : Controller
    {
        private readonly MyContactInfoRepository _repo;
        public MyContactInfoController(MyContactInfoRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            var model = new MyContactInfo();

            //this table has one row
            model = _repo.GetFirstMyContactInfo();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MyContactInfo model)
        {
            //this table has one row
            var existingMyContactInfoModel = _repo.GetFirstMyContactInfo();

            if (existingMyContactInfoModel == null)
            {
                _repo.Add(model);

                return View(model);
            }

            existingMyContactInfoModel.Address = model.Address;
            existingMyContactInfoModel.Email = model.Email;
            existingMyContactInfoModel.PhoneNumber = model.PhoneNumber;

            _repo.Update(existingMyContactInfoModel);

            return View(existingMyContactInfoModel);
        }

    }
}