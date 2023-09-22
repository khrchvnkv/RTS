using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Ecs.Commands;
using Common.Ecs.Components.Animation;
using Common.Ecs.Components.Movement;
using Common.Ecs.Core;
using Common.Ecs.Systems;
using Common.Infrastructure.Services.UpdateBehaviour;

namespace Common.Ecs
{
    public sealed class EcsWorld : IDisposable
    {
        private readonly List<ISystem> _systems = new();
        private readonly List<IUpdateSystem> _updateSystems = new();
        private readonly List<IFixedUpdateSystem> _fixedUpdateSystems = new();
        private readonly List<ILateUpdateSystem> _lateUpdateSystems = new();

        private readonly Dictionary<Type, IComponentPool> _componentPools = new();
        private readonly List<bool> _entities = new();

        private readonly IUpdateBehaviour _updateBehaviour;

        public EcsWorld(IUpdateBehaviour updateBehaviour)
        {
            _updateBehaviour = updateBehaviour;
        }
        public void Init()
        {
            _updateBehaviour.OnUpdate += Update;
            _updateBehaviour.OnFixedUpdate += FixedUpdate;
            _updateBehaviour.OnLateUpdate += LateUpdate;
            
            BindComponents();
            BindSystems();
            
            Install();
        }
        private void BindComponents()
        {
            BindComponent<MoveStateComponent>();
            BindComponent<MoveSpeedComponent>();
            BindComponent<MoveRotationComponent>();
            BindComponent<TransformComponent>();
            BindComponent<AnimatorComponent>();
            BindComponent<MoveToPositionCommand>();
        }
        private void BindSystems()
        {
            BindSystem<MovementSystem>();
            BindSystem<MoveToPositionSystem>();
            BindSystem<MoveAnimationSystem>();
        }
        public int CreateEntity()
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                if (_entities[i] == false)
                {
                    _entities[i] = true;
                    return i;
                }
            }

            var id = _entities.Count;
            _entities.Add(true);

            foreach (var pool in _componentPools.Values)
            {
                pool.AllocateComponentsArray();
            }

            return id;
        }
        public void DestroyEntity(in int entity)
        {
            _entities[entity] = false;
            foreach (var pool in _componentPools.Values)
            {
                pool.RemoveComponent(entity);
            }
        }
        public ref T GetComponent<T>(int entity) where T : struct
        {
            var pool = (ComponentPool<T>)_componentPools[typeof(T)];
            return ref pool.GetComponent(entity);
        }
        public void SetComponent<T>(in int entity, ref T component) where T : struct
        {
            var pool = (ComponentPool<T>)_componentPools[typeof(T)];
            pool.SetComponent(entity, ref component);
        }
        public void Dispose()
        {
            _updateBehaviour.OnUpdate -= Update;
            _updateBehaviour.OnFixedUpdate += FixedUpdate;
            _updateBehaviour.OnLateUpdate += LateUpdate;
            
            _systems.Clear();
            _updateSystems.Clear();
            _fixedUpdateSystems.Clear();
            _componentPools.Clear();
            _entities.Clear();
        } 
        private void BindComponent<T>() where T : struct
        {
            _componentPools[typeof(T)] = new ComponentPool<T>();
        }
        private T BindSystem<T>() where T : ISystem, new()
        {
            var system = new T();
            _systems.Add(system);

            if (system is IUpdateSystem updateSystem)
            {
                _updateSystems.Add(updateSystem);
            }
            
            if (system is IFixedUpdateSystem fixedUpdateSystem)
            {
                _fixedUpdateSystems.Add(fixedUpdateSystem);
            }
            
            if (system is ILateUpdateSystem lateUpdateSystem)
            {
                _lateUpdateSystems.Add(lateUpdateSystem);
            }

            return system;
        }
        private void Install()
        {
            foreach (var system in _systems)
            {
                InstallSystem(system);
            }
        }
        private void InstallSystem(ISystem system)
        {
            Type systemType = system.GetType();
            var fields = systemType.GetFields(
                BindingFlags.Instance | BindingFlags.DeclaredOnly |
                BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var field in fields)
            {
                if (field.FieldType.GetInterfaces().Contains(typeof(IComponentPool)))
                {
                    var componentType = field.FieldType.GenericTypeArguments[0];
                    var componentPool = _componentPools[componentType];
                    field.SetValue(system, componentPool);
                }
            }
        }
        private void Update()
        {
            foreach (var system in _updateSystems)
            {
                for (var entity = 0; entity < _entities.Count; entity++)
                {
                    if (_entities[entity]) system.OnUpdate(entity);
                }
            }
        }
        private void FixedUpdate()
        {
            foreach (var system in _fixedUpdateSystems)
            {
                for (var entity = 0; entity < _entities.Count; entity++)
                {
                    if (_entities[entity]) system.OnFixedUpdate(entity);
                }
            }
        }
        private void LateUpdate()
        {
            foreach (var system in _lateUpdateSystems)
            {
                for (var entity = 0; entity < _entities.Count; entity++)
                {
                    if (_entities[entity]) system.OnLateUpdate(entity);
                }
            }
        }
    }
}