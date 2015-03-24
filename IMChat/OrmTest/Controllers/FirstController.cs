using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrmTest.Controllers
{
    public class FirstController : Controller
    {
        //
        // GET: /First/

        public ActionResult Index()
        {
            MyTestService.testSoapClient Instance = new MyTestService.testSoapClient();
            double sum = Instance.Add(1, 3.4);
            ViewBag.A = sum;
            return View();
        }

    }
}
