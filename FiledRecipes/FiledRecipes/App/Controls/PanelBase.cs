using System;

namespace FiledRecipes.App.Controls
{
    public abstract class PanelBase
    {
        private int _width;

        public int Width
        {
            get { return _width; }
            set
            {
                _width = value; 
            }
        }

        public string Text { get; set; }

        public PanelBase(int width = 40)
        {
            Width = width;
        }
        
        public virtual void Show()
        {
            string frameLine = new String('═', Width);

            Console.WriteLine(" ╔═{0}═╗ ", frameLine);
            Console.WriteLine(" ║ {0} ║ ", Text.CenterAlignString(new String(' ', Width)));
            Console.WriteLine(" ╚═{0}═╝ ", frameLine);
        }
    }
}
