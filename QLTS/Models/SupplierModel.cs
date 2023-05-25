using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("Supplier")]
    public class SupplierModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Group { get; set; }
        [StringLength(100)]
        public string Subject { get; set; }
        [Required]
        [StringLength(15)]
        public string TaxCode { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(16)]
        public string Phone { get; set; }
        [StringLength(16)]
        public string Fax { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Country { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string District { get; set; }
        [StringLength(100)]
        public string Ward { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
    }

    public class SupplierHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<SupplierModel> GetSuppliers()
        {
            return db.Suppliers.OrderBy(i => i.Name).ToList();
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        //Kiểm tra tồn tại các tài sản thuộc nhà cung cấp
        public static bool Validation(string selectedRowIds)
        {
            /*List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<Asset> assets = AssetHelper.GetAssetsNotJoin().Where(i => selectedIds.Contains((int)i.SupplierId));
            if (assets.Count() > 0)
            {
                return false;
            }*/
            return true;
        }
        public static void AddNewRecord(SupplierModel supplier)
        {
            supplier.AtCreate = DateTime.Now;
            db.Suppliers.Add(supplier);
            db.SaveChanges();
        }

        public static void UpdateRecord(SupplierModel supplier)
        {
            SupplierModel item = db.Suppliers.Find(supplier.Id);
            item.Name = supplier.Name;
            item.Address = supplier.Address;
            item.City = supplier.City;
            item.Country = supplier.Country;
            item.District = supplier.District;
            item.Email = supplier.Email;
            item.Fax = supplier.Fax;
            item.Group = supplier.Group;
            item.Phone = supplier.Phone;
            item.Subject = supplier.Subject;
            item.TaxCode = supplier.TaxCode;
            item.Ward = supplier.Ward;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<SupplierModel> suppliers = GetSuppliers().Where(i => selectedIds.Contains(i.Id));
            db.Suppliers.RemoveRange(suppliers);
            db.SaveChanges();
        }
    }
}