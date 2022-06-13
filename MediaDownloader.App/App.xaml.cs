using System;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using MediaDownloader.App.Services.Navigation;
using MediaDownloader.App.Services.Navigation.Abstract;
using MediaDownloader.App.Services.Navigation.Abstract.Models;
using MediaDownloader.App.ViewModels;
using MediaDownloader.App.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MediaDownloader.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(context.Configuration, services);
                })
                .Build();
        }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<IMessenger, Messenger>();
            services.AddSingleton<IViewService, ViewService>();
            services.AddSingleton<MainViewModel>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var viewService = _host.Services.GetRequiredService<IViewService>();
            var mainWindow = viewService.CreateWindow<MainViewModel, MainWindow>(WindowMode.Window);
            Current.MainWindow = mainWindow;
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}