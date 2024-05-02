using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServerInterface.Services;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ServerInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebApplication? webApplication;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartServer()
        {
            var builder = WebApplication.CreateBuilder();
            // Add services to the container.
            builder.Services.AddGrpc();
            builder.WebHost.UseUrls("http://*:5118");
            builder.Services.AddSingleton(x => 
            new InputService(x.GetRequiredService<ILogger<InputService>>(), myCanvas, rect));

            webApplication = builder.Build();

            webApplication.Lifetime.ApplicationStarted.Register(() => state.Dispatcher.Invoke(() => state.Text = "Started"));
            webApplication.Lifetime.ApplicationStopping.Register(() => state.Dispatcher.Invoke(() => state.Text = "Stopping"));
            webApplication.Lifetime.ApplicationStopped.Register(() => state.Dispatcher.Invoke(() => state.Text = "Stopped"));

            // Configure the HTTP request pipeline.
            webApplication.MapGrpcService<InputService>();
            webApplication.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            Task.Factory.StartNew(() => webApplication.Run());
        }

        private void showHost_Click(object sender, RoutedEventArgs e)
        {
            host.Text = string.Join(' ', AppData.GetApi()) + "\n";
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            StartServer();
        }
    }
}