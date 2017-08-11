using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fehteam.app.Controllers
{
    public class YuTuApiController : Controller
    {
        // GET: TuTuApi
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload(FormCollection content)
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase c = Request.Files[0];
                if (c == null || c.ContentLength == 0)
                {
                    MemoryStream ms = (c.InputStream) as MemoryStream;

                    Convert.ToBase64String(ms.ToArray());

                    return View();
                }
            }

            return View();
        }
    }
}