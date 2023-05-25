using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("MinuteAssetDeliveryReceive")]
    public class MinuteAssetDeliveryReceiveModel
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
        public int DeliveryDepartmentId { get; set; }
        [Required]
        public int DeliveryStaffId { get; set; }
        [Required]
        public int ReceiveDepartmentId { get; set; }
        [Required]
        public int ReceiveStaffId { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
        public virtual DepartmentModel DeliveryDepartment { get; set; }
        public virtual DepartmentModel ReceiveDepartment { get; set; }
        public virtual StaffModel DeliveryStaff { get; set; }
        public virtual StaffModel ReceiveStaff { get; set; }
    }

    public class MinuteAssetDeliveryReceiveHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<MinuteAssetDeliveryReceiveModel> GetMinuteAssetDeliveryReceives()
        {
            return db.MinuteAssetDeliveryReceives.OrderByDescending(i => i.AtCreate).ToList();
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        public static void AddNewRecord(MinuteAssetDeliveryReceiveModel minuteAssetDeliveryReceive)
        {
            minuteAssetDeliveryReceive.AtCreate = DateTime.Now;
            db.MinuteAssetDeliveryReceives.Add(minuteAssetDeliveryReceive);
            db.SaveChanges();
        }

        public static void UpdateRecord(MinuteAssetDeliveryReceiveModel minuteAssetDeliveryReceive)
        {
            MinuteAssetDeliveryReceiveModel item = db.MinuteAssetDeliveryReceives.Find(minuteAssetDeliveryReceive.Id);
            item.DeliveryDepartmentId = minuteAssetDeliveryReceive.DeliveryDepartmentId;
            item.ReceiveDepartmentId = minuteAssetDeliveryReceive.ReceiveDepartmentId;
            item.DeliveryStaffId = minuteAssetDeliveryReceive.DeliveryStaffId;
            item.ReceiveStaffId = minuteAssetDeliveryReceive.ReceiveStaffId;
            item.AtEstablishment = minuteAssetDeliveryReceive.AtEstablishment;
            item.Name = minuteAssetDeliveryReceive.Name;
            item.EnglishName = minuteAssetDeliveryReceive.EnglishName;
            item.Initials = minuteAssetDeliveryReceive.Initials;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<MinuteAssetDeliveryReceiveModel> minuteAssetDeliveryReceives = GetMinuteAssetDeliveryReceives().Where(i => selectedIds.Contains(i.Id));
            db.MinuteAssetDeliveryReceives.RemoveRange(minuteAssetDeliveryReceives);
            db.SaveChanges();
        }
    }
}