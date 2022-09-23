using System;
using System.Collections;
using Services.CoroutineRunner;
using UnityEngine.SceneManagement;

namespace Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public event Action<float> ProgressChanged;
        public event Action Loaded;

        public void LoadScene(string sceneName)
        {
            _coroutineRunner.StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
            {
                ProgressChanged?.Invoke(operation.progress);
                yield return null;
            }
            Loaded?.Invoke();
        }
    }
}