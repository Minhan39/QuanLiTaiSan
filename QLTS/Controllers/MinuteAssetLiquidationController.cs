using QLTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    public class MinuteAssetLiquidationController : BaseController
    {
        /*public ActionResult Index()
        {
            return View(MinuteAssetLiquidationHelper.GetMinuteAssetLiquidations());
        }
        public ActionResult GridViewDetailsPage(long id)
        {
            ViewBag.ShowBackButton = true;
            return View(MinuteAssetLiquidationHelper.GetMinuteAssetLiquidations().Find(i => i.Id == id));
        }
        public ActionResult GridViewPartial()
        {
            return PartialView("GridViewPartial", MinuteAssetLiquidationHelper.GetMinuteAssetLiquidations());
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
                SafeExecute(() => PerformDelete());
            return GridViewPartial();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewAddNewPartial(MinuteAssetLiquidationModel minuteAssetLiquidation)
        {
            return UpdateModelWithDataValidation(minuteAssetLiquidation, MinuteAssetLiquidationHelper.AddNewRecord);
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewUpdatePartial(MinuteAssetLiquidationModel minuteAssetLiquidation)
        {
            return UpdateModelWithDataValidation(minuteAssetLiquidation, MinuteAssetLiquidationHelper.UpdateRecord);
        }

        private ActionResult UpdateModelWithDataValidation(MinuteAssetLiquidationModel minuteAssetLiquidation, Action<MinuteAssetLiquidationModel> updateMethod)
        {
            if (ModelState.IsValid)
                SafeExecute(() => updateMethod(minuteAssetLiquidation));
            else
                ViewBag.GeneralError = "Ôi không, đã có lỗi xảy ra!";
            return GridViewPartial();
        }
        private void PerformDelete()
        {
            if (!string.IsNullOrEmpty(Request.Params["SelectedRows"]))
                AreaHelper.DeleteRecords(Request.Params["SelectedRows"]);
        }*/
    }
}