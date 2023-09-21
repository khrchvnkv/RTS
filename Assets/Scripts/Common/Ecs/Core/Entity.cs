using System;
using UnityEngine;
using VContainer;

namespace Common.Ecs.Core
{
    public class Entity : MonoBehaviour, IDisposable
    {
        public int Id { get; private set; }

        [Inject] private EcsWorld _ecsWorld;
        
        public void SetData<T>(T component) where T : struct => _ecsWorld.SetComponent(Id, ref component);
        public ref T GetData<T>() where T : struct => ref _ecsWorld.GetComponent<T>(Id);
        public void Dispose()
        {
            _ecsWorld.DestroyEntity(Id);
            _ecsWorld = null;
            Id = -1;
        }
    }
}