using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace fehteam.app.Bll
{
    public class webcache
    {
        public static string DbPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DbPath"];
            }
        }

        protected const string KEY_STORE = "CACHE.WIKI";

        public static fehteam.dal.Wiki wiki
        {
            get
            {
                if (MemoryCache.Default[KEY_STORE] == null)
                {
                    fehteam.dal.Wiki db = new dal.Wiki();
                    db.FromJson(CML.ComFile.Read(DbPath + "Wiki.data"));
                    MemoryCache.Default[KEY_STORE] = db;
                }
                return MemoryCache.Default[KEY_STORE] as fehteam.dal.Wiki;
            }
        }
    }
}