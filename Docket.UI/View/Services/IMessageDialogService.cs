﻿namespace Docket.UI.View.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOkCancelDialog(string text, string title);
    }
}