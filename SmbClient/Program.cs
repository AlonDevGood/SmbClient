using System;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using SharpCifs.Smb;
using System.Text;

namespace SmbClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json")
            .Build();

            var auth = new NtlmPasswordAuthentication(string.Format("{0}:{1}", config["UserName"], config["Password"]));
            var folder = new SmbFile(string.Format("smb://{0}",config["NetworkPath"]), auth);
            Console.WriteLine(folder.GetPath());
            var writeStream = folder.GetOutputStream();
            writeStream.Write(Encoding.UTF8.GetBytes("Hello!"));
            writeStream.Dispose();
            Console.WriteLine("hi");
        }
    }
}
