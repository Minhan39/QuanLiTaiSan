using QLTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLTS.Controllers
{
    public class AssetApiController : ApiController
    {
        [HttpGet]
        public AssetDetailModel Get(int id)
        {
            return AssetDetailHelper.GetAssetDetails().Find(n => n.DefinitionId == id);
        }
    }
}
