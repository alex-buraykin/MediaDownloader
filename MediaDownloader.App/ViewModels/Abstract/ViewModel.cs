using System.Windows;
using GalaSoft.MvvmLight;

namespace MediaDownloader.App.ViewModels.Abstract;

public abstract class ViewModel<TView> : ViewModelBase, IViewModel<TView>
    where TView : FrameworkElement, new()
{
    private TView? _view;

    public abstract string Header { get; }

    public TView View
    {
        get => _view ??= new TView();
        set => _view = value;
    }
}