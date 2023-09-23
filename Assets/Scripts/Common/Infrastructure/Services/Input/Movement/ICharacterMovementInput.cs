using System;
using UnityEngine;

namespace Common.Infrastructure.Services.Input.Movement
{
    public interface ICharacterMovementInput
    {
        public event Action<Vector3> OnCharacterMoveCommandReleased;

        void ActivateMovementInput(Camera mainCamera);
        void DeactivateMovementInput();
        void ReleaseInput();
    }
}