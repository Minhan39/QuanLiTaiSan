using DevExpress.Web.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("Asset")]
    public class AssetModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int ValueClassificationId { get; set; }
        [Required]
        public int UnitId { get; set; }
        [Required]
        public int SupplierId { get; set; }
        public DateTime? AtUsing { get; set; }
        public int? YearOfUse { get; set; }
        public DateTime? AtExpiration { get; set; }
        public decimal? QualityStatus { get; set; }
        public DateTime? AtStartOfDepreciation { get; set; }
        public decimal? DepreciationRate { get; set; }
        public int? Maintenance { get; set; }
        public string Notes { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
        public virtual CategoryModel Category { get; set; }
        public virtual UnitModel Unit { get; set; }
        public virtual SupplierModel Supplier { get; set; }
        public virtual List<AssetDetailModel> AssetDetails { get; set; }

    }

    public class AssetHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public const string ImageQueryKey = "DXImage";
        public static List<AssetModel> GetAssets()
        {
            return db.Assets.OrderBy(i => i.Name).ToList();
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        public static void AddNewRecord(AssetModel asset)
        {
            asset.AtCreate = DateTime.Now;
            db.Assets.Add(asset);
            db.SaveChanges();
        }

        public static void UpdateRecord(AssetModel asset)
        {
            AssetModel item = db.Assets.Find(asset.Id);
            item.Name = asset.Name;
            item.Image = asset.Image;
            item.CategoryId = asset.CategoryId;
            item.ValueClassificationId = asset.ValueClassificationId;
            item.UnitId = asset.UnitId;
            item.SupplierId = asset.SupplierId;
            item.AtUsing = asset.AtUsing;
            item.YearOfUse = asset.YearOfUse;
            item.AtExpiration = asset.AtExpiration;
            item.QualityStatus = asset.QualityStatus;
            item.AtStartOfDepreciation = asset.AtStartOfDepreciation;
            item.DepreciationRate = asset.DepreciationRate;
            item.Maintenance = asset.Maintenance;
            item.Notes = asset.Notes;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<AssetModel> assets = GetAssets().Where(i => selectedIds.Contains(i.Id));
            db.Assets.RemoveRange(assets);
            db.SaveChanges();
        }
        public static string GetImageRouteUrl()
        {
            return DevExpressHelper.GetUrl(new
            {
                Controller = "Asset",
                Action = "GetImage"
            });
        }
        public static Binary GetPhoto(int id)
        {
            return (from asset in db.Assets
                    where asset.Id == id
                    select asset.Image).SingleOrDefault();
        }
    }
}