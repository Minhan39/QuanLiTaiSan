using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("Department")]
    public class DepartmentModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
    }
    public class DepartmentHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<DepartmentModel> GetDepartments()
        {
            return db.Departments.OrderBy(i => i.Name).ToList();
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        public static void AddNewRecord(DepartmentModel department)
        {
            department.AtCreate = DateTime.Now;
            db.Departments.Add(department);
            db.SaveChanges();
        }

        public static void UpdateRecord(DepartmentModel department)
        {
            DepartmentModel item = db.Departments.Find(department.Id);
            item.Name = department.Name;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<DepartmentModel> departments = GetDepartments().Where(i => selectedIds.Contains(i.Id));
            db.Departments.RemoveRange(departments);
            db.SaveChanges();
        }
    }
}