using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoProj.Models
{
    public class TestModel
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }
    }
}