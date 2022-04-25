using System;
using System.Collections;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace DefaultNamespace
{
    public class Launcher
    {
        private float _speedPrjectile;
        private float _speedFiring;

        private bool _isReadyShot;
        private Transform _dir;
        private GameObject[] _projectiles;

        public Launcher(GameObject[] projectiles, float speedFiring, float speedProjectile)
        {
            _projectiles = projectiles;
            _isReadyShot = true;
            _speedFiring = speedFiring;
            _speedPrjectile = speedProjectile;
        }

        public IEnumerator Shot(Transform startPoint)
        {
            if (_isReadyShot)
            {
                _isReadyShot = false;
                var projectile = GetFreeProjectile();
                projectile.GetComponent<Projectile>().SetSpeed = _speedPrjectile;
                projectile.transform.position = startPoint.position + startPoint.up;
                projectile.transform.rotation = startPoint.rotation;
                projectile.SetActive(true);
                yield return new WaitForSeconds(_speedFiring);
                _isReadyShot = true;
            }
        }

        private GameObject GetFreeProjectile()
        {
            foreach (var projectile in _projectiles)
            {
                if (!projectile.activeSelf)
                    return projectile;
            }

            return null;
        }
    }
}