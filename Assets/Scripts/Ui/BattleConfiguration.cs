using Services.StaticData;
using UnityEngine.UI;
using UnityEngine;

namespace Ui
{
    public class BattleConfiguration : MonoBehaviour
    {
        public Button StartButton;
        public GameObject SelectionRoot;
        
        private void Construct(ICharacterDataService characterDataService)
        {
            foreach (var data in characterDataService.GetAll())
            {
                
            }
        }
        
        private void Awake()
        {
            StartButton.onClick.AddListener(StartBattle);
        }

        private void OnDestroy()
        {
            StartButton.onClick.RemoveListener(StartBattle);
        }

        private void StartBattle()
        {
            Destroy(this);
        }
    }
}