using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fehteam.app.Controllers
{
    public class FehTeamController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult F7Check()
        {
            return View();
        }

        public ActionResult Report()
        {
            return View();
        }

        public JsonResult GetHeroes()
        {
            try
            {
                dal.UserData user = Bll.session.GetUserData(HttpContext);

                return Json(new { result = true, data = user }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AddHero()
        {
            try
            {
                if (!Bll.session.ExistUserData(HttpContext))
                    throw new Exception("Session Time out!");

                dal.UserData user = Bll.session.GetUserData(HttpContext);

                dal.UserData.Hero hero = CML.ComUtility.FromJson<dal.UserData.Hero>(Request["param"]);

                user.Add(hero);

                return Json(new { result = true, data = hero }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ModHero()
        {
            try
            {
                if (!Bll.session.ExistUserData(HttpContext))
                    throw new Exception("Session Time out!");

                dal.UserData user = Bll.session.GetUserData(HttpContext);

                dal.UserData.Hero hero = CML.ComUtility.FromJson<dal.UserData.Hero>(Request["param"]);

                dal.UserData.Hero mod = user.Mod(hero);

                if (mod == null)
                {
                    throw new Exception("Modify Fail: No Found Hero!");
                }

                return Json(new { result = true, data = mod }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
        public JsonResult ReportModHero()
        {
            try
            {
                if (!Bll.session.ExistUserData(HttpContext))
                    throw new Exception("Session Time out!");

                dal.UserData user = Bll.session.GetUserData(HttpContext);

                dal.UserData.Hero hero = CML.ComUtility.FromJson<dal.UserData.Hero>(Request["param"]);

                dal.UserData.Hero mod = user.Mod(hero);

                if (mod == null)
                {
                    throw new Exception("Modify Fail: No Found Hero!");
                }

                return Json(new
                {
                    result = true,
                    data = new
                    {
                        user = mod,
                        report = Bll.session.GetTre(mod)
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DelHero()
        {
            try
            {
                if (!Bll.session.ExistUserData(HttpContext))
                    throw new Exception("Session Time out!");

                dal.UserData user = Bll.session.GetUserData(HttpContext);

                dal.UserData.Hero del = user.Del(CML.ComUtility.XInt(Request["param"], 0));

                if (del == null)
                {
                    throw new Exception("Delete Fail: No Found Hero!");
                }

                return Json(new { result = true, data = del }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
        public JsonResult Save()
        {
            try
            {
                if (!Bll.session.ExistUserData(HttpContext))
                    throw new Exception("Session Time out!");

                dal.UserData user = Bll.session.GetUserData(HttpContext);

                string path = string.Empty;
                if (user.Key == string.Empty)
                {
                    do
                    {
                        user.Key = CML.ComUtility.UniqueID();
                        path = string.Format(@"{0}Users\{1}.data", Bll.webcache.DbPath, user.Key);
                    }
                    while (System.IO.File.Exists(path));
                }

                path = string.Format(@"{0}Users\{1}.data", Bll.webcache.DbPath, user.Key);
                CML.ComFile.Write(path, CML.ComUtility.ToJson(user));

                return Json(new { result = true, data = user.Key }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Load()
        {
            try
            {
                string key = CML.ComUtility.FromJson<string>(Request["param"]);
                string path = string.Format(@"{0}Users\{1}.data", Bll.webcache.DbPath, key);

                if (!System.IO.File.Exists(path))
                    throw new Exception("NO FOUND!");

                string content = CML.ComFile.Read(path);
                dal.UserData tmp = CML.ComUtility.FromJson<dal.UserData>(content);
                if (tmp == null)
                {
                    tmp = new dal.UserData();
                }

                tmp.Key = key;

                Bll.session.SetUserData(HttpContext, tmp);
                
                return Json(new { result = true, data = tmp.Key }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}