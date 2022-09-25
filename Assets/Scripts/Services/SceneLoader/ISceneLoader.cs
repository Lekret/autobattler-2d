using System;

namespace Services.SceneLoader
{
    public interface ISceneLoader
    {
        event Action LoadingStarted;
        event Action LoadingEnded;
        event Action<float> ProgressChanged;
        void LoadScene(string sceneName);
        void LoadSceneAddressable(string address);
    }
}