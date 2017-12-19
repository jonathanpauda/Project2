using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project2.Models
{
    [Table("MissionQuestions")]
    public class MissionQuestions
    {
        [Key]
        public int MissionQuestionID { get; set; }
        public int MissionID { get; set; }
        public int UserID { get; set; }
        public String Question { get; set; }
        public String Answer { get; set; }
    }
}