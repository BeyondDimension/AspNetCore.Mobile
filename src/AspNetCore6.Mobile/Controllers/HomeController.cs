using AspNetCore6.Mobile.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AspNetCore6.Mobile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        static string? GetAssemblyVersion(Assembly assembly)
          => assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
          .InformationalVersion
          .Split(new[] { '+' }, StringSplitOptions.RemoveEmptyEntries)
          .FirstOrDefault();

        public IActionResult Index()
        {
            return Content($"<h1>OK!</h1><br/><h2>AspNetCore Version: {GetAssemblyVersion(typeof(Controller).Assembly)}</h2><br/><h3>ProcessArch: {RuntimeInformation.ProcessArchitecture}</h3><br/><h3>OSArch: {RuntimeInformation.OSArchitecture}</h3><br/><h3>EnvOSVerPlatform: {Environment.OSVersion.Platform}</h3><br/><h3>IsWindows: {OperatingSystem.IsWindows()}</h3><br/><h3>IsAndroid: {OperatingSystem.IsAndroid()}</h3>", "text/html");
            //return View();
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