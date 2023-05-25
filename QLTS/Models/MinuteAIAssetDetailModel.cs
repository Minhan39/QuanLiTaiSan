using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("MinuteAIAssetDetail")]
    public class MinuteAIAssetDetailModel
    {
        public int Id { get; set; }
        [Required]
        public int MinuteAssetInventoryId { get; set; }
        [Required]
        public int AssetDetailId { get; set; }
        [Required]
        public int AssetStatusId { get; set; }
        [Required]
        public int RoomId { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
        public virtual MinuteAssetInventoryModel MinuteAssetInventoty { get; set; }
        public virtual AssetDetailModel AssetDetail { get; set; }
        public virtual RoomModel Room { get; set; }
    }
}