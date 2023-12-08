using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DotNetRepositoryTemplate.UI.ViewModels;

public abstract partial class WindowViewModelBase : ViewModelBase
{
    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private double top;

    [ObservableProperty]
    private double left;

    [ObservableProperty]
    private double width;

    [ObservableProperty]
    private double height;

    protected WindowViewModelBase(
        string title,
        double top = default,
        double left = default,
        double width = default,
        double height = default)
    {
        this.title = title;
        this.top = top;
        this.left = left;
        this.width = width;
        this.height = height;
    }

    public void Initialize()
    {
        this.OnInitialized();
    }

    public void ContentRendered()
    {
        this.OnContentRendered();
    }

    protected virtual void OnInitialized()
    {
        // Add initialization logic here
    }

    protected virtual void OnContentRendered()
    {
        // Add content rendered logic here
    }
}
