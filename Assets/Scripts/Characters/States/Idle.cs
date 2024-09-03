using Characters.States.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Characters.States
{
    public class Idle : State
    {
        public Idle(CharacterStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override CharacterStateType Type => CharacterStateType.Idle;
    }
}
