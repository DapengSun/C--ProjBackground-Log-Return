using Common;
using DemoProj.BLL;
using DemoProj.DAL;
using DemoProj.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoProj.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            try
            {
                string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalMyConnection"].ConnectionString;
                using (MySqlConnection _mysqlConnection = new MySqlConnection(_connectionString))
                {
                    string sql = "select * from testtable";
                    MySqlCommand _mysqlCommand = new MySqlCommand(sql, _mysqlConnection);
                    //_mysqlConnection.Open();
                    //MySqlDataReader _mysqlDataReader = _mysqlCommand.ExecuteReader();
                    MySqlDataAdapter _mysqlDataAdapter = new MySqlDataAdapter(_mysqlCommand);
                    DataTable Dt = new DataTable();
                    _mysqlDataAdapter.Fill(Dt);
                }
            }
            catch(Exception ee) {
                throw ee;
            }
            return View();
        }

        public ActionResult TestData(string id)
        {
            TestModel _testModel = new TestModel();
            try
            {
                TestBLL _testBLL = new TestBLL();
                 _testModel = _testBLL.getTestModel();
                ViewBag.ID = id;
            }
            catch (Exception ee)
            {
                throw ee;
            }
            return View(_testModel);
        }

        public ActionResult ChangeData(string id)
        {
            return Content("新数据是" + id);
        }

        public ActionResult CreateDataBase() {
            try
            {
                TestDBContext _textDbContext = new TestDBContext();
                _textDbContext.Database.CreateIfNotExists();

                return Content("成功！");
            }
            catch (Exception ee)
            {
                return Content(ee.ToString());
            }
        }

        public ActionResult UpdateLoadIndex()
        {
            return View("UpdateLoad");
        }

        public JsonResult UpdateLoad() {
            var oFile = Request.Files["txt_file"];

            var oStream = oFile.InputStream;
            //得到了文件的流对象，我们不管是用NPOI、GDI还是直接保存文件都不是问题了吧。。。。

            try
            {
                new OSSHelper();

                new ResponseHelper().ReturnResultAndLog(HttpContext.Request.Url.Host, Guid.NewGuid(),
                    System.Net.HttpStatusCode.OK, EnumModel.Errorflag.Info, "OSS创建成功","");
            }
            catch (Exception ee)
            {
                new ResponseHelper().ReturnResultAndLog(HttpContext.Request.Url.Host, Guid.NewGuid(),
                    System.Net.HttpStatusCode.BadGateway, EnumModel.Errorflag.Error, ee.ToString(),
                    "");
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}