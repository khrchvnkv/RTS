using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.AssetsManagement;
using Common.Infrastructure.Services.Coroutines;
using Common.Infrastructure.Services.DontDestroyOnLoadCreator;
using Common.Infrastructure.Services.Progress;
using Common.Infrastructure.Services.SaveLoad;
using Common.Infrastructure.Services.SceneLoading;
using Common.Infrastructure.Services.StaticData;
using Common.Infrastructure.StateMachine;
using Common.Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace Common.Infrastructure
{
    public sealed class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private GameObject _eventSystemPrefab;

        private IContainerBuilder _containerBuilder;

        private GameObject _gameLifetimeScope;
        private Transform _servicesRoot;
        
        private GameBootstrapper _bootstrapper;
        private DontDestroyOnLoadCreator _dontDestroyOnLoadCreator;
        private CoroutineRunner _coroutineRunner;

        protected override void Configure(IContainerBuilder builder)
        {
            _containerBuilder = builder;
            CreateServicesRoot();
            InstantiateServices();
            BindServices();
            BindGameStateMachine();
            BindFactories();
        }
        private void CreateServicesRoot()
        {
            _gameLifetimeScope = new GameObject("GameLifetimeScope");
            DontDestroyOnLoad(_gameLifetimeScope);
            
            var services = new GameObject("Services");
            services.transform.SetParent(_gameLifetimeScope.transform);
            _servicesRoot = services.transform;
        }
        private void InstantiateServices()
        {
            // GameBootstrapper
            _bootstrapper = new GameObject("GameBootstrapper").AddComponent<GameBootstrapper>();
            _bootstrapper.transform.SetParent(_servicesRoot);
            
            // Dont Destroy on load creator
            _dontDestroyOnLoadCreator = new GameObject("DontDestroyOnLoadCreator").AddComponent<DontDestroyOnLoadCreator>();
            _dontDestroyOnLoadCreator.transform.SetParent(_servicesRoot);

            // Coroutine Runner
            _coroutineRunner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
            _coroutineRunner.transform.SetParent(_servicesRoot);

            // Event System
            var eventSystem = Instantiate(_eventSystemPrefab, _gameLifetimeScope.transform);
            eventSystem.name = nameof(EventSystem);
        }
        private void BindServices()
        {
            _containerBuilder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
            _containerBuilder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
            _containerBuilder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
            _containerBuilder.Register<IPersistentProgressService, PersistentProgressService>(Lifetime.Singleton);
            _containerBuilder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);

            _containerBuilder.RegisterInstance(_bootstrapper);
            _containerBuilder.RegisterInstance<IDontDestroyOnLoadCreator>(_dontDestroyOnLoadCreator);
            _containerBuilder.RegisterInstance<ICoroutineRunner>(_coroutineRunner);
        }
        private void BindGameStateMachine()
        {
            _containerBuilder.Register<GameStateMachine, GameStateMachine>(Lifetime.Singleton);
            _containerBuilder.Register<BootstrapState, BootstrapState>(Lifetime.Singleton);
            _containerBuilder.Register<LoadMainMenuState, LoadMainMenuState>(Lifetime.Singleton);
            _containerBuilder.Register<LoadLevelState, LoadLevelState>(Lifetime.Singleton);
            _containerBuilder.Register<GameLoopState, GameLoopState>(Lifetime.Singleton);
        }
        private void BindFactories()
        {
            _containerBuilder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
        }
    }
}
