using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("MinuteAssetInventory")]
    public class MinuteAssetInventoryModel
    {
        public int Id { get; set; }
        [Required]
        public int TypeOfMinutesId { get; set; }
        [Required]
        public DateTime AtEstablishment { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Initials { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int StaffId { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
        public virtual DepartmentModel Department { get; set; }
        public virtual StaffModel Staff { get; set; }
    }

    public class MinuteAssetInventoryHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<MinuteAssetInventoryModel> GetMinuteAssetInventories()
        {
            return db.MinuteAssetInventories.OrderByDescending(i => i.AtCreate).ToList();
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        public static void AddNewRecord(MinuteAssetInventoryModel MinuteAssetInventory)
        {
            MinuteAssetInventory.AtCreate = DateTime.Now;
            db.MinuteAssetInventories.Add(MinuteAssetInventory);
            db.SaveChanges();
        }

        public static void UpdateRecord(MinuteAssetInventoryModel MinuteAssetInventory)
        {
            MinuteAssetInventoryModel item = db.MinuteAssetInventories.Find(MinuteAssetInventory.Id);
            item.DepartmentId = MinuteAssetInventory.DepartmentId;
            item.StaffId = MinuteAssetInventory.StaffId;
            item.AtEstablishment = MinuteAssetInventory.AtEstablishment;
            item.Name = MinuteAssetInventory.Name;
            item.EnglishName = MinuteAssetInventory.EnglishName;
            item.Initials = MinuteAssetInventory.Initials;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<MinuteAssetInventoryModel> MinuteAssetInventories = GetMinuteAssetInventories().Where(i => selectedIds.Contains(i.Id));
            db.MinuteAssetInventories.RemoveRange(MinuteAssetInventories);
            db.SaveChanges();
        }
    }
}