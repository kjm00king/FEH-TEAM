using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace fehteam.app.Controllers
{
    public class BackendController : Controller
    {
        private static readonly string API_URL = "https://feheroes.gamepedia.com/api.php";
        private static readonly string GET_HEROES = "action=ask&query=[[Category:Heroes]]&format=json";

        public ActionResult UpdateWikiData()
        {
            try
            {
                var responseString = CML.ComUrl.Get(API_URL, GET_HEROES);

                return Content(responseString);
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }
        
    }
}