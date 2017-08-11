using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fehteam.wiki
{
    public class QueryHeroIcon
    {
        public class Query
        {
            public class Page
            {
                public class ImageInfo
                {
                    public string url = string.Empty;
                }

                public List<ImageInfo> imageinfo = new List<ImageInfo>();

            }

            public Dictionary<string, Page> pages = new Dictionary<string, Page>();
        }

        public Query query = new Query();
    }
}
