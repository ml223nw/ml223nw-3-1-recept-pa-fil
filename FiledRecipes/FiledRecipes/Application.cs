using Microsoft.Practices.Unity;
using FiledRecipes.App.Input;
using FiledRecipes.Presenters;
using System.Globalization;

namespace FiledRecipes
{
    /// <summary>
    /// Provides members to manage an application. This class cannot be inherited.
    /// </summary>
    public sealed class Application
    {
        /// <summary>
        /// Begins running the main menu application loop.
        /// </summary>
        public void Run()
        {
            // Set the culture (used by the Resource Manager to look up 
            // culture-specific resources).
            System.Threading.Thread.CurrentThread.CurrentCulture = 
                System.Threading.Thread.CurrentThread.CurrentUICulture = 
                CultureInfo.CurrentCulture;

            // Create the Unity container,...
            using (IUnityContainer container = new UnityContainer())
            {
                // ...initialize it and let it create depending objects.            
                ContainerBootstrapper.RegisterTypes(container);
                IMenuPresenter presenter = container.Resolve<IMenuPresenter>("MainMenu");

                // Start the application loop - show the main menu, get the command and 
                // act on it.
                ICommand command;
                while (!((command = presenter.GetCommand()) is ExitCommand))
                {
                    command.Execute();
                }
            }
        }
    }
}
