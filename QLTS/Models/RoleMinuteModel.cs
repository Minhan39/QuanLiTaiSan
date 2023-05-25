using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    public class RoleMinuteModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class RoleMinuteHelper
    {
        public static List<RoleMinuteModel> GetRoleMinutes()
        {
            List<RoleMinuteModel> list = new List<RoleMinuteModel>();
            list.Add(new RoleMinuteModel() { Id = 1, Name = "Đơn vị nhận" });
            list.Add(new RoleMinuteModel() { Id = 2, Name = "Đơn vị giao" });
            list.Add(new RoleMinuteModel() { Id = 3, Name = "Người xác nhận" });
            return list;
        }
    }
}