using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using MediaDownloader.App.Services.Navigation.Abstract;
using MediaDownloader.App.Services.Navigation.Abstract.Models;
using MediaDownloader.App.ViewModels.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace MediaDownloader.App.Services.Navigation;

public class ViewService : IViewService, IDisposable
{
    private readonly IMessenger _messenger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IList<Window> _openedWindows;

    public ViewService(
        IMessenger messenger,
        IServiceProvider serviceProvider)
    {
        _messenger = messenger;
        _serviceProvider = serviceProvider;

        _openedWindows = new List<Window>();

        // Listen for the close event
        messenger.Register<ViewService, RequestCloseMessage>(this, static (r, m) => r.OnRequestClose(m));
    }

    [DebuggerStepThrough]
    public void OpenWindow<TViewModel, TView>()
            where TViewModel : IViewModel<TView>
            where TView : Window, new()
        {
            // Create window for that view tabModel.
            var window = CreateWindow<TViewModel, TView>(WindowMode.Window);

            // Open the window.
            window.Show();
        }

        [DebuggerStepThrough]
        public void OpenWindow<TViewModel, TView>(TViewModel viewModel)
            where TViewModel : IViewModel<TView>
            where TView : Window, new()
        {
            // Create window for that view tabModel.
            var window = CreateWindow<TViewModel, TView>(viewModel, WindowMode.Window);

            // Open the window.
            window.Show();
        }

        [DebuggerStepThrough]
        public bool? OpenDialog<TViewModel, TView>()
            where TViewModel : IViewModel<TView>
            where TView : Window, new()
        {
            // Create window for that viewModel.
            var window = CreateWindow<TViewModel, TView>(WindowMode.Dialog);

            // Open the window and return the result.
            return window.ShowDialog();
        }

        [DebuggerStepThrough]
        public bool? OpenDialog<TViewModel, TView>(TViewModel viewModel)
            where TViewModel : IViewModel<TView>
            where TView : Window, new()
        {
            // Create window for that viewModel.
            var window = CreateWindow<TViewModel, TView>(viewModel, WindowMode.Dialog);

            // Open the window and return the result.
            return window.ShowDialog();
        }

        [DebuggerStepThrough]
        public Window CreateWindow<TViewModel, TView>(WindowMode windowMode)
            where TViewModel : IViewModel<TView>
            where TView : Window, new()
        {
            var viewModel = _serviceProvider.GetService<TViewModel>()!;

            return CreateWindow<TViewModel, TView>(viewModel, windowMode);
        }

        [DebuggerStepThrough]
        public Window CreateWindow<TViewModel, TView>(TViewModel viewModel, WindowMode windowMode)
            where TViewModel : IViewModel<TView>
            where TView : Window, new()
        {
            var window = viewModel.View;
            window.DataContext = viewModel;
            window.Closed += OnClosed!;

            lock (_openedWindows)
            {
                // Last window opened is considered the 'owner' of the window.
                // May not be 100% correct in some situations but it is more
                // then good enough for handling dialog windows
                if (windowMode == WindowMode.Dialog && _openedWindows.Count > 0)
                {
                    var lastOpened = _openedWindows[^1];

                    if (lastOpened.IsActive && !Equals(window, lastOpened))
                        window.Owner = lastOpened;
                }

                _openedWindows.Add(window);
            }

            return window;
        }

        public int GetOpenedWindowsCount()
        {
            lock (_openedWindows)
            {
                return _openedWindows.Count;
            }
        }

        private void OnRequestClose(RequestCloseMessage message)
        {
            var window = _openedWindows.SingleOrDefault(w => w.DataContext == message.ViewModel);
            if (window == null) return;

            if (message.DialogResult != null)
                window.DialogResult = message.DialogResult;
            else
                window.Close();
        }

        private void OnClosed(object sender, EventArgs e)
        {
            var window = (Window) sender;
            window.Closed -= OnClosed!;

            lock (_openedWindows)
            {
                _openedWindows.Remove(window);
            }
        }

        public void Dispose()
        {
            _messenger.Unregister<RequestCloseMessage>(this);
        }
}