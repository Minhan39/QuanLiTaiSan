using OfficeOpenXml;
using QLTS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    public class MinuteEnterAssetAtBeginController : BaseController
    {
        public ActionResult Index()
        {
            return View(MinuteEnterAssetAtBeginHelper.GetMinuteEnterAssetAtBegins());
        }
        public ActionResult GridViewDetailsPage(long id)
        {
            ViewBag.ShowBackButton = true;
            return View(MinuteEnterAssetAtBeginHelper.GetMinuteEnterAssetAtBegins().Find(i => i.Id == id));
        }
        public ActionResult GridViewPartial()
        {
            return PartialView("GridViewPartial", MinuteEnterAssetAtBeginHelper.GetMinuteEnterAssetAtBegins());
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
                SafeExecute(() => PerformDelete());
            return GridViewPartial();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewAddNewPartial(MinuteEnterAssetAtBeginModel minuteEnterAssetAtBegin)
        {
            return UpdateModelWithDataValidation(minuteEnterAssetAtBegin, MinuteEnterAssetAtBeginHelper.AddNewRecord);
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewUpdatePartial(MinuteEnterAssetAtBeginModel minuteEnterAssetAtBegin)
        {
            return UpdateModelWithDataValidation(minuteEnterAssetAtBegin, MinuteEnterAssetAtBeginHelper.UpdateRecord);
        }

        private ActionResult UpdateModelWithDataValidation(MinuteEnterAssetAtBeginModel minuteEnterAssetAtBegin, Action<MinuteEnterAssetAtBeginModel> updateMethod)
        {
            if (ModelState.IsValid)
                SafeExecute(() => updateMethod(minuteEnterAssetAtBegin));
            else
                ViewBag.GeneralError = "Ôi không, đã có lỗi xảy ra!";
            return GridViewPartial();
        }
        private void PerformDelete()
        {
            if (!string.IsNullOrEmpty(Request.Params["SelectedRows"]))
                AreaHelper.DeleteRecords(Request.Params["SelectedRows"]);
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string fileContentType = file.ContentType;
                byte[] fileBytes = new byte[file.ContentLength];
                file.InputStream.Read(fileBytes, 0, file.ContentLength);
                var excelData = new List<MinuteEABAssetDetailModel>();

                using (var stream = new MemoryStream(fileBytes))
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;
                        for (int i = 2; i <= rowCount; i++)
                        {
                            var data = new MinuteEABAssetDetailModel
                            {
                                /*Id = Convert.ToInt32(worksheet.Cells[i, 1].Value),*/
                                MinuteEnterAssetAtBeginId = Convert.ToInt32(worksheet.Cells[i, 2].Value),
                                AssetDetailId = Convert.ToInt32(worksheet.Cells[i, 3].Value),
                                AssetStatusId = Convert.ToInt32(worksheet.Cells[i, 5].Value),
                                RoomId = Convert.ToInt32(worksheet.Cells[i, 4].Value),
                            };
                            excelData.Add(data);
                        }
                    }
                }

                MinuteEABAssetDetailHelper.AddNewRecords(excelData);
            }
            return RedirectToAction("Index");
        }

        public ActionResult MasterDetailDetailPartial(int Id)
        {
            ViewData["Id"] = Id;
            return PartialView("MasterDetailDetailPartial", MinuteEABAssetDetailHelper.GetList(Id));
        }
    }
}