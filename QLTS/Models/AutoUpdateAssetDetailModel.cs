using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("AutoUpdateAssetDetail")]
    public class AutoUpdateAssetDetailModel
    {
        public int Id { get; set; }
        public int AssetDetailId { get; set; }
        public int AssetStatusId { get; set; }
        public int RoomId { get; set; }
        public DateTime AtImplementation { get; set; }
        public bool isUsed { get; set; }
    }

    public class AutoUpdateAssetDetailHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<AutoUpdateAssetDetailModel> GetAutoUpdateAssetDetails()
        {
            return db.AutoUpdateAssetDetails.Where(n => n.isUsed == false && n.AtImplementation == DateTime.Today).OrderBy(n => n.Id).ToList();
        }
        public static void AutoUpdate()
        {
            foreach (AutoUpdateAssetDetailModel item in GetAutoUpdateAssetDetails())
            {
                AssetDetailModel model = db.AssetDetails.Find(item.AssetDetailId);
                model.AssetStatusId = item.AssetStatusId;
                model.RoomId = item.RoomId;
            }
            db.AutoUpdateAssetDetails.Where(n => n.isUsed == false && n.AtImplementation == DateTime.Today).ToList().ForEach(i => i.isUsed = true);
            db.SaveChanges();
        }
    }
}