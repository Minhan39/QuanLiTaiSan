using QLTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    public class SupplierController : BaseController
    {
        public ActionResult Index()
        {
            return View(SupplierHelper.GetSuppliers());
        }
        public ActionResult GridViewDetailsPage(long id)
        {
            ViewBag.ShowBackButton = true;
            return View(SupplierHelper.GetSuppliers().Find(i => i.Id == id));
        }
        public ActionResult GridViewPartial()
        {
            return PartialView("GridViewPartial", SupplierHelper.GetSuppliers());
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                if (!SupplierHelper.Validation(Request.Params["SelectedRows"]))
                {
                    return Json("Xoá thất bại, vui lòng xoá hết quan hệ với các tài sản liên quan", JsonRequestBehavior.AllowGet);
                }
                SafeExecute(() => PerformDelete());
            }
            return GridViewPartial();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewAddNewPartial(SupplierModel supplier)
        {
            return UpdateModelWithDataValidation(supplier, SupplierHelper.AddNewRecord);
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewUpdatePartial(SupplierModel supplier)
        {
            return UpdateModelWithDataValidation(supplier, SupplierHelper.UpdateRecord);
        }

        private ActionResult UpdateModelWithDataValidation(SupplierModel supplier, Action<SupplierModel> updateMethod)
        {
            if (ModelState.IsValid)
                SafeExecute(() => updateMethod(supplier));
            else
                ViewBag.GeneralError = "Ôi không, đã có lỗi xảy ra!";
            return GridViewPartial();
        }
        private void PerformDelete()
        {
            if (!string.IsNullOrEmpty(Request.Params["SelectedRows"]))
                SupplierHelper.DeleteRecords(Request.Params["SelectedRows"]);
        }
    }
}