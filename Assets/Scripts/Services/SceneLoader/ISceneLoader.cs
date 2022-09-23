using System;

namespace Services.SceneLoader
{
    public interface ISceneLoader
    {
        event Action<float> ProgressChanged;
        event Action Loaded;
        void LoadScene(string sceneName);
    }
}