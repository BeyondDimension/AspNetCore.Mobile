using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace AspNetCore6.Mobile.Maui
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}