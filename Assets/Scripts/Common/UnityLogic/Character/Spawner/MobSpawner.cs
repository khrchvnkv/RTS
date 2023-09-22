using Common.Infrastructure.Factories.CharacterFactory;
using UnityEngine;
using VContainer;

namespace Common.UnityLogic.Character.Spawner
{
    public class MobSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        private ICharacterFactory _characterFactory;

        [Inject]
        private void Construct(ICharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;
            Init();
        }
        private void Init() => _characterFactory.CreateCharacter(_spawnPoint);
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