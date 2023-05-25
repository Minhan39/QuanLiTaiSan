using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("MinuteADRAssetDetail")]
    public class MinuteADRAssetDetailModel
    {
        public int Id { get; set; }
        [Required]
        public int MinuteAssetDeliveryReceiveId { get; set; }
        [Required]
        public int AssetDetailId { get; set; }
        [Required]
        public int AssetStatusId { get; set; }
        [Required]
        public int RoomId { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
        public virtual MinuteAssetDeliveryReceiveModel MinuteAssetDeliveryReceive { get; set; }
        public virtual AssetDetailModel AssetDetail { get; set; }
        public virtual RoomModel Room { get; set; }
    }

    public class MinuteADRAssetDetailHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<MinuteADRAssetDetailModel> GetList()
        {
            return db.MinuteADRAssetDetails.ToList();
        }
        public static List<MinuteADRAssetDetailModel> GetList(int Id)
        {
            return db.MinuteADRAssetDetails.Where(n => n.MinuteAssetDeliveryReceiveId == Id).ToList();
        }
        public static void AddNewRecords(List<MinuteADRAssetDetailModel> list)
        {
            List<MinuteADRAssetDetailModel> newList = list.Join(db.AssetDetails.ToList(), n => n.AssetDetailId, m => m.DefinitionId, (n, m) => new MinuteADRAssetDetailModel
            {
                MinuteAssetDeliveryReceiveId = n.MinuteAssetDeliveryReceiveId,
                AssetDetailId = m.Id,
                AssetStatusId = n.AssetStatusId,
                RoomId = n.RoomId,
                AtCreate = n.AtCreate,
                AtUpdate = n.AtUpdate
            }).ToList();
            MinuteAssetDeliveryReceiveModel model = db.MinuteAssetDeliveryReceives.Find(newList[0].MinuteAssetDeliveryReceiveId);
            foreach (MinuteADRAssetDetailModel item in newList)
            {
                AutoUpdateAssetDetailModel auto = new AutoUpdateAssetDetailModel();
                auto.AtImplementation = model.AtEstablishment;
                auto.AssetDetailId = item.AssetDetailId;
                auto.AssetStatusId = item.AssetStatusId;
                auto.RoomId = item.RoomId;
                auto.isUsed = false;
                db.AutoUpdateAssetDetails.Add(auto);
                item.AtCreate = DateTime.Now;
                db.MinuteADRAssetDetails.Add(item);
            }
            db.SaveChanges();
        }
    }
}