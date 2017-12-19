using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project2.Models
{
    [Table("Missions")]
    public class Missions
    {
        [Key]
        public int MissionID { get; set; }
        public String MissionName { get; set; }
        public String MissionPresident { get; set; }
        public String MissionAddress { get; set; }
        public String MissionLanguage { get; set; }
        public String MissionClimate { get; set; }

    }
}