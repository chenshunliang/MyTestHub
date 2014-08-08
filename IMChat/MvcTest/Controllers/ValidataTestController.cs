using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTest.Models;

namespace MvcTest.Controllers
{
    public class ValidataTestController : Controller
    {
        //
        // GET: /ValidataTest/

        [HttpGet]
        public ActionResult Index()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult Index(Person per)
        {
            Validate(per);

            if (!ModelState.IsValid)
                return View(per);
            else
                return Content("数据验证通过");
        }

        private void Validate(Person per)
        {
            if (string.IsNullOrEmpty(per.Name))
                ModelState.AddModelError("Name", "name字段必填");
        }

    }
}
