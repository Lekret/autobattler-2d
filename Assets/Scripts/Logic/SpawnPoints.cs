using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logic
{
    public class SpawnPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] _leftSpawnPoints;
        [SerializeField] private Transform[] _rightSpawnPoints;

        public IEnumerable<Transform> LeftSpawnPoints => _leftSpawnPoints;
        public IEnumerable<Transform> RightSpawnPoints => _rightSpawnPoints;
    }
}