using System;

namespace FiledRecipes.App.Controls
{
    public enum MessagePanelOptions { Basic, Info, Success, Warning, Alert };

    public class MessagePanel : PanelBase
    {

        private ConsoleColor BackgroundColor
        {
            get
            {
                switch (Options)
                {
                    case MessagePanelOptions.Basic:
                        goto default;

                    case MessagePanelOptions.Info:
                        return ConsoleColor.Blue;

                    case MessagePanelOptions.Success:
                        return ConsoleColor.DarkGreen;

                    case MessagePanelOptions.Warning:
                        return ConsoleColor.Yellow;

                    case MessagePanelOptions.Alert:
                        return ConsoleColor.Red;

                    default:
                        return ConsoleColor.Black;
                }
            }
        }

        private ConsoleColor Color
        {
            get
            {  
                switch (Options)
                {
                    case MessagePanelOptions.Basic:
                        goto default;

                    case MessagePanelOptions.Info:
                        return ConsoleColor.White;

                    case MessagePanelOptions.Success:
                        return ConsoleColor.White;

                    case MessagePanelOptions.Warning:
                        return ConsoleColor.Black;

                    case MessagePanelOptions.Alert:
                        return ConsoleColor.White;

                    default:
                        return ConsoleColor.Gray;
                }
            }
        }

        public MessagePanelOptions Options { get; set; }

        public MessagePanel(int width = 40)
            : base(width)
        {
        }

        public override void Show()
        {
            ConsoleColor prevBackgroundColor = Console.BackgroundColor;
            ConsoleColor prevForegroundColor = Console.ForegroundColor;

            Console.ForegroundColor = Color;
            Console.BackgroundColor = BackgroundColor;

            base.Show();

            Console.ForegroundColor = prevForegroundColor;
            Console.BackgroundColor = prevBackgroundColor;
        }
    }
}
