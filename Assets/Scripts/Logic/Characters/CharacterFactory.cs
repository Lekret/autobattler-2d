using StaticData;
using UnityEngine;
using Zenject;

namespace Logic.Characters
{
    public class CharacterFactory : ICharacterFactory
    {
        private readonly IInstantiator _instantiator;

        public CharacterFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public Character Create(CharacterStaticData data, Team team, Vector3 position)
        {
            var character = _instantiator.InstantiatePrefabForComponent<Character>(data.Prefab);
            character.Init(team, data.Hp);
            character.transform.position = position;
            ConfigureAnimatorListeners(character);
            return character;
        }

        private static void ConfigureAnimatorListeners(Character character)
        {
            var listener = character.GetComponentsInChildren<IAnimatorListener>();
            var behaviours = character.GetComponentInChildren<Animator>().GetBehaviours<AnimatorTrigger>();
            foreach (var behaviour in behaviours)
            {
                behaviour.SetListeners(listener);
            }
        }
    }
}