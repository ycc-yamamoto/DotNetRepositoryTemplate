using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotNetRepositoryTemplate.Library;
using Microsoft.Win32;

namespace DotNetRepositoryTemplate.UI.ViewModels;

public sealed partial class MainViewModel : WindowViewModelBase
{
    [ObservableProperty]
    private string filePath;

    [ObservableProperty]
    private ObservableCollection<HashHistoryViewModel> hashHistories;

    public MainViewModel()
        : base("メイン画面", 100, 100, 500, 350)
    {
        this.filePath = string.Empty;
        this.hashHistories = new ObservableCollection<HashHistoryViewModel>();
    }

    [RelayCommand]
    private void OpenFile()
    {
        var dialog = new OpenFileDialog
        {
            Title = "ファイルを開く",
            Filter = "すべてのファイル (*.*)|*.*",
            Multiselect = false,
        };

        if (dialog.ShowDialog() is true)
        {
            this.FilePath = dialog.FileName;
        }
    }

    [RelayCommand]
    private async Task GetHashAsync()
    {
        if (string.IsNullOrEmpty(this.FilePath))
        {
            return;
        }

        var hash = await FileHash.GetHashCode(this.FilePath, default).ConfigureAwait(true);

        this.HashHistories.Add(new HashHistoryViewModel(this.FilePath, hash, DateTime.Now));
    }

    [RelayCommand]
    private void ClearHistories()
    {
        this.HashHistories.Clear();
    }
}
