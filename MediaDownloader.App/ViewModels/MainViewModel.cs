using MediaDownloader.App.ViewModels.Abstract;
using MediaDownloader.App.Views;

namespace MediaDownloader.App.ViewModels;

public class MainViewModel : ViewModel<MainWindow>
{
    public override string Header => "Media Downloader";
}