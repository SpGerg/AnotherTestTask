using Characters.States.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.States
{
    public class CharacterStateMachine : MonoBehaviour
    {
        public Dictionary<CharacterStateType, State> States { get; set; }

        public Character Character => _character;

        public CharacterStateType Current
        {
            get
            {
                return _current;
            }
            set 
            {
                var lastState = _currentState;

                _currentState?.Exit();

                if (!States.TryGetValue(value, out _currentState))
                {
                    return;
                }

                _current = value;

                _currentState.Enter();

                LastState = lastState;
            }
        }

        public State CurrentState => _currentState;

        public State LastState { get; private set; }

        [SerializeField]
        private Character _character;

        private State _currentState;

        [SerializeField]
        private CharacterStateType _current;

        public void Update()
        {
            _currentState?.Update();
        }
    }
}
