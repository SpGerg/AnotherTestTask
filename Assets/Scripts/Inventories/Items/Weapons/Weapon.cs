using Characters.Interfaces;
using Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Characters.States.Enums;
using UnityEngine.Events;
using System.Threading;

namespace Inventories.Items.Weapons
{
    public class Weapon : Item
    {
        public CharacterStatistics Statistics => Character.Statistics;

        public UnityEvent Reloaded { get; private set; } = new();

        public bool IsCanAttack => !Animator.GetBool("IsReloading") || Character.StateMachine.Current is CharacterStateType.Attack || Character.IsReloadPause;

        public IDamageable Target { get; set; }

        [SerializeField]
        private string _equipAnimationName;

        public const int CriticalDamageChance = 25;

        public const int CriticalDamagePercent = 20;

        public override void Equip()
        {
            base.Equip();

            EquipWeapon();

            Character.AttackAnimation.AddListener(AttackOnAttackAnimation);

            if (EquipAnimationName == string.Empty)
            {
                return;
            }

            Animator.SetBool(EquipAnimationName, true);
        }

        public override void Unequip()
        {
            base.Unequip();

            UnequipWeapon();

            Target?.Died.RemoveListener(OnTargetDied);
            Character.AttackAnimation.RemoveListener(AttackOnAttackAnimation);

            if (EquipAnimationName == string.Empty)
            {
                return;
            }

            Animator.SetBool(EquipAnimationName, false);
        }

        public virtual void EquipWeapon() { }

        public virtual void UnequipWeapon() { }

        public virtual void Attack(IDamageable target)
        {
            if (Character.CurrentWeapon != this)
            {
                return;
            }

            if (!IsCanAttack)
            {
                return;
            }

            Animator.SetBool("IsAttacking", true);

            var damage = Statistics.AttackDamage;
            var chance = UnityEngine.Random.Range(1, 100);

            if (chance < CriticalDamageChance)
            {
                damage += (int)((Statistics.AttackDamage / 100m) * CriticalDamagePercent);
            }

            target.TakeDamage(damage);

            StartCoroutine(AttackReloadCoroutine());
        }

        public void AttackWhileTargetAlive(IDamageable target)
        {
            Character.StateMachine.Current = CharacterStateType.Attack;

            Target?.Died.RemoveListener(OnTargetDied);

            Target = target;

            Target.Died.AddListener(OnTargetDied);
        }

        protected IEnumerator AttackReloadCoroutine()
        {
            Animator.SetBool("IsAttacking", false);
            Animator.SetBool("IsReloading", true);

            var delay = Statistics.AttackDelay / 0.1f;
            Character.ReloadTime = delay;

            for (var i = 0;i < delay; i++)
            {
                if (Character.IsReloadPause)
                {
                    yield return new WaitForEndOfFrame();

                    i--;

                    continue;
                }

                Character.ReloadProgress += 1;

                yield return new WaitForSeconds(0.1f);
            }

            Character.ReloadProgress = 0;
            Character.ReloadTime = 0;

            Animator.SetBool("IsAttacking", true);
            Animator.SetBool("IsReloading", false);

            Reloaded.Invoke();
        }

        private void AttackOnAttackAnimation()
        {
            if (Target == null)
            {
                Animator.SetBool("IsAttacking", false);

                return;
            }

            Attack(Target);
        }

        private void OnTargetDied()
        {
            Character.StateMachine.Current = CharacterStateType.Idle;
        }
    }
}
