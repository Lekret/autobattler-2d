using System;
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
            _sceneLoader.ProgressChanged += SetProgress;
            _sceneLoader.Loaded += Hide;
        }

        private void OnDestroy()
        {
            _sceneLoader.ProgressChanged -= SetProgress;
            _sceneLoader.Loaded -= Hide;
        }

        private void SetProgress(float progress)
        {
            CanvasGroup.alpha = 1;
            Progress.text = $"{progress * 100}%";
        }
        
        private void Hide()
        {
            CanvasGroup.alpha = 0;
        }
    }
}