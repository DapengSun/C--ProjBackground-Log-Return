using DemoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoProj.DAL
{
    public class TestDAL
    {
        public TestModel GetTestData() {
            using (var dbContext = new TestDBContext()) {
                return dbContext.TestModel.First();
            }
        }
    }
}