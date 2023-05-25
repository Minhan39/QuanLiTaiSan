using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    [Table("Unit")]
    public class UnitModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(10)]
        public string Abbreviation { get; set; }
        public DateTime? AtCreate { get; set; }
        public DateTime? AtUpdate { get; set; }
    }

    public class UnitHelper
    {
        private static QLTSDbContext db = new QLTSDbContext();
        public static List<UnitModel> GetUnits()
        {
            return db.Units.OrderBy(i => i.Name).ToList();
        }
    }
}