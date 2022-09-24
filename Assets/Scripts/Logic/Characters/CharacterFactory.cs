using System.Collections.Generic;
using System.Linq;
using Services.StaticData;
using StaticData;
using UnityEngine;
using Zenject;

namespace Logic.Characters
{
    public class CharacterFactory : ICharacterFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInstantiator _instantiator;

        public CharacterFactory(IStaticDataService staticDataService, IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _staticDataService = staticDataService;
        }
        
        public Character Create(string id, Team team, Vector3 position)
        {
            var data = _staticDataService.GetById(id);
            var character = _instantiator.InstantiatePrefabForComponent<Character>(data.Prefab);
            character.Init(team, data.Hp);
            character.transform.position = position;
            SetAnimatorListener(character);
            return character;
        }

        private static void SetAnimatorListener(Character character)
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