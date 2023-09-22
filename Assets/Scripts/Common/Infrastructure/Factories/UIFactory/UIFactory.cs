using System.Collections.Generic;
using Common.Infrastructure.Services.AssetsManagement;
using Common.Infrastructure.Services.DontDestroyOnLoadCreator;
using Common.Infrastructure.Services.StaticData;
using Common.Infrastructure.WindowsManagement;
using Common.UnityLogic.UI.Windows;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Common.Infrastructure.Factories.UIFactory
{
    public sealed class UIFactory : IUIFactory
    {
        private const string UI_PATH = "UI/{0}";

        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IDontDestroyOnLoadCreator _dontDestroyOnLoadCreator;
        private readonly IObjectResolver _objectResolver;

        private readonly Dictionary<string, GameObject> _createdObjects;

        private UIRoot _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, 
            IDontDestroyOnLoadCreator dontDestroyOnLoadCreator, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _dontDestroyOnLoadCreator = dontDestroyOnLoadCreator;
            _objectResolver = objectResolver;
            _createdObjects = new Dictionary<string, GameObject>();
        }
        public void CreateUIRoot()
        {
            if (_uiRoot is not null) Object.Destroy(_uiRoot.gameObject);

            var prefab = _staticDataService.GameStaticData.WindowStaticData.UIRoot;
            _uiRoot = _objectResolver.Instantiate(prefab);
            _dontDestroyOnLoadCreator.MarkAsDontDestroy(_uiRoot.gameObject);
        }
        public void ShowLoadingCurtain()
        {
            if (_uiRoot.LoadingCurtain is null)
            {
                var prefab = _staticDataService.GameStaticData.WindowStaticData.LoadingCurtainPrefab;
                _uiRoot.LoadingCurtain = _objectResolver.Instantiate(prefab, _uiRoot.transform);
            }
            _uiRoot.LoadingCurtain.Show();
        }
        public void HideLoadingCurtain() => _uiRoot.LoadingCurtain.Hide();
        public void ShowWindow<TData>(TData data) where TData : struct, IWindowData
        {
            if (!_createdObjects.TryGetValue(data.WindowName, out var window))
            {
                var path = string.Format(UI_PATH, data.WindowName);
                var windowPrefab = _assetProvider.Load(path);
                window = _objectResolver.Instantiate(windowPrefab, _uiRoot.WindowsParent);
                _createdObjects.Add(data.WindowName, window);
            }
            
            window.GetComponent<IWindow>().Show(data);
        }
        public void Hide<TData>(TData data) where TData : struct, IWindowData
        {
            if (_createdObjects.TryGetValue(data.WindowName, out var window))
            {
                if (data.DestroyOnClosing) _createdObjects.Remove(data.WindowName);
                window.GetComponent<IWindow>().Hide();
            }
        }
    }
}