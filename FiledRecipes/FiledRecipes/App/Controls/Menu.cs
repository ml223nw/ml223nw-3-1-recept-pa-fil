using FiledRecipes.App.Input;
using FiledRecipes.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiledRecipes.App.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class Menu : IMenu
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IMenuItem> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void Show()
        {
            foreach (IMenuItem menuItem in Items)
            {
                if (!menuItem.IsSeparator)
                {
                    Console.WriteLine(" {0,2}. {1}", menuItem.Index, menuItem.Text);
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(menuItem.Text))
                    {
                        string text = menuItem.Text.Trim() + " ";
                        Console.WriteLine("\n - {0}\n", text.PadRight(42, '-'));
                    }
                    else
                    {
                        Console.WriteLine("\n --------------------------------------------\n");
                    }
                }
            }
            Console.WriteLine("\n ════════════════════════════════════════════\n");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICommand GetCommand()
        {
            Console.Write(" {0}: ", Strings.ChooseCommand);

            int key;
            if (int.TryParse(Console.ReadLine(), out key) && Items.Any(mi => !mi.IsSeparator && mi.Index == key))
            {
                return Items.Single(mi => !mi.IsSeparator && mi.Index == key).Command;
            }

            Console.WriteLine();

            throw new ApplicationException(Strings.InvalidMenuChoice);
        }
    }
}
