using Characters.States.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Characters.States
{
    public class Attack : State
    {
        public Attack(CharacterStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override CharacterStateType Type => CharacterStateType.Attack;

        public Animator Animator => Character.Animator;

        public override void Enter()
        {
            Animator.SetBool("IsAttacking", true);

            base.Enter();
        }

        public override void Exit()
        {
            Animator.SetBool("IsAttacking", false);

            base.Exit();
        }
    }
}
