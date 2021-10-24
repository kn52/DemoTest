using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace RazorPagesSln.AppConfig
{
    public class AppSettingConfig
    {
        protected readonly IConfiguration _config;
        public string _dbConn;
        public int _pageSize;
        public int _pageSubSize;
        public List<Obj> _obj;
        public AppSettingConfig(IConfiguration config)
        {
            _config = config;
            _dbConn = _config["ConnectionStrings:MyDBConnection"];
            _pageSize = Convert.ToInt32(_config["PageSize"]);
            _pageSubSize = Convert.ToInt32(_config["PageSubSize"]);
            _obj = _config.GetSection("Obj:hi").Get<List<Obj>>();
        }

    }

    public class Obj
    {
        public string one { get; set; }
        public string tow { get; set; }
    }
}
