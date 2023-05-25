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
    public class MinuteAssetDeliveryReceiveController : BaseController
    {
        public ActionResult Index()
        {
            return View(MinuteAssetDeliveryReceiveHelper.GetMinuteAssetDeliveryReceives());
        }
        public ActionResult GridViewDetailsPage(long id)
        {
            ViewBag.ShowBackButton = true;
            return View(MinuteAssetDeliveryReceiveHelper.GetMinuteAssetDeliveryReceives().Find(i => i.Id == id));
        }
        public ActionResult GridViewPartial()
        {
            return PartialView("GridViewPartial", MinuteAssetDeliveryReceiveHelper.GetMinuteAssetDeliveryReceives());
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
                SafeExecute(() => PerformDelete());
            return GridViewPartial();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewAddNewPartial(MinuteAssetDeliveryReceiveModel minuteAssetDeliveryReceive)
        {
            minuteAssetDeliveryReceive.TypeOfMinutesId = 2;
            return UpdateModelWithDataValidation(minuteAssetDeliveryReceive, MinuteAssetDeliveryReceiveHelper.AddNewRecord);
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewUpdatePartial(MinuteAssetDeliveryReceiveModel minuteAssetDeliveryReceive)
        {
            return UpdateModelWithDataValidation(minuteAssetDeliveryReceive, MinuteAssetDeliveryReceiveHelper.UpdateRecord);
        }

        private ActionResult UpdateModelWithDataValidation(MinuteAssetDeliveryReceiveModel minuteAssetDeliveryReceive, Action<MinuteAssetDeliveryReceiveModel> updateMethod)
        {
            if (ModelState.IsValid)
                SafeExecute(() => updateMethod(minuteAssetDeliveryReceive));
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
                var excelData = new List<MinuteADRAssetDetailModel>();

                using (var stream = new MemoryStream(fileBytes))
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;
                        for (int i = 2; i <= rowCount; i++)
                        {
                            var data = new MinuteADRAssetDetailModel
                            {
                                /*Id = Convert.ToInt32(worksheet.Cells[i, 1].Value),*/
                                MinuteAssetDeliveryReceiveId = Convert.ToInt32(worksheet.Cells[i, 2].Value),
                                AssetDetailId = Convert.ToInt32(worksheet.Cells[i, 3].Value),
                                AssetStatusId = Convert.ToInt32(worksheet.Cells[i, 5].Value),
                                RoomId = Convert.ToInt32(worksheet.Cells[i, 4].Value),
                            };
                            excelData.Add(data);
                        }
                    }
                }

                MinuteADRAssetDetailHelper.AddNewRecords(excelData);
            }
            return RedirectToAction("Index");
        }

        public ActionResult MasterDetailDetailPartial(int Id)
        {
            ViewData["Id"] = Id;
            return PartialView("MasterDetailDetailPartial", MinuteADRAssetDetailHelper.GetList(Id));
        }
    }
}