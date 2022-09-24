using Services.GameMode;
using Services.SceneLoader;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace Ui
{
    public class MainMenu : MonoBehaviour
    {
        public Button StoryMode;
        public Button SandBox;

        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private IGameModeService _gameModeService;
        
        private void Awake()
        {
            StoryMode.onClick.AddListener(LaunchStoryMode);
            SandBox.onClick.AddListener(LaunchSandbox);
        }

        private void OnDestroy()
        {
            StoryMode.onClick.RemoveListener(LaunchStoryMode);
            SandBox.onClick.RemoveListener(LaunchSandbox);
        }
        
        private void LaunchStoryMode()
        {
            _gameModeService.SetGameMode(GameMode.Story);
            Debug.LogError("Not implemented");
        }
        
        private void LaunchSandbox()
        {
            _gameModeService.SetGameMode(GameMode.Sandbox);
            _sceneLoader.LoadScene("Sandbox");
        }
    }
}