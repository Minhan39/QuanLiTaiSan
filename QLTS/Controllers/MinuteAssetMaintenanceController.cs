using QLTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    public class MinuteAssetMaintenanceController : BaseController
    {
        /*public ActionResult Index()
        {
            return View(MinuteAssetMaintenanceHelper.GetMinuteAssetMaintenances());
        }
        public ActionResult GridViewDetailsPage(long id)
        {
            ViewBag.ShowBackButton = true;
            return View(MinuteAssetMaintenanceHelper.GetMinuteAssetMaintenances().Find(i => i.Id == id));
        }
        public ActionResult GridViewPartial()
        {
            return PartialView("GridViewPartial", MinuteAssetMaintenanceHelper.GetMinuteAssetMaintenances());
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
                SafeExecute(() => PerformDelete());
            return GridViewPartial();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewAddNewPartial(MinuteAssetMaintenanceModel minuteAssetMaintenance)
        {
            return UpdateModelWithDataValidation(minuteAssetMaintenance, MinuteAssetMaintenanceHelper.AddNewRecord);
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewUpdatePartial(MinuteAssetMaintenanceModel minuteAssetMaintenance)
        {
            return UpdateModelWithDataValidation(minuteAssetMaintenance, MinuteAssetMaintenanceHelper.UpdateRecord);
        }

        private ActionResult UpdateModelWithDataValidation(MinuteAssetMaintenanceModel minuteAssetMaintenance, Action<MinuteAssetMaintenanceModel> updateMethod)
        {
            if (ModelState.IsValid)
                SafeExecute(() => updateMethod(minuteAssetMaintenance));
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