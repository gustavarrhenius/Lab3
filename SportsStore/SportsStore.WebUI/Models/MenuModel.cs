using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    public class MenuModel
    {
        public IEnumerable<string> categories { get; set; }
        public string selected { get; set; }
    }
}