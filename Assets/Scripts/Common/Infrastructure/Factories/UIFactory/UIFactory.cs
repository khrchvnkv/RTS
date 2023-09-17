using Common.Infrastructure.Services.DontDestroyOnLoadCreator;
using Common.Infrastructure.Services.StaticData;
using Common.UnityLogic.LoadingCurtain;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Common.Infrastructure.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IDontDestroyOnLoadCreator _dontDestroyOnLoadCreator;
        private readonly IObjectResolver _objectResolver;

        private Transform _uiRoot;
        private LoadingCurtain _loadingCurtain;

        public UIFactory(IStaticDataService staticDataService, IDontDestroyOnLoadCreator dontDestroyOnLoadCreator,
            IObjectResolver objectResolver)
        {
            _staticDataService = staticDataService;
            _dontDestroyOnLoadCreator = dontDestroyOnLoadCreator;
            _objectResolver = objectResolver;
        }
        public void CreateUIRoot()
        {
            if (_uiRoot is not null) Object.Destroy(_uiRoot.gameObject);

            var prefab = _staticDataService.GameStaticData.WindowStaticData.UIRoot;
            var instance = _objectResolver.Instantiate(prefab);
            _uiRoot = instance.transform;
            _dontDestroyOnLoadCreator.MarkAsDontDestroy(instance);
        }
        public void ShowLoadingCurtain()
        {
            if (_loadingCurtain is null)
            {
                var prefab = _staticDataService.GameStaticData.WindowStaticData.LoadingCurtainPrefab;
                _loadingCurtain = _objectResolver.Instantiate(prefab, _uiRoot);
            }
            _loadingCurtain.Show();
        }
        public void HideLoadingCurtain() => _loadingCurtain.Hide();
    }
}