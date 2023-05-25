using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("MinuteEnterAssetAtBegin")]
    public class MinuteEnterAssetAtBeginModel
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

    public class MinuteEnterAssetAtBeginHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<MinuteEnterAssetAtBeginModel> GetMinuteEnterAssetAtBegins()
        {
            return db.MinuteEnterAssetAtBegins.OrderByDescending(i => i.AtCreate).ToList();
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        public static void AddNewRecord(MinuteEnterAssetAtBeginModel MinuteEnterAssetAtBegin)
        {
            MinuteEnterAssetAtBegin.AtCreate = DateTime.Now;
            db.MinuteEnterAssetAtBegins.Add(MinuteEnterAssetAtBegin);
            db.SaveChanges();
        }

        public static void UpdateRecord(MinuteEnterAssetAtBeginModel MinuteEnterAssetAtBegin)
        {
            MinuteEnterAssetAtBeginModel item = db.MinuteEnterAssetAtBegins.Find(MinuteEnterAssetAtBegin.Id);
            item.DepartmentId = MinuteEnterAssetAtBegin.DepartmentId;
            item.StaffId = MinuteEnterAssetAtBegin.StaffId;
            item.AtEstablishment = MinuteEnterAssetAtBegin.AtEstablishment;
            item.Name = MinuteEnterAssetAtBegin.Name;
            item.EnglishName = MinuteEnterAssetAtBegin.EnglishName;
            item.Initials = MinuteEnterAssetAtBegin.Initials;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<MinuteEnterAssetAtBeginModel> MinuteEnterAssetAtBegins = GetMinuteEnterAssetAtBegins().Where(i => selectedIds.Contains(i.Id));
            db.MinuteEnterAssetAtBegins.RemoveRange(MinuteEnterAssetAtBegins);
            db.SaveChanges();
        }
    }
}