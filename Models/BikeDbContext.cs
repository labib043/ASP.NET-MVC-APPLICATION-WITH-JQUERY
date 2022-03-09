using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bikemvc.Models
{
    public class BikeDbContext : DbContext
    {
        public BikeDbContext() : base("Con")
        {

        }
        public DbSet<BikeInfo> BikeInfos { get; set; }
        public DbSet<ModelInfo> ModelInfos { get; set; }
        public DbSet<SignUp> SignUps { get; set; }
    }
}
