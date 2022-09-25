using Services.AssetsManagement;
using Services.CoroutineRunner;
using Services.GameMode;
using Services.Randomizer;
using Services.SceneLoader;
using Services.StaticData;
using StaticData;
using Ui;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    [CreateAssetMenu(menuName = "Installers/BoostrapInstaller", fileName = "BootstrapInstaller")]
    public class BoostrapInstaller : ScriptableObjectInstaller
    {
        public CharacterStaticData[] CharacterStaticData;
        public LoadingScreen LoadingScreenPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<Randomizer>().AsSingle();
            Container.BindInterfacesTo<GameModeService>().AsSingle();
            Container.BindInterfacesTo<CoroutineRunner>().FromNewComponentOnNewGameObject().AsSingle();
            Container.BindInterfacesTo<AssetProvider>().AsSingle();
            Container.BindInterfacesTo<EntryPoint>().AsSingle().WithArguments(LoadingScreenPrefab);
            Container.BindInterfacesTo<CharacterDataService>().AsSingle().WithArguments(CharacterStaticData);
        }

        private class EntryPoint : IInitializable
        {
            private readonly ISceneLoader _sceneLoader;
            private readonly IInstantiator _instantiator;
            private readonly LoadingScreen _loadingScreenPrefab;

            public EntryPoint(
                ISceneLoader sceneLoader, 
                IInstantiator instantiator,
                LoadingScreen loadingScreenPrefab)
            {
                _sceneLoader = sceneLoader;
                _instantiator = instantiator;
                _loadingScreenPrefab = loadingScreenPrefab;
            }

            public void Initialize()
            {
                var loadingScreen = _instantiator.InstantiatePrefab(_loadingScreenPrefab);
                DontDestroyOnLoad(loadingScreen);
                _sceneLoader.LoadScene("MainMenu");
            }
        }
    }
}