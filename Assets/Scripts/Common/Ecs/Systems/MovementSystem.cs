using Common.Ecs.Components.Movement;
using Common.Ecs.Core;
using UnityEngine;

namespace Common.Ecs.Systems
{
    public sealed class MovementSystem : IUpdateSystem
    {
        private ComponentPool<MoveStateComponent> _moveStatePool;
        private ComponentPool<MoveSpeedComponent> _moveSpeedPool;
        private ComponentPool<MoveRotationComponent> _moveRotationPool;
        private ComponentPool<TransformComponent> _transformPool;

        public void OnUpdate(in int entity)
        {
            if (!_moveStatePool.HasComponent(entity)) return;

            ref var stateComponent = ref _moveStatePool.GetComponent(entity);
            if (!stateComponent.MoveRequired) return;

            ref var transformComponent = ref _transformPool.GetComponent(entity);
            ref var speedComponent = ref _moveSpeedPool.GetComponent(entity);
            ref var rotationComponent = ref _moveRotationPool.GetComponent(entity);

            transformComponent.Value.position += stateComponent.Direction * speedComponent.Value * Time.deltaTime;
            var rotation = transformComponent.Value.rotation;
            transformComponent.Value.rotation =
                Quaternion.RotateTowards(rotation, Quaternion.LookRotation(stateComponent.Direction),
                    rotationComponent.MaxDegreesDelta * Time.deltaTime);
        }
    }
} 