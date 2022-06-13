using System.Windows;
using MediaDownloader.App.Services.Navigation.Abstract.Models;
using MediaDownloader.App.ViewModels.Abstract;

namespace MediaDownloader.App.Services.Navigation.Abstract;

public interface IViewService
{
    void OpenWindow<TViewModel, TView>()
        where TViewModel : IViewModel<TView>
        where TView : Window, new();

    void OpenWindow<TViewModel, TView>(TViewModel viewModel)
        where TViewModel : IViewModel<TView>
        where TView : Window, new();

    bool? OpenDialog<TViewModel, TView>()
        where TViewModel : IViewModel<TView>
        where TView : Window, new();

    bool? OpenDialog<TViewModel, TView>(TViewModel viewModel)
        where TViewModel : IViewModel<TView>
        where TView : Window, new();

    Window CreateWindow<TViewModel, TView>(WindowMode windowMode)
        where TViewModel : IViewModel<TView>
        where TView : Window, new();

    Window CreateWindow<TViewModel, TView>(TViewModel viewModel, WindowMode windowMode)
        where TViewModel : IViewModel<TView>
        where TView : Window, new();

    int GetOpenedWindowsCount();
}