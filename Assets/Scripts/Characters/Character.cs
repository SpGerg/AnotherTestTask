using Characters.Interfaces;
using Characters.States;
using Characters.States.Enums;
using Inventories;
using Inventories.Items.Weapons;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Characters
{
    [RequireComponent(typeof(BasicHealth), typeof(Animator), typeof(CharacterStateMachine))]
    [RequireComponent(typeof(Inventory))]
    public abstract class Character : MonoBehaviour
    {
        public CharacterStatistics BaseStatistics => _baseStatistics;

        public CharacterStatistics Statistics => _statistics;

        public Weapon CurrentWeapon {  get => _inventory.CurrentWeapon; set => _inventory.CurrentWeapon = value; }

        public Inventory Inventory => _inventory;

        public CharacterStateMachine StateMachine => _stateMachine;

        public BasicHealth Health => _health;

        public Animator Animator => _animator;

        public UnityEvent AttackAnimation { get; private set; } = new();

        public UnityEvent AttackAnimationEnded { get; private set; } = new();

        public float ReloadTime { get; set; }

        public float ReloadProgress { get; set; }

        public bool IsReloadPause { get; set; }

        [SerializeField]
        private CharacterStateMachine _stateMachine;

        [SerializeField]
        private Inventory _inventory;

        [SerializeField]
        private BasicHealth _health;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private CharacterStatistics _baseStatistics;

        [SerializeField]
        private CharacterStatistics _statistics;

        public virtual void Initaliaze(CharacterStatistics characterStatistics)
        {
            _animator.runtimeAnimatorController = characterStatistics.RuntimeAnimatorController;
            _animator.SetFloat("AttackSpeed", 1 + characterStatistics.AttackSpeed);

            _baseStatistics = characterStatistics;
            _statistics = _statistics != null ? _statistics : characterStatistics.Copy();

            _statistics.AttackSpeedChanged.AddListener(UpdateSpeedOnAttackSpeedChanged);

            StateMachine.States = new()
            {
                { CharacterStateType.Attack, new Attack(StateMachine) },
                { CharacterStateType.Idle, new Idle(StateMachine) },
                { CharacterStateType.ChangingWeapon, new ChangingWeapon(StateMachine) }
            };

            _health.Initialize();

            if (CurrentWeapon != null)
            {
                CurrentWeapon.Initialize(_inventory, CurrentWeapon.ItemData);
            }

            StateMachine.Current = CharacterStateType.Idle;
        }

        public virtual void OnDisable()
        {
            _statistics.AttackSpeedChanged.RemoveListener(UpdateSpeedOnAttackSpeedChanged);
        }

        public void AddStatistics(Statistic[] statistics)
        {
            foreach (var statistic in statistics)
            {
                AddStatistic(statistic);
            }
        }

        public void RemoveStatistics(Statistic[] statistics)
        {
            foreach (var statistic in statistics)
            {
                RemoveStatistic(statistic);
            }
        }

        public void AddStatistic(Statistic statistic)
        {
            switch (statistic.Type)
            {
                case StatisticType.MaxHealth:
                    Health.MaxHealth += (int)statistic.Value;
                    Health.HealthValue += (int)statistic.Value;
                    break;
                case StatisticType.Armor:
                    Statistics.Armor += (int)statistic.Value;
                    break;
                case StatisticType.AttackDamage:
                    Statistics.AttackDamage += (int)statistic.Value;
                    break;
                case StatisticType.AttackDelay:
                    Statistics.AttackDelay += statistic.Value;
                    break;
                case StatisticType.AttackSpeed:
                    Statistics.AttackSpeed += statistic.Value;
                    break;
            }
        }

        public void RemoveStatistic(Statistic statistic) => AddStatistic(new Statistic()
        {
            Type = statistic.Type,
            Value = -statistic.Value
        });

        public void Attack(IDamageable target)
        {
            if (CurrentWeapon == null)
            {
                return;
            }

            CurrentWeapon.Attack(target);
        }

        public void AttackWhileTargetAlive(IDamageable target)
        {
            if (CurrentWeapon == null)
            {
                return;
            }

            CurrentWeapon.AttackWhileTargetAlive(target);
        }

        public void CallEventOnAttackAnimation()
        {
            AttackAnimation.Invoke();
        }

        public void CallEventOnAttackAnimationEnded()
        {
            AttackAnimationEnded.Invoke();
        }

        public void UpdateSpeedOnAttackSpeedChanged(float oldSpeed)
        {
            _animator.SetFloat("AttackSpeed", 1 + _statistics.AttackSpeed);
        }
    } 
}
