using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.State{
    public abstract class InteractionManager
    {
        private const float moveIntensity_Static = 0f;
        private const float moveIntensity_Walk = 0.5f;
        private const float moveIntensity_Run = 1f;
        private const float moveDirection_L = -1f;
        private const float moveDirection_R = 1f;
        private const float moveDirection_F = 1f;
        private const float moveDirection_B = -1f;

        public float speedLR { get; private set; }
        public float speedFR { get; private set; }
        public float rotDir { get; private set; }
        public float speedUD { get; private set; }

        public void Start(){
            speedFR = speedLR = speedUD = 0;
        }

        public bool GetHumanInput(){
            bool _interaction1 = false;
            bool _interaction2 = false;
            bool _interaction3 = false;
            bool _interaction4 = false;
            
            // Se verifica que el personaje se deba mover a la izquierda o a la derecha
            if(Input.GetKey("right")||Input.GetKey("left")) {
                if(Input.GetKey("right")){
                    //mover a la derecha
                    speedLR = moveDirection_R*moveIntensity_Walk;
                    speedFR = moveIntensity_Static;
                }
                else{
                    //mover a la izquierda
                    speedLR = moveDirection_L*moveIntensity_Walk;
                    speedFR = moveIntensity_Static;
                }
                _interaction1 = true;
            }
            else
            {
                _interaction1 = false;
                speedLR = 0;
            }

            if(Input.GetKey("up")||Input.GetKey("down")) {
                if(Input.GetKey("up")){
                    //mover hacia adelante
                    speedLR = moveIntensity_Static;
                    speedFR = moveDirection_F*moveIntensity_Walk;

                }
                else{
                    //mover hacia atrás
                    speedLR = moveIntensity_Static;
                    speedFR = moveDirection_B*moveIntensity_Walk;
                }
                _interaction2 = true;
            }
            else
            {
                _interaction2 = false;
            }

            // Se verifica que el personaje deba saltar
            if(Input.GetKeyDown("space")){
                //saltar
                _interaction3 = true;
                speedUD = moveIntensity_Run;
            }
            else
            {
               _interaction3 = false;
                speedUD = 0; 
            }

            // Se verifica que el personaje deba rotar su vista
            if(Input.GetKey("a")||Input.GetKey("d")){
                _interaction4 = true;
                if(Input.GetKey("a")){
                    //rotar a la izquierda
                    rotDir = -1;
                }
                else{
                    //rotar a la derecha
                    rotDir = 1;
                }
            }
            else{
                _interaction4 = false;
                rotDir = 0;
            }

            if(_interaction1||_interaction2||_interaction3||_interaction4){
                return true;
            }
            else{
                speedLR = moveIntensity_Static;
                speedFR = moveIntensity_Static;
                return false;
            }
        }
    }
}
