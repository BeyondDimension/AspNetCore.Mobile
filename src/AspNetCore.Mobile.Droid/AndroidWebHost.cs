using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Hosting
{
    public sealed class AndroidWebHost : IWebHost
    {
        readonly IWebHost host;
        readonly Action<string> writeLine;
        public AndroidWebHost(IWebHost host, Action<string> writeLine)
        {
            this.host = host;
            this.writeLine = writeLine;
        }

        public IFeatureCollection ServerFeatures => host.ServerFeatures;

        public IServiceProvider Services => host.Services;

        public void Dispose()
        {
            host.Dispose();
        }

        public void Start()
        {
            host.Start();
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            return host.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            return host.StopAsync(cancellationToken);
        }

        public void WriteLine(string value)
        {
            writeLine(value);
        }
    }
}