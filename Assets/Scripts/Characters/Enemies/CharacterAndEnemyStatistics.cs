using Characters.Enemies;
using System;
using UnityEngine;

namespace Assets.Scripts.Characters.Enemies
{
    [Serializable]
    public class CharacterAndEnemyStatistics
    {
        public CharacterStatistics CharacterStatistics { get => _characterStatistics; set => _characterStatistics = value; }
        public EnemyStatistics EnemyStatistics { get => _enemyStatistics; set => _enemyStatistics = value; }

        [SerializeField]
        private CharacterStatistics _characterStatistics;

        [SerializeField]
        private EnemyStatistics _enemyStatistics;
    }
}
