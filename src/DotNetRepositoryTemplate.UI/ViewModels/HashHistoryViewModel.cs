using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DotNetRepositoryTemplate.UI.ViewModels;

public sealed partial class HashHistoryViewModel : ViewModelBase
{
    [ObservableProperty]
    private string filePath;

    [ObservableProperty]
    private string hash;

    [ObservableProperty]
    private DateTime dateTime;

    public HashHistoryViewModel(string filePath, string hash, DateTime dateTime)
    {
        this.filePath = filePath;
        this.hash = hash;
        this.dateTime = dateTime;
    }
}
