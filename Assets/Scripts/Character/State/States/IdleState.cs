using UnityEngine;

namespace Character.State
{

    public class IdleState : InteractionManager, IState
    {
        private CharacterController controller;

        public IdleState(CharacterController controller)
        {
            this.controller = controller;
        }

        public void Enter(){
            base.Start();
            controller.idle();
        }
        public void Update(){
            if(GetHumanInput()){
                if(speedUD == 1 && controller.onTheGround){
                    controller.stateMachine.TransitionTo(controller.stateMachine.jumpState);
                }
                else if((speedLR != 0) ||(speedFR != 0) || (rotDir !=0)){
                    controller.stateMachine.TransitionTo(controller.stateMachine.moveState);
                }
                
            }
        }
        public void Exit(){
            Debug.Log("[IdleState]Exiting");
        }
    }

}