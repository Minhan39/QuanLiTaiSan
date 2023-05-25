using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("Staff")]
    public class StaffModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string StaffId { get; set; }
        public byte[] Image { get; set; }
        [StringLength(50)]
        [Required]
        public string Firstname { get; set; }
        [StringLength(50)]
        [Required]
        public string Lastname { get; set; }
        [NotMapped]
        public string Fullname { get { return string.Format("{0} {1}", Firstname, Lastname); } }
        [StringLength(100)]
        public string Status { get; set; }
        [StringLength(100)]
        public string TypeStaff { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(16)]
        public string Phone { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
    }

    public class StaffHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<StaffModel> GetStaffs()
        {
            return db.Staffs.OrderBy(n => n.Lastname).ToList();
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        public static void AddNewRecord(StaffModel staff)
        {
            staff.AtCreate = DateTime.Now;
            db.Staffs.Add(staff);
            db.SaveChanges();
        }

        public static void UpdateRecord(StaffModel staff)
        {
            StaffModel item = db.Staffs.Find(staff.Id);
            item.Firstname = staff.Firstname;
            item.Lastname = staff.Lastname;
            item.Status = staff.Status;
            item.TypeStaff = staff.TypeStaff;
            item.StaffId = staff.StaffId;
            item.Image = staff.Image;
            item.Email = staff.Email;
            item.Phone = staff.Phone;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<StaffModel> staffs = GetStaffs().Where(i => selectedIds.Contains(i.Id));
            db.Staffs.RemoveRange(staffs);
            db.SaveChanges();
        }
    }
}