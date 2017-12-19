using Project2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project2.DAL
{
    public class Project2Context : DbContext
    {
        public Project2Context() : base("Project2Context")
        {

        }

        public DbSet<Missions> Missions { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<MissionQuestions> MissionQuestions { get; set; }
    }
}