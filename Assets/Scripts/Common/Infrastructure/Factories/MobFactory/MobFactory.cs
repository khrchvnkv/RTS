using Common.Infrastructure.Services.StaticData;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Common.Infrastructure.Factories.MobFactory
{
    public class MobFactory : IMobFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IObjectResolver _objectResolver;

        public MobFactory(IStaticDataService staticDataService, IObjectResolver objectResolver)
        {
            _staticDataService = staticDataService;
            _objectResolver = objectResolver;
        }
        public GameObject CreateMob(Transform point)
        {
            var prefab = _staticDataService.GameStaticData.CharacterStaticData.Prefab;
            var mob = _objectResolver.Instantiate(prefab, point.position, point.rotation, null);
            return mob;
        }
    }
}