using FiledRecipes.App.Controls;
using FiledRecipes.Properties;
using System;

namespace FiledRecipes.App.Mvp
{
    /// <summary>
    /// Represents the members that are needed to render a view.
    /// </summary>
    public abstract class ViewBase : IView
    {
        /// <summary>
        /// Gets or sets the header of the view.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Pauses the execution of the application and waits for the user to press 
        /// any key to continue.
        /// </summary>
        public virtual void ContinueOnKeyPressed()
        {
            ShowPanel(Strings.PressAnyKeyToContinue, MessagePanelOptions.Info);
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.Clear();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// Renders a header panel.
        /// </summary>
        public virtual void ShowHeaderPanel()
        {
            Panel panel = new Panel
            {
                Text = Header,
                BackgroundColor = ConsoleColor.DarkCyan,
                Color = ConsoleColor.White,
            };
            panel.Show();
        }

        /// <summary>
        /// Reanders a panel with text.
        /// </summary>
        /// <param name="text">The text to be rendered in the panel.</param>
        /// <param name="options">The options that determines how the panel is rendered.</param>
        public virtual void ShowPanel(string text, MessagePanelOptions options)
        {
            MessagePanel messagePanel = new MessagePanel
            {
                Text = text,
                Options = options
            };
            Console.WriteLine();
            messagePanel.Show();
        }

        /// <summary>
        /// Renders a panel with text.
        /// </summary>
        /// <param name="text">The text to be rendered in the panel.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="backgroundColor">The color of the panel.</param>
        public virtual void ShowPanel(string text, ConsoleColor color, ConsoleColor backgroundColor)
        {
            Panel panel = new Panel
            {
                Text = text,
                Color = color,
                BackgroundColor = backgroundColor
            };
            Console.WriteLine();
            panel.Show();
        }
    }
}
