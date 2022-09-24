using System.Collections;
using UnityEngine;
using Zenject;

namespace Logic.ActionComponents
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _projectilePrefab;
        [Inject] private IInstantiator _instantiator;

        public IEnumerator LaunchAt(Vector3 targetPosition)
        {
            var projectile = _instantiator.InstantiatePrefab(_projectilePrefab);
            var projectileTransform = projectile.transform;
            SetPositionAndRotation(projectileTransform, targetPosition);
            while ((projectileTransform.position - targetPosition).sqrMagnitude > Vector3.kEpsilon)
            {
                projectileTransform.position = Vector3.MoveTowards(
                    projectileTransform.position,
                    targetPosition,
                    _projectileSpeed * Time.deltaTime);
                yield return null;
            }
            Destroy(projectile);
        }

        private void SetPositionAndRotation(Transform projectileTransform, Vector3 targetPosition)
        {
            projectileTransform.position = _spawnPoint.position;
            var targetDirection = targetPosition - projectileTransform.position;
            var angle = Vector3.SignedAngle(Vector3.right, targetDirection, Vector3.forward);
            projectileTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}