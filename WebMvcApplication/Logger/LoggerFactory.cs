using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvcApplication.Logger
{
    public class LoggerFactory
    {
        private static Serilog.Core.Logger _logger = null;
        private LoggerFactory()
        {
        }

        public static Serilog.Core.Logger Initialize(IConfiguration configuration)
        {
            _logger = _logger ?? new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            return _logger;
        }
    }
}
