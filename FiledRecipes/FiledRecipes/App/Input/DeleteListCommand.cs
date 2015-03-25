using FiledRecipes.Domain;
using FiledRecipes.Presenters;

namespace FiledRecipes.App.Input
{
    public class DeleteListCommand : CommandBase
    {
        private IMenuPresenter _receiver;

        public DeleteListCommand(IMenuPresenter receiver)
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
