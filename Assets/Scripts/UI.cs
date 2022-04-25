using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UI : MonoBehaviour
    {
        public Text TextScorePlayer;
        public Text TextScoreComputer;

        private int _scorePlayer = 0;
        private int _scoreComputer = 0;
        private Projectile[] _projectiles;

        private void Awake()
        {
            _projectiles = FindObjectsOfType<Projectile>();
        }

        private void OnEnable()
        {
            foreach (var projectile in _projectiles)
            {
                projectile.OnHitInPlayer += PlusPointComputer;
                projectile.OnHitInComputer += PlusPointPlayer;
            }
        }

        private void OnDisable()
        {
            foreach (var projectile in _projectiles)
            {
                projectile.OnHitInPlayer -= PlusPointComputer;
                projectile.OnHitInComputer -= PlusPointPlayer;
            }
        }

        private void PlusPointPlayer()
        {
            _scorePlayer++;
            TextScorePlayer.text = _scorePlayer.ToString();
        }

        private void PlusPointComputer()
        {
            _scoreComputer++;
            TextScoreComputer.text = _scoreComputer.ToString();
        }
    }
}