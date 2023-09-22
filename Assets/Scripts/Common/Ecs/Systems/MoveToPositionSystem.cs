using Common.Ecs.Commands;
using Common.Ecs.Components.Movement;
using Common.Ecs.Core;
using UnityEngine;

namespace Common.Ecs.Systems
{
    public sealed class MoveToPositionSystem : IUpdateSystem
    {
        private const float MIN_SQR_DISTANCE = 0.01f;
        private ComponentPool<MoveToPositionCommand> _moveToPositionPool;
        private ComponentPool<MoveStateComponent> _moveStatePool;
        private ComponentPool<TransformComponent> _transformPool;

        public void OnUpdate(in int entity)
        {
            if (!_moveToPositionPool.HasComponent(entity)) return;

            ref var moveToPositionCommand = ref _moveToPositionPool.GetComponent(entity);
            ref var moveStateComponent = ref _moveStatePool.GetComponent(entity);
            ref var transformComponent = ref _transformPool.GetComponent(entity);

            var startPosition = transformComponent.Value.position;
            var endPosition = moveToPositionCommand.Destination;

            var distanceVector = endPosition - startPosition;

            if (distanceVector.sqrMagnitude <= MIN_SQR_DISTANCE)
            {
                moveStateComponent.MoveRequired = false;
                moveStateComponent.Direction = Vector3.zero;
                _moveToPositionPool.RemoveComponent(entity);
            }
            else
            {
                moveStateComponent.MoveRequired = true;
                moveStateComponent.Direction = distanceVector.normalized;
            }
        }
    }
}