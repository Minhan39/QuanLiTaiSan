using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("MinuteAssetMaintenance")]
    public class MinuteAssetMaintenanceModel
    {
        public int Id { get; set; }
        [Required]
        public int MinuteId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public decimal Cost { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
        /*public virtual MinuteModel Minute { get; set; }*/
        public virtual DepartmentModel Department { get; set; }
    }

    public class MinuteAssetMaintenanceHelper
    {
        /*private static QLTSDbContext db = new QLTSDbContext();
        public static List<MinuteAssetMaintenanceModel> GetMinuteAssetMaintenances()
        {
            return db.MinuteAssetMaintenances.OrderByDescending(i => i.AtCreate).ToList();
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        public static void AddNewRecord(MinuteAssetMaintenanceModel minuteAssetMaintenance)
        {
            minuteAssetMaintenance.AtCreate = DateTime.Now;
            db.MinuteAssetMaintenances.Add(minuteAssetMaintenance);
            db.SaveChanges();
        }

        public static void UpdateRecord(MinuteAssetMaintenanceModel minuteAssetMaintenance)
        {
            MinuteAssetMaintenanceModel item = db.MinuteAssetMaintenances.Find(minuteAssetMaintenance.Id);
            item.MinuteId = minuteAssetMaintenance.MinuteId;
            item.DepartmentId = minuteAssetMaintenance.DepartmentId;
            item.Cost = minuteAssetMaintenance.Cost;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<MinuteAssetMaintenanceModel> minuteAssetMaintenances = GetMinuteAssetMaintenances().Where(i => selectedIds.Contains(i.Id));
            db.MinuteAssetMaintenances.RemoveRange(minuteAssetMaintenances);
            db.SaveChanges();
        }*/
    }
}