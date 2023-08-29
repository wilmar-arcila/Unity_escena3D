using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.State{
    public abstract class InteractionManager
    {
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
                    speedLR = 1;
                }
                else{
                    //mover a la izquierda
                    speedLR = -1;
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
                    speedFR = 1;

                }
                else{
                    //mover hacia atrás
                    speedFR = -1;
                }
                _interaction2 = true;
            }
            else
            {
                _interaction2 = false;
                speedFR = 0;
            }

            // Se verifica que el personaje deba saltar
            if(Input.GetKeyDown("space")){
                //saltar
                _interaction3 = true;
                speedUD = 1;
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
                    //rotar la cápsula a la izquierda
                    rotDir = -1;
                }
                else{
                    //rotar la cápsula a la derecha
                    rotDir = 1;
                }
            }
            else{
                _interaction4 = false;
                rotDir = 0;
            }

            return _interaction1||_interaction2||_interaction3||_interaction4;
        }
    }
}
