using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("Area")]
    public class AreaModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? AtCreate { get; set; }

        public DateTime? AtUpdate { get; set; }  
    }

    public class AreaHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<AreaModel> GetAreas()
        {
            return db.Areas.OrderBy(i => i.Name).ToList();
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        public static void AddNewRecord(AreaModel area)
        {
            area.AtCreate = DateTime.Now;
            db.Areas.Add(area);
            db.SaveChanges();
        }

        public static void UpdateRecord(AreaModel area)
        {
            AreaModel item = db.Areas.Find(area.Id);
            item.Name = area.Name;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<AreaModel> areas = GetAreas().Where(i => selectedIds.Contains(i.Id));
            db.Areas.RemoveRange(areas);
            db.SaveChanges();
        }
    }
}