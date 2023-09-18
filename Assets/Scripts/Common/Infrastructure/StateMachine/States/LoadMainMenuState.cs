using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.SceneLoading;

namespace Common.Infrastructure.StateMachine.States
{
    public class LoadMainMenuState : State, IPayloadedState<string>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;

        public LoadMainMenuState(ISceneLoader sceneLoader, IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
        }
        public void Enter(string payload)
        {
            _sceneLoader.LoadScene(payload, OnLoaded);
        }
        public override void Exit()
        { }
        private void OnLoaded() => _uiFactory.HideLoadingCurtain();
    }
}