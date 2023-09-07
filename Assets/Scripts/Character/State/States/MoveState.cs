using UnityEngine;

namespace Character.State
{

    public class MoveState : InteractionManager, IState
    {
        private CharacterController controller;

        public MoveState(CharacterController controller)
        {
            this.controller = controller;
        }

        public void Enter(){
        }
        public void Update(){
            if(GetHumanInput()){
                controller.move(speedLR, speedFR, rotDir);
                if(speedUD == 1 && controller.onTheGround){
                    controller.stateMachine.TransitionTo(controller.stateMachine.jumpState);
                }
            }
            else{
                controller.stateMachine.TransitionTo(controller.stateMachine.idleState);
            }
        }
        public void Exit(){
            Debug.Log("[MoveState]Exiting");
        }
    }

}