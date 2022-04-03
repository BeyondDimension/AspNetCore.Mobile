using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Mobile.Models;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AspNetCore.Mobile.Controllers
{
    public class HomeController : Controller
    {
        static string? GetAssemblyVersion(Assembly assembly)
           => assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
           .InformationalVersion
           .Split(new[] { '+' }, StringSplitOptions.RemoveEmptyEntries)
           .FirstOrDefault();

        static bool IsAndroid =>
#if NETFRAMEWORK
            false;
#else
        OperatingSystem2.IsAndroid;
#endif

        public IActionResult Index()
        {
            return Content($"<h1>OK!</h1><br/><h2>AspNetCore Version: {GetAssemblyVersion(typeof(Controller).Assembly)}</h2><br/><h3>EnvVer: {Environment.Version}</h3><br/><h3>ProcessArch: {RuntimeInformation.ProcessArchitecture}</h3><br/><h3>OSArch: {RuntimeInformation.OSArchitecture}</h3><br/><h3>EnvOSVerPlatform: {Environment.OSVersion.Platform}</h3><br/><h3>IsWindows: {OperatingSystem2.IsWindows}</h3><br/><h3>IsAndroid: {IsAndroid}</h3>", "text/html");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
