using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    public class ValueClassificationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ValueClassificationHelper
    {
        public static List<ValueClassificationModel> GetValueClassifications()
        {
            List<ValueClassificationModel> list = new List<ValueClassificationModel>();
            list.Add(new ValueClassificationModel() { Id = 1, Name = "Dưới 500 triệu VNĐ" });
            list.Add(new ValueClassificationModel() { Id = 2, Name = "Lớn hơn hoặc bằng 500 triệu VNĐ" });
            return list;
        }
    }
}