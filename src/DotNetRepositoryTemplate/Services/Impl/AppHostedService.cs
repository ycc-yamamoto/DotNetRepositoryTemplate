using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DotNetRepositoryTemplate.Logs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetRepositoryTemplate.Services.Impl;

public class AppHostedService<TApplication, TView> : IHostedService
    where TApplication : Application
    where TView : Window
{
    private readonly IHostApplicationLifetime hostApplicationLifetime;

    private readonly ILogger<AppHostedService<TApplication, TView>> logger;

    private readonly TApplication application;

    private readonly TView view;

    public AppHostedService(
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<AppHostedService<TApplication, TView>> logger,
        TApplication application,
        TView view)
    {
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.logger = logger;
        this.application = application;
        this.view = view;
        this.view.Closed += this.OnClosedMainView;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.hostApplicationLifetime.ApplicationStarted.Register(this.OnStarted);
        this.hostApplicationLifetime.ApplicationStopping.Register(this.OnStopping);
        this.hostApplicationLifetime.ApplicationStopped.Register(this.OnStopped);
        this.application.Run(this.view);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this.application.Shutdown();
        return Task.CompletedTask;
    }

    private async void OnClosedMainView(object sender, EventArgs e)
    {
        await this.StopAsync(default).ConfigureAwait(false);
        this.view.Closed -= this.OnClosedMainView;
    }

    private void OnStarted()
    {
        this.logger.OnStartedHost();
    }

    private void OnStopping()
    {
        this.logger.OnStoppingHost();
    }

    private void OnStopped()
    {
        this.logger.OnStoppedHost();
    }
}
