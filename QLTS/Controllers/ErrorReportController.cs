using DevExpress.Web;
using DevExpress.Web.Mvc;
using QLTS.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    public class ErrorReportController : BaseController
    {
        // GET: DeviceErrorReport
        public ActionResult Index()
        {
            ErrorReportHelper.UpdateAdminReciver();
            return View(ErrorReportHelper.GetErrorReports());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]
        public ActionResult Report(ErrorReportModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                model.StaffId = StaffHelper.GetStaffs().Find(n => n.Email == User.Identity.Name).Id;
                if (ModelState.IsValid)
                {
                    SafeExecute(() => ErrorReportHelper.AddNewRecord(model));
                    var list = new List<ErrorReportModel>();
                    if (User.Identity.IsAuthenticated)
                    {
                        list = ErrorReportHelper.GetErrorReports().FirstOrDefault() != null ? ErrorReportHelper.GetErrorReports().Where(p => p.Staff.Email == "2024802010378@student.tdmu.edu.vn").Take(10).ToList() : list;
                    }
                    ViewBag.ListErrorReport = list;
                    return View();
                }
                return View(model);
            }
            return View(model);
        }
        [Authorize(Roles = "Employee")]
        [HttpGet]
        public ActionResult Report()
        {
            ErrorReportModel model = new ErrorReportModel();
            var list = new List<ErrorReportModel>();
            if (User.Identity.IsAuthenticated)
            {
                list = ErrorReportHelper.GetErrorReports().FirstOrDefault() != null ? ErrorReportHelper.GetErrorReports().Where(p => p.Staff.Email == User.Identity.Name).OrderByDescending(n => n.AtReport).Take(10).ToList() : list;
            }
            ViewBag.ListErrorReport = list;
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Employee")]
        public ActionResult Progress(int? reportId, string email)
        {
            if (reportId != null)
            {
                ErrorReportModel model = ErrorReportHelper.GetErrorReports().Find(n => n.Id == reportId);
                if (!String.IsNullOrEmpty(email) && model.Staff.Email == email)
                {
                    return View(model);
                }
                return RedirectToAction("Report");
            }
            return RedirectToAction("Report");
        }
        [HttpPost]
        [Authorize(Roles = "Employee")]
        public ActionResult Progress(FormCollection collection)
        {
            if (collection["reportId"] != null)
            {
                ErrorReportModel model = ErrorReportHelper.GetErrorReports().Find(n => n.Id == Convert.ToInt32(collection["reportId"]));
                if (!String.IsNullOrEmpty(collection["email"]) && model.Staff.Email == collection["email"])
                {
                    if (model.Step == 3)
                    {
                        model.Step = 4;
                        model.AtEnd = DateTime.Now;
                        SafeExecute(() => ErrorReportHelper.UpdateRecord(model));
                        return View(model);
                    }
                    return RedirectToAction("Report");
                }
                return RedirectToAction("Report");
            }
            return RedirectToAction("Report");
        }
        public ActionResult GridViewDetailsPage(long id)
        {
            ViewBag.ShowBackButton = true;
            return View(ErrorReportHelper.GetErrorReports().Find(i => i.Id == id));
        }
        public ActionResult GridViewPartial()
        {
            return PartialView("GridViewPartial", ErrorReportHelper.GetErrorReports());
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
                SafeExecute(() => PerformDelete());
            return GridViewPartial();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewUpdatePartial(ErrorReportModel errorReport)
        {
            return UpdateModelWithDataValidation(errorReport, ErrorReportHelper.UpdateRecord);
        }

        private ActionResult UpdateModelWithDataValidation(ErrorReportModel errorReport, Action<ErrorReportModel> updateMethod)
        {
            if (ModelState.IsValid)
                SafeExecute(() => updateMethod(errorReport));
            else
                ViewBag.GeneralError = "Ôi không, đã có lỗi xảy ra!";
            return GridViewPartial();
        }
        private void PerformDelete()
        {
            if (!string.IsNullOrEmpty(Request.Params["SelectedRows"]))
                ErrorReportHelper.DeleteRecords(Request.Params["SelectedRows"]);
        }
        public ActionResult BinaryImageColumnPhotoUpdate()
        {
            return BinaryImageEditExtension.GetCallbackResult();
        }
        public ActionResult ErrorImage()
        {
            if (Request.QueryString[ErrorReportHelper.ImageQueryKey] != null)
            {
                int id = int.Parse(Request.QueryString[ErrorReportHelper.ImageQueryKey]);
                Response.ContentType = "image";
                var photo = ErrorReportHelper.GetErrorPhoto(id);
                if (photo != null)
                    Response.BinaryWrite(photo.ToArray());
                Response.End();
            }
            return null;
        }
    }
}