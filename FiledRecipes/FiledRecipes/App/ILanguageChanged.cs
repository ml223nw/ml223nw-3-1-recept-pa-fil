using System;

namespace FiledRecipes.App
{
    /// <summary>
    /// Defines members for changing language.
    /// </summary>
    public interface ILanguageChanged
    {
        event EventHandler LanguageChangedEvent;

        void ChangeLanguage();
    }
}
