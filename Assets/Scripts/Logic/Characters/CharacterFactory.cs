using System.Threading.Tasks;
using Services.AssetsManagement;
using StaticData;
using UnityEngine;
using Zenject;

namespace Logic.Characters
{
    public class CharacterFactory : ICharacterFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        public CharacterFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }
        
        public async Task<Character> CreateAsync(CharacterStaticData data, Team team, Vector2 position)
        {
            var prefab = await _assetProvider.LoadAsync<GameObject>(data.PrefabReference);
            var character = _instantiator.InstantiatePrefabForComponent<Character>(prefab);
            character.Init(team, data.Hp);
            character.transform.position = position;
            ConfigureAnimatorListeners(character);
            return character;
        }

        private static void ConfigureAnimatorListeners(Character character)
        {
            var enterListeners = character.GetComponentsInChildren<IAnimatorEnterListener>();
            var enterTriggers = character.GetComponentInChildren<Animator>().GetBehaviours<AnimatorEnterTrigger>();
            foreach (var behaviour in enterTriggers)
            {
                behaviour.SetListeners(enterListeners);
            }
            
            var exitListeners = character.GetComponentsInChildren<IAnimatorExitListener>();
            var exitTriggers = character.GetComponentInChildren<Animator>().GetBehaviours<AnimatorExitTrigger>();
            foreach (var behaviour in exitTriggers)
            {
                behaviour.SetListeners(exitListeners);
            }
        }
    }
}