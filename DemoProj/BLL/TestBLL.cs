using DemoProj.DAL;
using DemoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoProj.BLL
{
    public class TestBLL
    {
        private TestDAL _testDal = new TestDAL();

        public TestModel getTestModel() {
            return _testDal.GetTestData();
        }
    }
}