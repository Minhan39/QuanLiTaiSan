using DevExpress.Web.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using QLTS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    public class AssetController : BaseController
    {
        public ActionResult Index()
        {
            AutoUpdateAssetDetailHelper.AutoUpdate();
            ViewBag.GetAllKeyMaster = AssetHelper.GetAssets().Select(n => n.Id).ToArray();
            return View(AssetHelper.GetAssets());
        }
        [AllowAnonymous]
        public ActionResult JsonData(int Id)
        {
            var list = AssetDetailHelper.GetAssetDetails(Id);
            return Content(JsonConvert.SerializeObject(list), "application/json");
        }
        public ActionResult GridViewDetailsPage(long id)
        {
            ViewBag.ShowBackButton = true;
            return View(AssetHelper.GetAssets().Find(i => i.Id == id));
        }
        public ActionResult GridViewPartial()
        {
            return PartialView("GridViewPartial", AssetHelper.GetAssets());
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
                SafeExecute(() => PerformDelete());
            return GridViewPartial();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewAddNewPartial(AssetModel asset)
        {
            return UpdateModelWithDataValidation(asset, AssetHelper.AddNewRecord);
        }
        [ValidateAntiForgeryToken]
        public ActionResult GridViewUpdatePartial(AssetModel asset)
        {
            return UpdateModelWithDataValidation(asset, AssetHelper.UpdateRecord);
        }

        private ActionResult UpdateModelWithDataValidation(AssetModel asset, Action<AssetModel> updateMethod)
        {
            if (ModelState.IsValid)
                SafeExecute(() => updateMethod(asset));
            else
                ViewBag.GeneralError = "Ôi không, đã có lỗi xảy ra!";
            return GridViewPartial();
        }
        private void PerformDelete()
        {
            if (!string.IsNullOrEmpty(Request.Params["SelectedRows"]))
                AssetHelper.DeleteRecords(Request.Params["SelectedRows"]);
        }
        public ActionResult BinaryImageColumnPhotoUpdate()
        {
            return BinaryImageEditExtension.GetCallbackResult();
        }

        public ActionResult PrintQr(string selectId)
        {
            ViewBag.AssetId = selectId.Split(',').ToList();
            return View();
        }

        public ActionResult GetImage()
        {
            if (Request.QueryString[AssetHelper.ImageQueryKey] != null)
            {
                int id = int.Parse(Request.QueryString[AssetHelper.ImageQueryKey]);
                Response.ContentType = "image";
                var photo = AssetHelper.GetPhoto(id);
                if (photo != null)
                    Response.BinaryWrite(photo.ToArray());
                Response.End();
            }
            return null;
        }

        public ActionResult MasterDetailDetailPartial(int Id)
        {
            ViewData["Id"] = Id;
            var list = AssetDetailHelper.GetAssetDetails(Id);
            return PartialView("MasterDetailDetailPartial", list);
        }

        public ActionResult DownloadXlsx(string fileName)
        {
            string fileFullPath = Server.MapPath("~/Content/Files/" + fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fileFullPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
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
                var excelData = new List<AssetDetailModel>();

                using (var stream = new MemoryStream(fileBytes))
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;
                        for (int i = 2; i <= rowCount; i++)
                        {
                            var data = new AssetDetailModel
                            {
                                /*Id = Convert.ToInt32(worksheet.Cells[i, 1].Value),*/
                                AssetId = Convert.ToInt32(worksheet.Cells[i, 2].Value),
                                DefinitionId = Convert.ToInt32(worksheet.Cells[i, 3].Value),
                                AssetStatusId = Convert.ToInt32(worksheet.Cells[i, 4].Value),
                                RoomId = Convert.ToInt32(worksheet.Cells[i, 5].Value),
                                Value = Convert.ToDecimal(worksheet.Cells[i, 6].Value)
                            };
                            excelData.Add(data);
                        }
                    }
                }

                AssetDetailHelper.AddNewRecords(excelData);
            }
            return RedirectToAction("Index");
        }
    }
}