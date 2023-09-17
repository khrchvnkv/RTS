using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.StaticData;

namespace Common.Infrastructure.StateMachine.States
{
    /// <summary>
    /// Loading progression data
    /// </summary>
    public class BootstrapState : State, IState
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;

        public BootstrapState(IStaticDataService staticDataService, IUIFactory uiFactory)
        {
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
        }
        public void Enter()
        {
            _staticDataService.Load();
            _uiFactory.CreateUIRoot();
            _uiFactory.ShowLoadingCurtain();
            StateMachine.Enter<LoadMainMenuState, string>(Constants.Scenes.MainMenuScene);
        }
        public override void Exit()
        {
        }
    }
}