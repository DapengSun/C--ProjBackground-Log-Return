using DemoProj.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemoProj.DAL
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class TestDBContext:DbContext
    {
        public TestDBContext() : base("name=LocalMyConnection") {}

        public DbSet<TestModel> TestModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}