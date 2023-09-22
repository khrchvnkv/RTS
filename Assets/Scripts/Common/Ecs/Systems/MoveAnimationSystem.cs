using Common.Ecs.Components.Animation;
using Common.Ecs.Components.Movement;
using Common.Ecs.Core;
using UnityEngine;

namespace Common.Ecs.Systems
{
    public sealed class MoveAnimationSystem : ILateUpdateSystem
    {
        private const string AnimatorMovingBoolKey = "IsMoving";
        
        private static readonly int IsMovingHash = Animator.StringToHash(AnimatorMovingBoolKey);
        
        private ComponentPool<AnimatorComponent> _animatorPool;
        private ComponentPool<MoveStateComponent> _moveStatePool;

        public void OnLateUpdate(in int entity)
        {
            if (!_animatorPool.HasComponent(entity) || !_moveStatePool.HasComponent(entity)) return;
            
            ref var animatorComponent = ref _animatorPool.GetComponent(entity);
            ref var moveStateComponent = ref _moveStatePool.GetComponent(entity);
                
            animatorComponent.Value.SetBool(IsMovingHash, moveStateComponent.MoveRequired);
        }
    }
}