using FiledRecipes.Presenters;

namespace FiledRecipes.App.Input
{
    public class ChangeLanguageCommand : CommandBase
    {
        private IToolsPresenter _receiver;

        public ChangeLanguageCommand(IToolsPresenter receiver)
        {
            _receiver = receiver;
        }

        public override void Execute()
        {
            _receiver.ChangeLanguage();
        }
    }
}
