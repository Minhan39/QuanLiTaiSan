using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace QLTS.Models
{
    [Table("AssetDetail")]
    public class AssetDetailModel
    {
        public int Id { get; set; }
        [Required]
        public int AssetId { get; set; }
        [Required]
        public int DefinitionId { get; set; }
        [Required]
        public int AssetStatusId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public decimal Value { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
        [NotMapped]
        public virtual AssetModel Asset { get 
            {
                return AssetHelper.GetAssets().Find(n => n.Id == AssetId);
            }
        }
        [NotMapped]
        public virtual RoomModel Room { get
            {
                return RoomHelper.GetRooms().Find(n => n.Id == RoomId);
            }
        }
    }
    public class AssetDetailHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public const string UploadDirectory = "~/Content/UploadFolder/";
        public static List<AssetDetailModel> GetAssetDetails()
        {
            return db.AssetDetails.OrderByDescending(i => i.AtCreate).ToList();
        }
        public static List<AssetDetailModel> GetAssetDetails(int Id)
        {
            return db.AssetDetails.OrderByDescending(i => i.AtCreate).Where(i => i.AssetId == Id).ToList();
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        public static void AddNewRecord(AssetDetailModel assetDetail)
        {
            assetDetail.AtCreate = DateTime.Now;
            db.AssetDetails.Add(assetDetail);
            db.SaveChanges();
        }

        public static void AddNewRecords(List<AssetDetailModel> list)
        {
            foreach(AssetDetailModel item in list)
            {
                item.AtCreate = DateTime.Now;
                db.AssetDetails.Add(item);
            }
            db.SaveChanges();
        }

        public static void UpdateRecord(AssetDetailModel assetDetail)
        {
            AssetDetailModel item = db.AssetDetails.Find(assetDetail.Id);
            item.AssetStatusId = assetDetail.AssetStatusId;
            item.Value = assetDetail.Value;
            item.RoomId = assetDetail.RoomId;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<AssetDetailModel> assetDetails = GetAssetDetails().Where(i => selectedIds.Contains(i.Id));
            db.AssetDetails.RemoveRange(assetDetails);
            db.SaveChanges();
        }
    }
}