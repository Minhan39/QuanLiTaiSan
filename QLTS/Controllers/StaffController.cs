using DevExpress.Web.Mvc;
using QLTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    public class StaffController : BaseController
    {
        public ActionResult Index()
        {
            return View(StaffHelper.GetStaffs());
        }
        public ActionResult GridViewDetailsPage(long id)
        {
            ViewBag.ShowBackButton = true;
            return View(StaffHelper.GetStaffs().Find(i => i.Id == id));
        }
        public ActionResult GridViewPartial()
        {
            return PartialView("GridViewPartial", StaffHelper.GetStaffs());
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
                SafeExecute(() => PerformDelete());
            return GridViewPartial();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewAddNewPartial(StaffModel staff)
        {
            return UpdateModelWithDataValidation(staff, StaffHelper.AddNewRecord);
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewUpdatePartial(StaffModel staff)
        {
            return UpdateModelWithDataValidation(staff, StaffHelper.UpdateRecord);
        }

        private ActionResult UpdateModelWithDataValidation(StaffModel staff, Action<StaffModel> updateMethod)
        {
            if (ModelState.IsValid)
                SafeExecute(() => updateMethod(staff));
            else
                ViewBag.GeneralError = "Ôi không, đã có lỗi xảy ra!";
            return GridViewPartial();
        }
        private void PerformDelete()
        {
            if (!string.IsNullOrEmpty(Request.Params["SelectedRows"]))
                StaffHelper.DeleteRecords(Request.Params["SelectedRows"]);
        }
        public ActionResult BinaryImageColumnPhotoUpdate()
        {
            return BinaryImageEditExtension.GetCallbackResult();
        }
    }
}