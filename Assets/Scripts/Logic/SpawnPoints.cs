using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class SpawnPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] _leftSpawnPoints;
        [SerializeField] private Transform[] _rightSpawnPoints;

        public IReadOnlyList<Transform> LeftSpawnPoints => _leftSpawnPoints;
        public IReadOnlyList<Transform> RightSpawnPoints => _rightSpawnPoints;
    }
}