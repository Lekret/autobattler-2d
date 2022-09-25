﻿using System.Threading.Tasks;
using Services.AssetsManagement;
using StaticData;
using UnityEngine;
using Zenject;

namespace Logic.Characters
{
    public class CharacterFactory : ICharacterFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssets _assets;

        public CharacterFactory(IInstantiator instantiator, IAssets assets)
        {
            _instantiator = instantiator;
            _assets = assets;
        }
        
        public async Task<Character> Create(CharacterStaticData data, Team team, Vector3 position)
        {
            var prefab = await _assets.Load<GameObject>(data.PrefabReference);
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