using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.Input;
using UnityEngine;

namespace Common.Infrastructure.StateMachine.States
{
    public class GameLoopState : State, IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly IInputService _inputService;

        public GameLoopState(IUIFactory uiFactory, IInputService inputService)
        {
            _uiFactory = uiFactory;
            _inputService = inputService;
        }
        public void Enter()
        {
            _uiFactory.HideLoadingCurtain();
            _inputService.MovementInput.ActivateMovementInput(Camera.main);
        }
        public override void Exit()
        {
            _inputService.MovementInput.DeactivateMovementInput();
        }
    }
}