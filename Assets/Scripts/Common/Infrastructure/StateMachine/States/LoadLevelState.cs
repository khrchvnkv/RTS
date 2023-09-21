using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.SceneLoading;
using Common.UnityLogic.UI.Windows.MainMenu;

namespace Common.Infrastructure.StateMachine.States
{
    public class LoadLevelState : State, IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly ISceneLoader _sceneLoader;

        public LoadLevelState(IUIFactory uiFactory, ISceneLoader sceneLoader)
        {
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
        }
        public void Enter()
        {
            _uiFactory.ShowLoadingCurtain();
            _uiFactory.Hide(new MainMenuWindowData());
            _sceneLoader.LoadScene(Constants.Scenes.GameScene, OnGameSceneLoaded);
        }
        public override void Exit()
        { }
        private void OnGameSceneLoaded()
        {
            StateMachine.Enter<GameLoopState>();
        }
    }
}