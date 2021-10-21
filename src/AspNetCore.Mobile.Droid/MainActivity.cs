using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Microsoft.AspNetCore.Hosting;
using System.Logging;
using System.Threading.Tasks;
using Android.Widget;
using Xamarin.Essentials;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using XEPlatform = Xamarin.Essentials.Platform;
using Environment = System.Environment;

namespace AspNetCore.Mobile.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        AndroidWebHost host;
        TextView tvConsole;
        Button btnOpenBrowser;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            XEPlatform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            tvConsole = FindViewById<TextView>(Resource.Id.tvConsole);

            btnOpenBrowser = FindViewById<Button>(Resource.Id.btnOpenBrowser);
            btnOpenBrowser.Visibility = ViewStates.Gone;
            btnOpenBrowser.Click += async (s, e) =>
            {
                await Browser.OpenAsync("http://localhost:5000");
            };

            Task.Run(() =>
            {
                try
                {
                    var host = Program.CreateWebHostBuilder(new string[0], PlatformLoggerProvider.Instance)
                      .Build();
                    this.host = new AndroidWebHost(host, WriteLine);
                    this.host.RunCompat();
                }
                catch (Exception e)
                {
                    WriteLine(e.ToString());
                }
            });
        }

        protected override void OnDestroy()
        {
            host?.Dispose();
            host = null;
            tvConsole = null;
            btnOpenBrowser = null;
            base.OnDestroy();
        }

        void WriteLine(string value)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                tvConsole.Append(value);
                tvConsole.Append(Environment.NewLine);
                if (value.Contains("Application started"))
                {
                    btnOpenBrowser.Visibility = ViewStates.Visible;
                }
            });
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            XEPlatform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
