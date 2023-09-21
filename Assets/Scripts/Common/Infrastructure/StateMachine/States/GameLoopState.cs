using System;
using Common.Ecs;
using Common.Infrastructure.Factories.UIFactory;

namespace Common.Infrastructure.StateMachine.States
{
    public class GameLoopState : State, IState, IDisposable
    {
        private readonly IUIFactory _uiFactory;
        private readonly EcsWorld _ecsWorld;

        public GameLoopState(IUIFactory uiFactory, EcsWorld ecsWorld)
        {
            _uiFactory = uiFactory;
            _ecsWorld = ecsWorld;
        }
        public void Enter()
        {
            _ecsWorld.Init();
            _uiFactory.HideLoadingCurtain();
        }
        public override void Exit()
        { }
        public void Dispose() => _ecsWorld?.Dispose();
    }
}