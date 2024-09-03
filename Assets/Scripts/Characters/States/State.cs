using Characters.States.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Characters.States
{
    public abstract class State
    {
        public State(CharacterStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract CharacterStateType Type { get; }

        public CharacterStateMachine StateMachine { get; }

        public State LastState => StateMachine.LastState;

        public Character Character => StateMachine.Character;

        public virtual void Enter() { }

        public virtual void Update() { }

        public virtual void Exit() { }
    }
}
