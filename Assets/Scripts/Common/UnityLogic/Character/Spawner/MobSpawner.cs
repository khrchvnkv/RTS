using Common.Infrastructure.Factories.MobFactory;
using UnityEngine;
using VContainer;

namespace Common.UnityLogic.Character.Spawner
{
    public class MobSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        private IMobFactory _mobFactory;

        [Inject]
        private void Construct(IMobFactory mobFactory)
        {
            _mobFactory = mobFactory;
            Init();
        }
        private void Init() => _mobFactory.CreateMob(_spawnPoint);
        private void OnDrawGizmos()
        {
            if (_spawnPoint is not null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_spawnPoint.position, 0.5f);
            }
        }
    }
}