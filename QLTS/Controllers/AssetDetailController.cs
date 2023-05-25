using QLTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    public class AssetDetailController : BaseController
    {
        public ActionResult Index()
        {
            return View(AssetDetailHelper.GetAssetDetails());
        }
        public ActionResult GridViewDetailsPage(long id)
        {
            ViewBag.ShowBackButton = true;
            return View(AssetDetailHelper.GetAssetDetails().Find(i => i.Id == id));
        }
        public ActionResult GridViewPartial()
        {
            return PartialView("GridViewPartial", AssetDetailHelper.GetAssetDetails());
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
                SafeExecute(() => PerformDelete());
            return GridViewPartial();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewAddNewPartial(AssetDetailModel assetDetail)
        {
            return UpdateModelWithDataValidation(assetDetail, AssetDetailHelper.AddNewRecord);
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewUpdatePartial(AssetDetailModel assetDetail)
        {
            return UpdateModelWithDataValidation(assetDetail, AssetDetailHelper.UpdateRecord);
        }

        private ActionResult UpdateModelWithDataValidation(AssetDetailModel assetDetail, Action<AssetDetailModel> updateMethod)
        {
            if (ModelState.IsValid)
                SafeExecute(() => updateMethod(assetDetail));
            else
                ViewBag.GeneralError = "Ôi không, đã có lỗi xảy ra!";
            return GridViewPartial();
        }
        private void PerformDelete()
        {
            if (!string.IsNullOrEmpty(Request.Params["SelectedRows"]))
                AreaHelper.DeleteRecords(Request.Params["SelectedRows"]);
        }
    }
}