using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum MinimapState
{
    OFF,  // Minimap Set Off
    ON1,  // Minimap Set On -> CameraDistance:200, IconSize:20
    ON2,  // Minimap Set On -> CameraDistance:100, IconSize:10
    ON3   // Minimap Set On -> CameraDistance: 50, IconSize:5
}
public class MinimapManager : MonoBehaviour
{
    [Header("Minimap UI")]
    [Tooltip("Objeto padre de los elementos UI del Minimap")]
    [SerializeField] private GameObject minimap;

    [Header("Minimap Cameras")]
    [Tooltip("Camara virtual (Cinemachine) con la vista de todo el terreno (Estática)")]
    [SerializeField] private CinemachineVirtualCamera minimapCamS;

    [Tooltip("Camara virtual (Cinemachine) con seguimiento de personaje")]
    [SerializeField] private CinemachineVirtualCamera minimapCam;

    private MinimapState minimapNextState;

    //////////////////////////////////////////////
    /*         OBSERVER PATTERN (as Publisher)  */
    public event Action<int> MinimapChanged;
    //////////////////////////////////////////////

    void Start()
    {
        minimap.SetActive(false);
        minimapNextState = MinimapState.ON1;
        minimapCam.gameObject.SetActive(false);
        minimapCamS.gameObject.SetActive(false);
        MinimapChanged?.Invoke(20);
    }

    void Update()
    {
        if(Input.GetKeyDown("m")){
            var follower = minimapCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            switch(minimapNextState){
                case MinimapState.OFF:
                    minimap.SetActive(false);
                    minimapCam.gameObject.SetActive(false);
                    minimapCamS.gameObject.SetActive(false);
                    minimapNextState = MinimapState.ON1;
                    break;
                case MinimapState.ON1:
                    minimap.SetActive(true);
                    minimapCam.gameObject.SetActive(false);
                    minimapCamS.gameObject.SetActive(true);
                    MinimapChanged?.Invoke(20);
                    minimapNextState = MinimapState.ON2;
                    break;
                case MinimapState.ON2:
                    minimapCam.gameObject.SetActive(true);
                    minimapCamS.gameObject.SetActive(false);
                    follower.ShoulderOffset = new Vector3(0,100,0);
                    MinimapChanged?.Invoke(10);
                    minimapNextState = MinimapState.ON3;
                    break;
                case MinimapState.ON3:
                    follower.ShoulderOffset = new Vector3(0,50,0);
                    MinimapChanged?.Invoke(5);
                    minimapNextState = MinimapState.OFF;
                    break;
            }
        }
    }
}
