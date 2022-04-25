using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SpawnManager : MonoBehaviour
    {
        private GameObject _player;
        private GameObject _computer;
        private Vector3 _startPlayerPosition;
        private Vector3 _startComputerPosition;
        private Quaternion _startPlayerQuaternion;
        private Quaternion _startComputerQuaternion;
        
        private GameObject[] _projectiles;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _computer = GameObject.FindGameObjectWithTag("Computer");
            _startPlayerPosition = _player.transform.position;
            _startComputerPosition = _computer.transform.position;
            _startPlayerQuaternion = _player.transform.rotation;
            _startComputerQuaternion = _computer.transform.rotation;
            _projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        }

        private void OnEnable()
        {
            foreach (var projectile in _projectiles)
            {
                projectile.GetComponent<Projectile>().OnHitInPlayer += Restart;
                projectile.GetComponent<Projectile>().OnHitInComputer += Restart;
            }
        }

        private void OnDisable()
        {
            foreach (var projectile in _projectiles)
            {
                projectile.GetComponent<Projectile>().OnHitInPlayer -= Restart;
                projectile.GetComponent<Projectile>().OnHitInComputer -= Restart;
            }
        }

        private void Restart()
        {
            foreach (var projectile in _projectiles)
            {
                projectile.SetActive(false);
            }
            _player.transform.position = _startPlayerPosition;
            _player.transform.rotation = _startPlayerQuaternion;
            _computer.transform.position = _startComputerPosition;
            _computer.transform.rotation = _startComputerQuaternion;
        }
    }
}