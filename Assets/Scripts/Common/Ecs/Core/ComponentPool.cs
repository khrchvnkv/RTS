using System;

namespace Common.Ecs.Core
{
    public class ComponentPool<T> : IComponentPool
    {
        private struct Component
        {
            public bool Exists;
            public T Value;
        }

        private Component[] _components = new Component[64];
        private int _size;

        public void AllocateComponentsArray()
        {
            if (_size + 1 >= _components.Length)
            {
                Array.Resize(ref _components, _components.Length * 2);
            }

            _components[_size] = new Component()
            {
                Exists = false,
                Value = default
            };

            _size++;
        }
        public ref T GetComponent(int entity)
        {
            ref var component = ref _components[entity];
            return ref component.Value;
        }
        public void SetComponent(in int entity, ref T data)
        {
            ref var component = ref _components[entity];
            component.Exists = true;
            component.Value = data;
        }
        public bool HasComponent(in int entity) => _components[entity].Exists;
        public void RemoveComponent(in int entity)
        {
            ref var component = ref _components[entity];
            component.Exists = false;
        }
    }
}