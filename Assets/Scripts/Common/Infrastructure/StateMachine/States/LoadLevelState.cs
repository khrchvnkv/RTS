using Common.Ecs;
using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.SceneLoading;
using Common.UnityLogic.UI.Windows.MainMenu;

namespace Common.Infrastructure.StateMachine.States
{
    public class LoadLevelState : State, IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly ISceneLoader _sceneLoader;
        private readonly EcsWorld _ecsWorld;

        public LoadLevelState(IUIFactory uiFactory, ISceneLoader sceneLoader, EcsWorld ecsWorld)
        {
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
            _ecsWorld = ecsWorld;
        }
        public void Enter()
        {
            _ecsWorld.Init();
            _uiFactory.ShowLoadingCurtain();
            _uiFactory.Hide(new MainMenuWindowData());
            _sceneLoader.LoadScene(Constants.Scenes.GameScene, OnGameSceneLoaded);
        }
        public override void Exit()
        { }
        private void OnGameSceneLoaded() => StateMachine.Enter<GameLoopState>();
    }
}