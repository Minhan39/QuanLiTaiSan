using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    public class TypeOfMinutesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
    }
    public class TypeOfMinutesHelper
    {
        public static List<TypeOfMinutesModel> GetTypeOfMinutes()
        {
            List<TypeOfMinutesModel> typeOfMinutes = new List<TypeOfMinutesModel>();
            typeOfMinutes.Add(new TypeOfMinutesModel() { Id = 1, Name = "Nhập tài sản đầu kỳ" });
            typeOfMinutes.Add(new TypeOfMinutesModel() { Id = 2, Name = "Giao nhận tài sản" });
            typeOfMinutes.Add(new TypeOfMinutesModel() { Id = 3, Name = "Kiểm kê tài sản" });
            typeOfMinutes.Add(new TypeOfMinutesModel() { Id = 4, Name = "Bảo trì tài sản" });
            typeOfMinutes.Add(new TypeOfMinutesModel() { Id = 5, Name = "Thanh lý tài sản" });
            return typeOfMinutes;
        }
    }
}