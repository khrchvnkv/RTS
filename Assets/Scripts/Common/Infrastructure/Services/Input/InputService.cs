using System;
using Common.Infrastructure.Services.Input.Movement;
using Common.Infrastructure.Services.UpdateBehaviour;

namespace Common.Infrastructure.Services.Input
{
    public sealed class InputService : IInputService, IDisposable
    {
        private readonly IUpdateBehaviour _updateBehaviour;
        
        public ICharacterMovementInput MovementInput { get; }
        
        public InputService(IUpdateBehaviour updateBehaviour)
        {
            _updateBehaviour = updateBehaviour;
            MovementInput = new CharacterMovementInput();
            
            _updateBehaviour.OnUpdate += Update;
        }
        public void Dispose() => _updateBehaviour.OnUpdate -= Update;
        private void Update()
        {
            ReleaseCharacterMovementInput();
        }
        private void ReleaseCharacterMovementInput()
        {
            MovementInput.ReleaseInput();
        }
    }
}