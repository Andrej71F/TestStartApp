using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskStar.ZvtTest.ZvtManager
{
    public class LoggerFactory
    {
        public static Serilog.ILogger CreateLoggerSerilog(string baseDirectory, string configFile, string sectionName)
        {
            string _baseDirectory = baseDirectory ?? System.IO.Directory.GetCurrentDirectory();
            string _configFile = configFile ?? "serilogconfig.json";
            string _sectionName = sectionName ?? "Serilog";

            IConfigurationRoot _serilogConfig = new ConfigurationBuilder()
                .SetBasePath(_baseDirectory)
                .AddJsonFile(path: _configFile, optional: false, reloadOnChange: true)
                .Build();

            Serilog.ILogger _serilog = new LoggerConfiguration()
                .ReadFrom.Configuration(_serilogConfig, sectionName: _sectionName)
                .CreateLogger();

            return _serilog;
        }

    }
}
