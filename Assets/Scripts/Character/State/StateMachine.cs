using System;
using UnityEngine;

namespace Character.State
{
    
    // handles
    [Serializable]
    public class StateMachine
    {
        public IState CurrentState { get; private set; }

        // ESTADOS
        public IdleState idleState;
        public MoveState moveState;
        public JumpState jumpState;

        // Evento que notifica del cambio de estado
        public event Action<IState> stateChanged;

        // Es necesario pasar la referencia del controlador al cual ésta máquina da servicio
        public StateMachine(CharacterController controller)
        {
            // Se crea una instancia de cada estado que la máquina va a controlar
            idleState = new IdleState(controller);
            moveState = new MoveState(controller);
            jumpState = new JumpState(controller);
        }

        // Inicialización de la máquina de estados
        public void Initialize(IState state)
        {
            CurrentState = state;
            state.Enter();
            stateChanged?.Invoke(state);
        }

        // Método que permite indicar a la máquina de estados que debe cambiar de estado
        public void TransitionTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
            stateChanged?.Invoke(nextState);
        }

        // Se llama en cada frame desde el controlador
        public void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.Update();
            }
        }
    }

}