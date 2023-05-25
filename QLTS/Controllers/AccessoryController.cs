using QLTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    public class AccessoryController : BaseController
    {
        public ActionResult Index()
        {
            return View(AccessoryHelper.GetAccessories());
        }
        public ActionResult GridViewDetailsPage(long id)
        {
            ViewBag.ShowBackButton = true;
            return View(AccessoryHelper.GetAccessories().Find(i => i.Id == id));
        }
        public ActionResult GridViewPartial()
        {
            return PartialView("GridViewPartial", AccessoryHelper.GetAccessories());
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
                SafeExecute(() => PerformDelete());
            return GridViewPartial();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewAddNewPartial(AccessoryModel accessory)
        {
            return UpdateModelWithDataValidation(accessory, AccessoryHelper.AddNewRecord);
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewUpdatePartial(AccessoryModel accessory)
        {
            return UpdateModelWithDataValidation(accessory, AccessoryHelper.UpdateRecord);
        }

        private ActionResult UpdateModelWithDataValidation(AccessoryModel accessory, Action<AccessoryModel> updateMethod)
        {
            if (ModelState.IsValid)
                SafeExecute(() => updateMethod(accessory));
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