using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("MinuteAssetLiquidation")]
    public class MinuteAssetLiquidationModel
    {
        public int Id { get; set; }
        [Required]
        public int MinuteId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public decimal Value { get; set; }
        [StringLength(100)]
        public string BuyerName { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
        /*public virtual MinuteModel Minute { get; set; }*/
        public virtual DepartmentModel Department { get; set; }
    }

    public class MinuteAssetLiquidationHelper
    {
        /*private static QLTSDbContext db = new QLTSDbContext();
        public static List<MinuteAssetLiquidationModel> GetMinuteAssetLiquidations()
        {
            return db.MinuteAssetLiquidations.OrderByDescending(i => i.AtCreate).ToList();
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        public static void AddNewRecord(MinuteAssetLiquidationModel minuteAssetLiquidation)
        {
            minuteAssetLiquidation.AtCreate = DateTime.Now;
            db.MinuteAssetLiquidations.Add(minuteAssetLiquidation);
            db.SaveChanges();
        }

        public static void UpdateRecord(MinuteAssetLiquidationModel minuteAssetLiquidation)
        {
            MinuteAssetLiquidationModel item = db.MinuteAssetLiquidations.Find(minuteAssetLiquidation.Id);
            item.MinuteId = minuteAssetLiquidation.MinuteId;
            item.DepartmentId = minuteAssetLiquidation.DepartmentId;
            item.Value = minuteAssetLiquidation.Value;
            item.BuyerName = minuteAssetLiquidation.BuyerName;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<MinuteAssetLiquidationModel> minuteAssetLiquidations = GetMinuteAssetLiquidations().Where(i => selectedIds.Contains(i.Id));
            db.MinuteAssetLiquidations.RemoveRange(minuteAssetLiquidations);
            db.SaveChanges();
        }*/
    }
}