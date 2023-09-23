using Common.Ecs;
using Common.Ecs.Commands;
using Common.Ecs.Components.Animation;
using Common.Ecs.Components.Movement;
using Common.Ecs.Core;
using Common.Infrastructure.Services.Input;
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

        private IInputService _inputService;
        private CharacterStaticData _characterStaticData;

        [Inject]
        private void Construct(EcsWorld ecsWorld, IStaticDataService staticDataService,
            IInputService inputService)
        {
            Setup(ecsWorld);
            _characterStaticData = staticDataService.GameStaticData.CharacterStaticData;
            _inputService = inputService;
            Init();
        }
        public override void Dispose()
        {
            base.Dispose();
            _inputService.MovementInput.OnCharacterMoveCommandReleased -= MoveToPoint;
        }
        private void Init()
        {
            _inputService.MovementInput.OnCharacterMoveCommandReleased += MoveToPoint;
            SetupEntityData();
        }
        private void SetupEntityData()
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
        private void MoveToPoint(Vector3 point)
        {
            point.y = _transform.position.y;
            
            SetData(new MoveToPositionCommand
            {
                Destination = point
            });
        }
    }
}