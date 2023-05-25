using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("Accessory")]
    public class AccessoryModel
    {
        public int Id { get; set; }
        [Required]
        public int AssetId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string Notes { get; set; }
        [Required]
        public decimal Value { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
        public virtual AssetModel Asset { get; set; }
    }
    public class AccessoryHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<AccessoryModel> GetAccessories()
        {
            return db.Accessories.OrderBy(i => i.Name).ToList();
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        public static void AddNewRecord(AccessoryModel accessory)
        {
            accessory.AtCreate = DateTime.Now;
            db.Accessories.Add(accessory);
            db.SaveChanges();
        }

        public static void UpdateRecord(AccessoryModel accessory)
        {
            AccessoryModel item = db.Accessories.Find(accessory.Id);
            item.Name = accessory.Name;
            item.Notes = accessory.Notes;
            item.Value = accessory.Value;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<AccessoryModel> accessories = GetAccessories().Where(i => selectedIds.Contains(i.Id));
            db.Accessories.RemoveRange(accessories);
            db.SaveChanges();
        }
    }
}