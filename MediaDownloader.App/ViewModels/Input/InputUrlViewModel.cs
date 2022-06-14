using System.Threading.Tasks;
using MediaDownloader.App.ViewModels.Abstract;
using MediaDownloader.App.Views.Input;
using MediaDownloader.Framework;
using Microsoft.Toolkit.Mvvm.Input;

namespace MediaDownloader.App.ViewModels.Input;

public class InputUrlViewModel : ViewModel<InputUrlView>
{
    private IAsyncRelayCommand<string?>? _downloadCommand;
    private string? _url;

    public string? Url
    {
        get => _url;
        set => SetProperty(ref _url, value);
    }

    public IAsyncRelayCommand<string?> DownloadCommand
        => _downloadCommand ??= new AsyncRelayCommand<string?>(OnDownloadAsync, url => !url.IsNullOrWhiteSpace());

    private Task OnDownloadAsync(string? url)
    {
        return Task.CompletedTask;
    }
}