using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("ErrorReport")]
    public class ErrorReportModel
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public int AssetDetailId { get; set; }
        public int? RoomId { get; set; }
        public string Message { get; set; }
        public byte[] Image { get; set; }
        public int Step { get; set; }
        public DateTime AtReport { get; set; }
        public DateTime? AtReciver { get; set; }
        public DateTime? AtRepair { get; set; }
        public DateTime? AtEnd { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
        public virtual StaffModel Staff { get; set; }
        public virtual AssetDetailModel AssetDetail { get; set; }
        public virtual RoomModel Room { get; set; }
    }

    public class ErrorReportHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public const string ImageQueryKey = "DXImage";
        public static List<ErrorReportModel> GetErrorReports()
        {
            return db.ErrorReports.OrderByDescending(n => n.AtReport).ToList();
        }
        public static void UpdateAdminReciver()
        {
            DateTime now = DateTime.Now;
            if (db.ErrorReports.FirstOrDefault() != null)
            {
                db.ErrorReports.Where(n => n.Step < 2).ToList().ForEach(n => { n.Step = 2; n.AtReciver = now; });
                db.SaveChanges();
            }
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        public static void AddNewRecord(ErrorReportModel errorReport)
        {
            AssetDetailModel model = db.AssetDetails.First(n => n.DefinitionId == errorReport.AssetDetailId);
            errorReport.AssetDetailId = model.Id;
            errorReport.Step = 1;
            errorReport.AtCreate = DateTime.Now;
            errorReport.AtReport = DateTime.Now;
            db.ErrorReports.Add(errorReport);
            db.SaveChanges();
        }

        public static void UpdateRecord(ErrorReportModel errorReport)
        {
            ErrorReportModel item = db.ErrorReports.Find(errorReport.Id);
            item.AssetDetailId = errorReport.AssetDetailId;
            item.RoomId = errorReport.RoomId;
            item.AtRepair = errorReport.AtRepair;
            if (item.Step == 2)
            {
                item.Step = item.AtRepair != null ? 3 : 2;
            }
            else
            if (item.Step == 3)
            {
                item.Step = errorReport.Step == 4 ? 4 : 3;
                item.AtEnd = errorReport.AtEnd;
            }
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<ErrorReportModel> errorReports = GetErrorReports().Where(i => selectedIds.Contains(i.Id));
            db.ErrorReports.RemoveRange(errorReports);
            db.SaveChanges();
        }
        public static string GetErrorImageRouteUrl()
        {
            return DevExpressHelper.GetUrl(new
            {
                Controller = "ErrorReport",
                Action = "ErrorImage"
            });
        }
        public static Binary GetErrorPhoto(int id)
        {
            return (from error in db.ErrorReports
                    where error.Id == id
                    select error.Image).SingleOrDefault();
        }
    }
}