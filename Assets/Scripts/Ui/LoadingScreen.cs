using Services.SceneLoader;
using TMPro;
using UnityEngine;
using Zenject;

namespace Ui
{
    public class LoadingScreen : MonoBehaviour
    {
        public CanvasGroup CanvasGroup;
        public TextMeshProUGUI Progress;
        [Inject] private ISceneLoader _sceneLoader;

        private void Awake()
        {
            _sceneLoader.LoadingStarted += Show;
            _sceneLoader.LoadingEnded += Hide;
            _sceneLoader.ProgressChanged += SetProgress;
        }
        
        private void OnDestroy()
        {
            _sceneLoader.LoadingStarted += Show;
            _sceneLoader.LoadingEnded -= Hide;
            _sceneLoader.ProgressChanged -= SetProgress;
        }

        private void Show()
        {
            CanvasGroup.alpha = 1;
        }
        
        private void Hide()
        {
            CanvasGroup.alpha = 0;
        }

        private void SetProgress(float progress)
        {
            Progress.text = $"{progress * 100}%";
        }
    }
}