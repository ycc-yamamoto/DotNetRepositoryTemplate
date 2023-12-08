using System;
using System.Windows;
using DotNetRepositoryTemplate.UI.ViewModels;

namespace DotNetRepositoryTemplate.UI.Views;

public class ViewBase : Window
{
    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);

        if (this.DataContext is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

        if (this.DataContext is WindowViewModelBase viewModel)
        {
            viewModel.Initialize();
        }
    }

    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);

        if (this.DataContext is WindowViewModelBase viewModel)
        {
            viewModel.ContentRendered();
        }
    }
}
