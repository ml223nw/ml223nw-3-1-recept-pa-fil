using FiledRecipes.App.Controls;
using System;

namespace FiledRecipes.App.Mvp
{
    /// <summary>
    /// Defines the members required for a view.
    /// </summary>
    public interface IView
    {
        void ContinueOnKeyPressed();
        string Header { get; set; }
        void ShowHeaderPanel();
        void ShowPanel(string text, MessagePanelOptions options);
        void ShowPanel(string text, ConsoleColor color, ConsoleColor backgroundColor);
    }
}
