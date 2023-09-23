using System;
using Common.UnityLogic.Character;
using UnityEngine;

namespace Common.Infrastructure.Services.Input.Movement
{
    public class CharacterMovementInput : ICharacterMovementInput
    {
        private Camera _mainCamera;
        private bool _isActive;

        public event Action<Vector3> OnCharacterMoveCommandReleased;

        public void ActivateMovementInput(Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _isActive = true;
        }
        public void DeactivateMovementInput()
        {
            _isActive = false;
        }
        public void ReleaseInput()
        {
            if (!_isActive) return;
            
            if (_mainCamera is null)
            {
                Debug.LogError("Character movement input active, but camera ref is null");
                return;
            }
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = _mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
        
                if (Physics.Raycast(ray, out hit) && 
                    !hit.collider.gameObject.TryGetComponent(out CharacterEntity _))
                {
                    OnCharacterMoveCommandReleased?.Invoke(hit.point);
                }
            }
        }
    }
}