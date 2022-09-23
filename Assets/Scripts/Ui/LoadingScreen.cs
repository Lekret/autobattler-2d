using System;
using Services.SceneLoader;
using TMPro;
using UnityEngine;
using Zenject;

namespace Ui
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _progress;
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
            _canvasGroup.alpha = 1;
            _progress.text = $"{progress}%";
        }
        
        private void Hide()
        {
            _canvasGroup.alpha = 0;
        }
    }
}