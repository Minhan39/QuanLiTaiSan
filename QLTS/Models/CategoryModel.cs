using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("Category")]
    public class CategoryModel
    {
        public int Id { get; set; }
        public int? Left { get; set; }
        public int? Right { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string ParentName { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
    }

    public class CategoryHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<CategoryModel> GetCategories()
        {
            return db.Categories.OrderBy(n => n.Name).ToList();
        }
        //Kiểm tra tồn tại các danh mục con và các tài sản thuộc phân loại
        public static bool Validation(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            List<CategoryModel> categories = GetCategories().Where(i => selectedIds.Contains((int)i.Id)).ToList();
            List<CategoryModel> categoriesFullChildAndParent = categories.ToList();
            foreach (CategoryModel item in categories)
            {
                List<CategoryModel> newList = db.Categories.Where(n => n.Left > item.Left && n.Right < item.Right).ToList();
                List<CategoryModel> itemToAdd = newList.Except(categoriesFullChildAndParent).ToList();
                if (itemToAdd.Count > 0)
                {
                    categoriesFullChildAndParent.AddRange(itemToAdd);
                }
            }
            /*var list = categoriesFullChildAndParent.Join(db.Assets, c => c.Id, a => a.CategoryId, (c, a) => new {
                CategoryName = c.Name,
                AssetName = a?.Name ?? string.Empty
            }).Select(x => $"{x.CategoryName}: {x.AssetName}").ToList();*/
            return true; /*!(list.Count > 0);*/
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }

        //sử dụng nested set model để xử lí phần phân cấp danh mục
        public static void AddNewRecord(CategoryModel category)
        {
            int? right;
            if (category.ParentName != "ROOT")
            {
                right = db.Categories.FirstOrDefault(n => n.Name == category.ParentName) != null ? (int?)db.Categories.First(n => n.Name == category.ParentName).Right : 1;
                List<CategoryModel> updateList = db.Categories.Where(n => n.Right >= right).ToList();
                foreach (CategoryModel item in updateList)
                {
                    item.Left += item.Left > right ? 2 : 0;
                    item.Right += 2;
                }
            }
            else
            {
                right = db.Categories.FirstOrDefault() != null ? (int?)db.Categories.OrderByDescending(n => n.Right).First().Right : 0;
                right += 1;
            }

            CategoryModel newItem = new CategoryModel()
            {
                Name = category.Name,
                Left = right,
                Right = right + 1,
                ParentName = (category.ParentName != "ROOT") ? category.ParentName : "",
                AtCreate = DateTime.Now,
                AtUpdate = null
            };
            db.Categories.Add(newItem);
            db.SaveChanges();
        }

        public static void UpdateRecord(CategoryModel category)
        {
            /*Category item = db.Categories.Find(category.Id);
            item.Name = category.Name;
            item.CategoryId = category.CategoryId;
            item.ParentId = category.ParentId;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();*/
        }

        //sử dụng nested set model để xử lí phần phân cấp danh mục
        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<CategoryModel> categories = GetCategories().Where(i => selectedIds.Contains(i.Id));
            List<CategoryModel> list = db.Categories.ToList();
            foreach (CategoryModel item in categories)
            {
                if (list.Find(n => n.Name == item.Name) != null)
                {
                    List<CategoryModel> removeList = db.Categories.Where(n => n.Left > item.Left && n.Right < item.Right).ToList();
                    db.Categories.RemoveRange(removeList);

                    int delta = (int)item.Right - (int)item.Left + 1;
                    List<CategoryModel> updateList = db.Categories.Where(n => n.Right >= item.Right).ToList();
                    foreach (CategoryModel updateItem in updateList)
                    {
                        updateItem.Left -= updateItem.Left > item.Right ? delta : 0;
                        updateItem.Right -= delta;
                    }
                    db.Categories.Remove(item);
                    foreach (var removeItem in removeList)
                    {
                        list.Remove(removeItem);
                    }
                    list.Remove(item);
                }
            }
            db.SaveChanges();
        }
    }
}