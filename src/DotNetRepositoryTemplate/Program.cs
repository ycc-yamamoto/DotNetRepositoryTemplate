using System.Threading;
using DotNetRepositoryTemplate.Services.Impl;
using DotNetRepositoryTemplate.UI;
using DotNetRepositoryTemplate.UI.ViewModels;
using DotNetRepositoryTemplate.UI.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
Thread.CurrentThread.SetApartmentState(ApartmentState.STA);

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureAppConfiguration((_, config) =>
{
    config.AddCommandLine(args);
});
builder.ConfigureServices((_, services) =>
{
    services.AddHostedService<AppHostedService<App, MainView>>();
    services.AddTransient<App>();
    services.AddTransient<MainView>();
    services.AddTransient<MainViewModel>();
});

var host = builder.Build();

await host.RunAsync().ConfigureAwait(false);
