using QLTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    public class MinuteAssetInventoryController : BaseController
    {
        /*public ActionResult Index()
        {
            return View(MinuteAssetInventoryHelper.GetMinuteAssetInventories());
        }
        public ActionResult GridViewDetailsPage(long id)
        {
            ViewBag.ShowBackButton = true;
            return View(MinuteAssetInventoryHelper.GetMinuteAssetInventories().Find(i => i.Id == id));
        }
        public ActionResult GridViewPartial()
        {
            return PartialView("GridViewPartial", MinuteAssetInventoryHelper.GetMinuteAssetInventories());
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
                SafeExecute(() => PerformDelete());
            return GridViewPartial();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewAddNewPartial(MinuteAssetInventoryModel minuteAssetInventory)
        {
            return UpdateModelWithDataValidation(minuteAssetInventory, MinuteAssetInventoryHelper.AddNewRecord);
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewUpdatePartial(MinuteAssetInventoryModel minuteAssetInventory)
        {
            return UpdateModelWithDataValidation(minuteAssetInventory, MinuteAssetInventoryHelper.UpdateRecord);
        }

        private ActionResult UpdateModelWithDataValidation(MinuteAssetInventoryModel minuteAssetInventory, Action<MinuteAssetInventoryModel> updateMethod)
        {
            if (ModelState.IsValid)
                SafeExecute(() => updateMethod(minuteAssetInventory));
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