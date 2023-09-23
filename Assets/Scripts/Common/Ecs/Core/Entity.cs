using System;
using UnityEngine;

namespace Common.Ecs.Core
{
    public class Entity : MonoBehaviour, IDisposable
    {
        public int Id { get; private set; }

        private EcsWorld _ecsWorld;

        protected void Setup(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
            Id = _ecsWorld.CreateEntity();
        }
        public void SetData<T>(T component) where T : struct => _ecsWorld.SetComponent(Id, ref component);
        public ref T GetData<T>() where T : struct => ref _ecsWorld.GetComponent<T>(Id);
        public virtual void Dispose()
        {
            _ecsWorld.DestroyEntity(Id);
            _ecsWorld = null;
            Id = -1;
        }
    }
}