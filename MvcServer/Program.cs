using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MvcServer {
    public class Program {
        public static void Main (string[] args) {
            CreateHostBuilder (args).Build ().Run ();
        }

        public static IHostBuilder CreateHostBuilder (string[] args) =>
            Host.CreateDefaultBuilder (args)
            .ConfigureWebHostDefaults (webBuilder => {
                var  config  =  new  ConfigurationBuilder ()
                    .SetBasePath (Directory.GetCurrentDirectory ())
                    .AddJsonFile ("appsetting.json",  optional :  true) 
                    .AddJsonFile ("hosting.json",  optional :  true) 
                    .AddCommandLine (args)
                    .Build ();
                webBuilder.UseConfiguration (config);
                webBuilder.UseStartup<Startup> ();
                Program.SetServerInfo (config);
            });
        public static void SetServerInfo (IConfiguration config) {
            String ServerNameKey = "ServerName";
            String ServerAddressKey = "urls";
            String serverName = config.GetSection (ServerNameKey)?.Value;
            String serverAddress = config.GetSection (ServerAddressKey)?.Value;
            String serverPort = "";
            if (!string.IsNullOrEmpty (serverAddress)) {
                string[] arr;
                if (serverAddress.IndexOf (";") > -1) {
                    arr = serverAddress.Split (";");
                    if (arr.Length > 0) {
                        serverAddress = arr[arr.Length - 1];
                        if (serverAddress.IndexOf (":") > -1) {
                            arr = serverAddress.Split (":");
                            serverPort = arr[arr.Length - 1];
                        }
                    }
                } else {
                    if (serverAddress.IndexOf (":") > -1) {
                        arr = serverAddress.Split (":");
                        serverPort = arr[arr.Length - 1];
                    }
                }
            }
            serverName = string.IsNullOrEmpty (serverName) ? "" : serverName;
            Console.Title = $"{serverName}({serverPort})";
        }
    }
}