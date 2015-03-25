using System;

namespace FiledRecipes.App.Controls
{
    public class Panel : PanelBase
    {
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor Color { get; set; }

        public Panel(int width = 40)
            : base (width)
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
