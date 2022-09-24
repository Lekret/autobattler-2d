using System.Collections.Generic;
using System.Linq;
using StaticData;
using UnityEngine;
using Zenject;

namespace Logic.Characters
{
    public class CharacterFactory : ICharacterFactory
    {
        private readonly Dictionary<string, CharacterStaticData> _characterStaticData;
        private readonly IInstantiator _instantiator;

        public CharacterFactory(List<CharacterStaticData> data, IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _characterStaticData = data.ToDictionary(x => x.Id, x => x);
        }
        
        public Character Create(string id, Team team, Vector3 position)
        {
            var data = _characterStaticData[id];
            var character = _instantiator.InstantiatePrefabForComponent<Character>(data.Prefab);
            character.Init(team, data.Hp);
            character.transform.position = position;
            SetAnimatorListener(character);
            return character;
        }

        private static void SetAnimatorListener(Character character)
        {
            var listener = character.GetComponentInChildren<IAnimatorListener>();
            var behaviours = character.GetComponentInChildren<Animator>().GetBehaviours<AnimatorTriggerBehaviour>();
            foreach (var behaviour in behaviours)
            {
                behaviour.SetListener(listener);
            }
        }
    }
}