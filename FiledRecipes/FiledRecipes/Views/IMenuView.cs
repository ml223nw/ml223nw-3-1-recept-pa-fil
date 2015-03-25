using FiledRecipes.App.Controls;
using FiledRecipes.App.Input;
using FiledRecipes.App.Mvp;
using System;
using System.Collections.Generic;

namespace FiledRecipes.Views
{
    public interface IMenuView : IView
    {
        IEnumerable<IMenuItem> MenuItems { get; set; }
        ICommand GetCommand();
        void Show();
    }
}
