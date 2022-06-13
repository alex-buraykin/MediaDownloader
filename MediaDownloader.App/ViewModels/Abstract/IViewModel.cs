using System.ComponentModel;

namespace MediaDownloader.App.ViewModels.Abstract;

public interface IViewModel<out TView>: INotifyPropertyChanged
{
    string Header { get; }
    TView View { get; }
}