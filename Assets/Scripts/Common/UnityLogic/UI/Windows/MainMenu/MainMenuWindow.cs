using Common.Infrastructure.StateMachine;
using Common.Infrastructure.StateMachine.States;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Common.UnityLogic.UI.Windows.MainMenu
{
    public class MainMenuWindow : WindowBase<MainMenuWindowData>
    {
        [SerializeField] private Button _startButton;

        private GameStateMachine _gameStateMachine;
        
        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        protected override void Subscribe()
        {
            base.Subscribe();
            _startButton.onClick.AddListener(EnterLoadLevelState);
        }
        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            _startButton.onClick.RemoveListener(EnterLoadLevelState);
        }
        private void EnterLoadLevelState()
        {
            _gameStateMachine.Enter<LoadLevelState>();
        }
    }
}