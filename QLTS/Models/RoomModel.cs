using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("Room")]
    public class RoomModel
    {
        public int Id { get; set; }
        [Required]
        public int AreaId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int? Floor { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
        public virtual AreaModel Area { get; set; }
    }

    public class RoomHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<RoomModel> GetRooms()
        {
            return db.Rooms.OrderBy(i => i.Name).ToList();
        }
        public static GridViewModel GetGridViewModel()
        {
            return new GridViewModel();
        }
        public static void AddNewRecord(RoomModel room)
        {
            room.AtCreate = DateTime.Now;
            db.Rooms.Add(room);
            db.SaveChanges();
        }

        public static void UpdateRecord(RoomModel room)
        {
            RoomModel item = db.Rooms.Find(room.Id);
            item.Name = room.Name;
            item.Floor = room.Floor;
            item.AreaId = room.AreaId;
            item.AtUpdate = DateTime.Now;
            db.SaveChanges();
        }

        public static void DeleteRecords(string selectedRowIds)
        {
            List<int> selectedIds = selectedRowIds.Split(',').ToList().ConvertAll(id => int.Parse(id));
            IEnumerable<RoomModel> rooms = GetRooms().Where(i => selectedIds.Contains(i.Id));
            db.Rooms.RemoveRange(rooms);
            db.SaveChanges();
        }
    }
}