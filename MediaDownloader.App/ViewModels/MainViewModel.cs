using MediaDownloader.App.ViewModels.Abstract;
using MediaDownloader.App.ViewModels.Input;
using MediaDownloader.App.Views;

namespace MediaDownloader.App.ViewModels;

public class MainViewModel : ViewModel<MainWindow>
{
    public override string Header => "Media Downloader";

    public InputUrlViewModel InputUrlViewModel { get; }

    public MainViewModel(InputUrlViewModel inputUrlViewModel)
    {
        InputUrlViewModel = inputUrlViewModel;
    }
}