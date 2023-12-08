using DotNetRepositoryTemplate.UI.ViewModels;

namespace DotNetRepositoryTemplate.UI.Views;

public partial class MainView
{
    public MainView(MainViewModel viewModel)
    {
        this.DataContext = viewModel;
        this.InitializeComponent();
    }
}

