using FiledRecipes.Domain;
using FiledRecipes.Presenters;
using System;

namespace FiledRecipes.App.Input
{
    public class ViewListCommand : CommandBase
    {
        private IMenuPresenter _receiver;

        public ViewListCommand(IMenuPresenter receiver)
        {
            _receiver = receiver;
        }

        public override void Execute()
        {
            // Show the recipe menu, get and execute the command.
            ICommand command;
            while (!((command = _receiver.GetCommand()) is CancelCommand))
            {
                command.Execute();
            }
        }
    }
}
