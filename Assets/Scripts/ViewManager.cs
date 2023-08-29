using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CamState
{
    FP, // First Person View
    TP, // Third Person View
    TV  // Top View
}
public class ViewManager : MonoBehaviour
{
    [Header("Camaras")]
    [Tooltip("Camara en PRIMERA PERSONA")]
    [SerializeField] private GameObject fpCamera;
    [Tooltip("Camara en TERCERA PERSONA")]
    [SerializeField] private GameObject tpCamera;
    [Tooltip("Camara en VISTA SUPERIOR")]
    [SerializeField] private GameObject tvCamera;

    private CamState camNextState;
    private bool noTopCamera;

    private void Start() {
        camNextState = CamState.FP;
        noTopCamera = (tvCamera == null);
    }

    void Update()
    {
        if(Input.GetKeyDown("v")){
            switch(camNextState){
                case CamState.FP:
                    fpCamera.SetActive(true);
                    tpCamera.SetActive(false);
                    if(!noTopCamera){
                        tvCamera.SetActive(false);
                    }
                    camNextState = CamState.TP;
                    break;
                case CamState.TP:
                    fpCamera.SetActive(false);
                    tpCamera.SetActive(true);
                    if(!noTopCamera){
                        tvCamera.SetActive(false);
                    }
                    camNextState = noTopCamera?CamState.FP:CamState.TV;
                    break;
                case CamState.TV:
                    fpCamera.SetActive(false);
                    tpCamera.SetActive(false);
                    tvCamera.SetActive(true);
                    camNextState = CamState.FP;
                    break;
            }
        }
    }
}
