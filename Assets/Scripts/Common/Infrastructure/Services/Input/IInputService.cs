using Common.Infrastructure.Services.Input.Movement;

namespace Common.Infrastructure.Services.Input
{
    public interface IInputService
    {
        ICharacterMovementInput MovementInput { get; }
    }
}