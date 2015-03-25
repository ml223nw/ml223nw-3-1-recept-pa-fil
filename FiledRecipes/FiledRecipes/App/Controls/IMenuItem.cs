using FiledRecipes.App.Input;

namespace FiledRecipes.App.Controls
{
    public interface IMenuItem
    {
        ICommand Command { get; set; }
        int Index { get; set; }
        bool IsSeparator { get; set; }
        bool Enabled { get; set; }
        string Text { get; set; }
        string ToString();
    }
}
