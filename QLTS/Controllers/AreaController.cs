using QLTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    public class AreaController : BaseController
    {
        public ActionResult Index()
        {
            return View(AreaHelper.GetAreas());
        }
        public ActionResult GridViewDetailsPage(long id)
        {
            ViewBag.ShowBackButton = true;
            return View(AreaHelper.GetAreas().Find(i => i.Id == id));
        }
        public ActionResult GridViewPartial()
        {
            return PartialView("GridViewPartial", AreaHelper.GetAreas());
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
                SafeExecute(() => PerformDelete());
            return GridViewPartial();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewAddNewPartial(AreaModel area)
        {
            return UpdateModelWithDataValidation(area, AreaHelper.AddNewRecord);
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewUpdatePartial(AreaModel area)
        {
            return UpdateModelWithDataValidation(area, AreaHelper.UpdateRecord);
        }

        private ActionResult UpdateModelWithDataValidation(AreaModel area, Action<AreaModel> updateMethod)
        {
            if (ModelState.IsValid)
                SafeExecute(() => updateMethod(area));
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