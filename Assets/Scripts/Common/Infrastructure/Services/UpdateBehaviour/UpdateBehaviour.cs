using System;
using UnityEngine;

namespace Common.Infrastructure.Services.UpdateBehaviour
{
    public class UpdateBehaviour : MonoBehaviour, IUpdateBehaviour
    {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;

        private void Update() => OnUpdate?.Invoke();
        private void FixedUpdate() => OnFixedUpdate?.Invoke();
        private void LateUpdate() => OnLateUpdate?.Invoke();
    }
}