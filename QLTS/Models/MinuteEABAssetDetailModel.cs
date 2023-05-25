using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("MinuteEABAssetDetail")]
    public class MinuteEABAssetDetailModel
    {
        public int Id { get; set; }
        [Required]
        public int MinuteEnterAssetAtBeginId { get; set; }
        [Required]
        public int AssetDetailId { get; set; }
        [Required]
        public int AssetStatusId { get; set; }
        [Required]
        public int RoomId { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
        public virtual MinuteEnterAssetAtBeginModel MinuteEnterAssetAtBegin { get; set; }
        public virtual AssetDetailModel AssetDetail { get; set; }
        public virtual RoomModel Room { get; set; }
    }

    public class MinuteEABAssetDetailHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<MinuteEABAssetDetailModel> GetList()
        {
            return db.MinuteEABAssetDetails.ToList();
        }
        public static List<MinuteEABAssetDetailModel> GetList(int Id)
        {
            return db.MinuteEABAssetDetails.Where(n => n.MinuteEnterAssetAtBeginId == Id).ToList();
        }
        public static void AddNewRecords(List<MinuteEABAssetDetailModel> list)
        {
            List<MinuteEABAssetDetailModel> newList = list.Join(db.AssetDetails.ToList(), n => n.AssetDetailId, m => m.DefinitionId, (n, m) => new MinuteEABAssetDetailModel
            {
                MinuteEnterAssetAtBeginId = n.MinuteEnterAssetAtBeginId,
                AssetDetailId = m.Id,
                AssetStatusId = n.AssetStatusId,
                RoomId = n.RoomId,
                AtCreate = n.AtCreate,
                AtUpdate = n.AtUpdate
            }).ToList();
            foreach (MinuteEABAssetDetailModel item in newList)
            {
                item.AtCreate = DateTime.Now;
                db.MinuteEABAssetDetails.Add(item);
            }
            db.SaveChanges();
        }
    }
}