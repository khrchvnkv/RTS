using Common.Ecs;
using Common.Ecs.Commands;
using Common.Ecs.Components.Animation;
using Common.Ecs.Components.Movement;
using Common.Ecs.Core;
using Common.Infrastructure.Services.StaticData;
using Common.StaticData;
using UnityEngine;
using VContainer;

namespace Common.UnityLogic.Character
{
    public class CharacterEntity : Entity
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Animator _animator;

        [Header("Move to point")] 
        [SerializeField] private Transform _targetPoint;
        
        private CharacterStaticData _characterStaticData;

        [Inject]
        private void Construct(EcsWorld ecsWorld, IStaticDataService staticDataService)
        {
            Setup(ecsWorld);
            _characterStaticData = staticDataService.GameStaticData.CharacterStaticData;
            Init();
        }
        private void Init()
        {
            SetData(new MoveSpeedComponent
            {
                Value = _characterStaticData.MovementSpeed
            });
            SetData(new MoveRotationComponent
            {
                MaxDegreesDelta = _characterStaticData.MaxRotationDegreesDelta
            });
            SetData(new MoveStateComponent());
            SetData(new TransformComponent
            {
                Value = _transform 
            });
            SetData(new AnimatorComponent
            {
                Value = _animator
            });
        }
        [ContextMenu("Move to target")]
        private void MoveToTargetPoint()
        {
            if (_targetPoint is null) return;
            
            SetData(new MoveToPositionCommand
            {
                Destination = _targetPoint.position
            });
        }
    }
}