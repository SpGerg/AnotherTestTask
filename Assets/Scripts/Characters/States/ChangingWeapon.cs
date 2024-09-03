using Characters.States.Enums;
using Inventories.Items.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Characters.States
{
    public class ChangingWeapon : State
    {
        public ChangingWeapon(CharacterStateMachine stateMachine) : base(stateMachine) { }

        public override CharacterStateType Type => CharacterStateType.ChangingWeapon;

        public Animator Animator => Character.Animator;

        public bool IsFinished { get; private set; }

        private Weapon _newWeapon;

        public override void Enter()
        {
            Character.AttackAnimation.AddListener(ChangeWeaponOnAttackAnimationEnded);

            IsFinished = false;

            base.Enter();
        }

        public override void Exit()
        {
            Character.AttackAnimationEnded.RemoveListener(ChangeWeaponOnAttackAnimationEnded);

            Animator.SetBool(_newWeapon.EquipAnimationName, true);

            base.Exit();
        }

        public void Enter(Weapon weapon)
        {
            _newWeapon = weapon;

            if (Character.CurrentWeapon == weapon & LastState is not null)
            {
                StateMachine.Current = LastState.Type;

                return;
            }

            if (Character.CurrentWeapon != null)
            {
                Animator.SetBool(Character.CurrentWeapon.EquipAnimationName, false);
            }

            if (!Animator.GetBool("IsAttacking"))
            {
                StateMachine.StartCoroutine(ChangeWeaponCoroutine(_newWeapon));
            }
        }

        private void ChangeWeaponOnAttackAnimationEnded()
        {
            if (StateMachine.Current is not CharacterStateType.ChangingWeapon)
            {
                return;
            }

            ChangeWeaponCoroutine(_newWeapon);
        }

        private IEnumerator ChangeWeaponCoroutine(Weapon weapon)
        {
            Character.IsReloadPause = true;

            yield return new WaitForSeconds(2);

            Character.IsReloadPause = false;

            IsFinished = true;

            Character.CurrentWeapon = weapon;
        }
    }
}
