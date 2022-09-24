using Logic.Characters;
using Services.Randomizer;
using UnityEngine;
using Zenject;

namespace Logic.ActionComponents
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private int _damageMin;
        [SerializeField] private int _damageMax;

        [Inject] private IRandomizer _randomizer;
        
        public void ApplyDamage(Character target)
        {
            var dmg = _randomizer.Range(_damageMin, _damageMax);
            target.Health.ApplyDamage(dmg);
        }
    }
}