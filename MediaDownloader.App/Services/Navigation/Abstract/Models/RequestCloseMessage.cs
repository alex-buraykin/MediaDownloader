using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MediaDownloader.App.Services.Navigation.Abstract.Models;

public class RequestCloseMessage
{
    public ObservableRecipient ViewModel { get; }
    public bool? DialogResult { get; }

    public RequestCloseMessage(
        ObservableRecipient viewModel,
        bool? dialogResult = null)
    {
        ViewModel = viewModel;
        DialogResult = dialogResult;
    }
}