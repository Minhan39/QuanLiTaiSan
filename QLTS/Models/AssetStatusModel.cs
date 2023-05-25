using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    public class AssetStatusModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AssetStatusHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<AssetStatusModel> GetAssetStatuses()
        {
            List<AssetStatusModel> list = new List<AssetStatusModel>();
            list.Add(new AssetStatusModel() { Id = 1, Name = "Đang sử dụng" });
            list.Add(new AssetStatusModel() { Id = 2, Name = "Đang sửa chữa" });
            list.Add(new AssetStatusModel() { Id = 3, Name = "Không sử dụng" });
            return list;
        }
    }
}