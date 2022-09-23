using Logic.Characters;
using Services.CoroutineRunner;
using Services.Randomizer;
using Services.SceneLoader;
using StaticData;
using Zenject;
using Ui;
using UnityEngine;

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
            Container.BindInterfacesTo<CoroutineRunner>().FromNewComponentOnNewGameObject().AsSingle();
            Container.BindInterfacesTo<EntryPoint>().AsSingle().WithArguments(LoadingScreenPrefab);
            Container.BindInstances(CharacterStaticData);
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