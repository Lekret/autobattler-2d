using System.Collections;
using UnityEngine;
using Utils;
using Zenject;

namespace Logic.Actions.Components
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _projectilePrefab;
        [Inject] private IInstantiator _instantiator;

        public IEnumerator LaunchAt(Vector2 targetPosition)
        {
            var projectile = _instantiator.InstantiatePrefab(_projectilePrefab);
            var projectileTransform = projectile.transform;
            SetPositionAndRotation(projectileTransform, targetPosition);
            while ((projectileTransform.position.ToVec2() - targetPosition).sqrMagnitude > Vector2.kEpsilon)
            {
                projectileTransform.position = Vector2.MoveTowards(
                    projectileTransform.position,
                    targetPosition,
                    _projectileSpeed * Time.deltaTime);
                yield return null;
            }
            Destroy(projectile);
        }

        private void SetPositionAndRotation(Transform projectileTransform, Vector2 targetPosition)
        {
            var spawnOffset = _spawnPoint.localPosition;
            var characterPosition = transform.position;
            if (targetPosition.x < characterPosition.x)
            {
                spawnOffset.x *= -1;
            }
            projectileTransform.position = characterPosition + spawnOffset;
            var targetDirection = targetPosition - projectileTransform.position.ToVec2();
            var angle = Vector2.SignedAngle(Vector2.right, targetDirection);
            projectileTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}