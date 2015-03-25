using FiledRecipes.App.Controls;
using FiledRecipes.App.Input;
using FiledRecipes.App.Mvp;
using FiledRecipes.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiledRecipes.Views
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuView : ViewBase, IMenuView
    {
        /// <summary>
        /// 
        /// </summary>
        private IMenu _menu;

        public IEnumerable<IMenuItem> MenuItems
        {
            get { return _menu.Items; }
            set { _menu.Items = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public MenuView(IMenu menu)
        {
            _menu = menu;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Show()
        {
            Console.Clear();
            ShowHeaderPanel();
            _menu.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICommand GetCommand()
        {
            do
            {
                try
                {
                    Show();
                    return _menu.GetCommand();
                }
                catch (Exception)
                {
                    ShowPanel(Strings.InvalidMenuChoice, MessagePanelOptions.Alert);
                    ContinueOnKeyPressed();
                }                
            } while (true);
        }
    }
}
