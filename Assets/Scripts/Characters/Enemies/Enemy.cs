using Characters.States.Enums;
using Characters.States;
using UnityEngine;
using Inventories.Items.Weapons;

namespace Characters.Enemies
{
    public class Enemy : Character
    {
        public EnemyStatistics EnemyStatistics => _enemyStatistics;

        [SerializeField]
        private EnemyStatistics _enemyStatistics;

        public override void Initaliaze(CharacterStatistics characterStatistics)
        {
            base.Initaliaze(characterStatistics);
        }

        public void InitializeEnemy(EnemyStatistics enemyStatistics, Weapon weapon)
        {
            _enemyStatistics = enemyStatistics != null ? enemyStatistics : _enemyStatistics;

            CurrentWeapon = weapon;
        }

        public void Awake()
        {
            StateMachine.States = new()
            {
                { CharacterStateType.Attack, new Attack(StateMachine) },
                { CharacterStateType.Idle, new Idle(StateMachine) },
                { CharacterStateType.ChangingWeapon, new ChangingWeapon(StateMachine) }
            };
        }
    }
}
