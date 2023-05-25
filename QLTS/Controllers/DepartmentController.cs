using QLTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    public class DepartmentController : BaseController
    {
        public ActionResult Index()
        {
            return View(DepartmentHelper.GetDepartments());
        }
        public ActionResult GridViewDetailsPage(long id)
        {
            ViewBag.ShowBackButton = true;
            return View(DepartmentHelper.GetDepartments().Find(i => i.Id == id));
        }
        public ActionResult GridViewPartial()
        {
            return PartialView("GridViewPartial", DepartmentHelper.GetDepartments());
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
                SafeExecute(() => PerformDelete());
            return GridViewPartial();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewAddNewPartial(DepartmentModel department)
        {
            return UpdateModelWithDataValidation(department, DepartmentHelper.AddNewRecord);
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewUpdatePartial(DepartmentModel department)
        {
            return UpdateModelWithDataValidation(department, DepartmentHelper.UpdateRecord);
        }

        private ActionResult UpdateModelWithDataValidation(DepartmentModel department, Action<DepartmentModel> updateMethod)
        {
            if (ModelState.IsValid)
                SafeExecute(() => updateMethod(department));
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