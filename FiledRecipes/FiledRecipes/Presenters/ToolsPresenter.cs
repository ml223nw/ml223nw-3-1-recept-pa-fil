using System;
using System.Globalization;

namespace FiledRecipes.Presenters
{
    /// <summary>
    /// 
    /// </summary>
    public class ToolsPresenter : IToolsPresenter
    {
        /// Occurs after change of the language.
        public event EventHandler LanguageChangedEvent;

        /// <summary>
        /// Changes the language from swedish to english, or vice versa.
        /// </summary>
        public void ChangeLanguage()
        {
            CultureInfo ci;
            if (CultureInfo.CurrentUICulture.Name == "sv-SE")
            {
                ci = new CultureInfo("en-US");
            }
            else
            {
                ci = new CultureInfo("sv-SE");
            }

            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            
            OnLanguageChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the RecipesChanged event.
        /// </summary>
        /// <param name="e">The EventArgs that contains the event data.</param>
        protected virtual void OnLanguageChanged(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of 
            // a race condition if the last subscriber unsubscribes 
            // immediately after the null check and before the event is raised.
            EventHandler handler = LanguageChangedEvent;

            // Event will be null if there are no subscribers. 
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, e);
            }
        }
    }
}
