using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MediaDownloader.App.ViewModels.Abstract;

public abstract class ViewModel<TView> : ObservableRecipient, IViewModel<TView>
    where TView : FrameworkElement, new()
{
    private TView? _view;

    public virtual string Header => string.Empty;

    public TView View => _view ??= new TView { DataContext = this };
}