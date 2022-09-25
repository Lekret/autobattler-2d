using System;
using System.Collections;
using Services.AssetsManagement;
using Services.CoroutineRunner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IAssetProvider _assetProvider;

        public SceneLoader(ICoroutineRunner coroutineRunner, IAssetProvider assetProvider)
        {
            _coroutineRunner = coroutineRunner;
            _assetProvider = assetProvider;
        }

        public event Action LoadingStarted;
        public event Action LoadingEnded;
        public event Action<float> ProgressChanged;

        public void LoadScene(string sceneName)
        {
            LoadingStarted?.Invoke();
            var operation = SceneManager.LoadSceneAsync(sceneName);
            _coroutineRunner.StartCoroutine(TrackProgress(operation));
        }

        public void LoadSceneAddressable(string address)
        {
            LoadingStarted?.Invoke();
            _coroutineRunner.StartCoroutine(LoadWithActivation(address));
        }

        private IEnumerator LoadWithActivation(string address)
        {
            var handle = _assetProvider.LoadSceneAsync(address);
            yield return new WaitUntil(() => handle.IsCompleted);
            var operation = handle.Result.ActivateAsync();
            yield return TrackProgress(operation);
        }

        private IEnumerator TrackProgress(AsyncOperation operation)
        {
            while (!operation.isDone)
            {
                ProgressChanged?.Invoke(operation.progress);
                yield return null;
            }
            LoadingEnded?.Invoke();
        }
    }
}