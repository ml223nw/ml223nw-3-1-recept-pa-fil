using FiledRecipes.App.Controls;
using FiledRecipes.App.Input;
using FiledRecipes.App.Mvp;
using FiledRecipes.Domain;
using FiledRecipes.Models;
using FiledRecipes.Presenters;
using FiledRecipes.Views;
using Microsoft.Practices.Unity;
using System.Configuration;

namespace FiledRecipes
{
    /// <summary>
    /// 
    /// </summary>
    internal class ContainerBootstrapper
    {
        /// <summary>
        /// Initializes the container.
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterTypes(IUnityContainer container)
        {
            // Repository object with singleton behavior.
            container.RegisterType<IRecipeRepository, RecipeRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(ConfigurationManager.AppSettings["RecipesPath"]));

            // Main menu - MVP and command objects.
            container.RegisterType<IMenuModel, MainMenuModel>(
                "MainMenuModel",
                new InjectionConstructor(
                    typeof(IRecipeRepository),
                    new ResolvedArrayParameter<ICommand>(
                        new ResolvedParameter<ExitCommand>(),
                        new ResolvedParameter<OpenCommand>(),
                        new ResolvedParameter<SaveCommand>(),
                        new ResolvedParameter<DeleteListCommand>(),
                        new ResolvedParameter<ViewListCommand>(),
                        new ResolvedParameter<ViewAllCommand>(),
                        new ResolvedParameter<ChangeLanguageCommand>()
                    ),
                    typeof(IToolsPresenter)
                ));

            container.RegisterType<IMenuPresenter, MenuPresenter>(
                "MainMenu",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IMenuModel>("MainMenuModel"),
                    typeof(IMenuView)
                )
            );

            // Delete recipe menu - MVP and command objects.
            container.RegisterType<IMenuModel, RecipeMenuModel>(
                "RecipeDeleteMenuModel",
                new InjectionConstructor(
                    typeof(IRecipeRepository),
                    new ResolvedArrayParameter<ICommand>(
                        new ResolvedParameter<CancelCommand>(),
                        new ResolvedParameter<DeleteCommand>()
                    ),
                    typeof(IToolsPresenter)
                ));

            container.RegisterType<IMenuPresenter, MenuPresenter>(
                "RecipeDeleteMenu",
                new InjectionConstructor(
                    new ResolvedParameter<IMenuModel>("RecipeDeleteMenuModel"),
                    typeof(IMenuView)
                )
            );

            // View recipe menu - MVP and command objects.
            container.RegisterType<IMenuModel, RecipeMenuModel>(
                "RecipeViewMenuModel",
                new InjectionConstructor(
                    typeof(IRecipeRepository),
                    new ResolvedArrayParameter<ICommand>(
                        new ResolvedParameter<CancelCommand>(),
                        new ResolvedParameter<ViewCommand>()
                    ),
                    typeof(IToolsPresenter)
                ));

            container.RegisterType<IMenuPresenter, MenuPresenter>(
                "RecipeViewMenu",
                new InjectionConstructor(
                    new ResolvedParameter<IMenuModel>("RecipeViewMenuModel"),
                    typeof(IMenuView)
                )
            );

            // Tools
            container.RegisterType<IToolsPresenter, ToolsPresenter>();

            // Common objects.
            container.RegisterType<IMenu, Menu>();
            container.RegisterType<IMenuView, MenuView>();
            container.RegisterType<ToolsPresenter>(
                new ContainerControlledLifetimeManager()
            );

            // MVP objects handling recipes.
            container.RegisterType<IRecipePresenter, RecipePresenter>();
            container.RegisterType<IRecipeView, RecipeView>();

            // Commands showing a recipe menu, i.e. a list of recipes.
            container.RegisterType<DeleteListCommand>(
                new InjectionConstructor(
                    new ResolvedParameter<IMenuPresenter>("RecipeDeleteMenu")
                )
            );

            container.RegisterType<ViewListCommand>(
                new InjectionConstructor(
                    new ResolvedParameter<IMenuPresenter>("RecipeViewMenu")
                )
            );
        }
    }
}
