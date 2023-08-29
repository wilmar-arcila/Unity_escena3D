using UnityEngine;

namespace Character.State
{

    public class JumpState : InteractionManager, IState
    {
        private CharacterController controller;

        public JumpState(CharacterController controller)
        {
            this.controller = controller;
        }

        public void Enter(){
            controller.jump();
        }
        public void Update(){
            controller.verifyFalling();
            if(controller.onTheGround){
                if(GetHumanInput()){
                    controller.stateMachine.TransitionTo(controller.stateMachine.moveState);
                }
                else{
                    controller.stateMachine.TransitionTo(controller.stateMachine.idleState);
                }
            }
        }
        public void Exit(){
            Debug.Log("[JumpState]Exiting");
        }
    }

}