using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiledRecipes.App.Input
{
    public interface ICommand
    {
        object Argument { get; set; }
        void Execute();
    }
}
