﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace MediaDownloader.App.Services.Navigation.Abstract.Models;

public class RequestCloseMessage : MessageBase
{
    public ViewModelBase ViewModel { get; }
    public bool? DialogResult { get; }

    public RequestCloseMessage(
        ViewModelBase viewModel,
        bool? dialogResult = null)
    {
        ViewModel = viewModel;
        DialogResult = dialogResult;
    }
}