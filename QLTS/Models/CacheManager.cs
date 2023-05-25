using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    public class CacheManager
    {
        public static void ResetCache()
        {
            if (HttpContext.Current.Session != null)
                HttpContext.Current.Session["UniqueCacheParam"] = Guid.NewGuid();
        }
        public static Guid UniqueCacheParam
        {
            get
            {
                if (HttpContext.Current.Session == null)
                    return Guid.Empty;
                else
                {
                    if (HttpContext.Current.Session["UniqueCacheParam"] == null)
                        ResetCache();
                    return (Guid)HttpContext.Current.Session["UniqueCacheParam"];
                }
            }
        }
    }
}