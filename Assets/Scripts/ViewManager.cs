using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ViewManager : MonoBehaviour
{
    [Header("Camaras")]
    [Tooltip("Camara PRINCIPAL")]
    [SerializeField] private Camera mainCamera;

    [Tooltip("Camaras virtuales entre las que se rota la vista del juego")]
    [SerializeField] private List<CinemachineVirtualCameraBase> vCams;

    [Space(8)]
    [Header("Capas excluídas")]
    [Tooltip("Capas que se desean excluir de las vistas")]
    [SerializeField] private List<ExcludedLayers> excludedLayers;

    private KeyCode viewChangeKey;
    private int actualViewIndex;
    private LayerMask baseMask;

    private void Start() {
        SetViewChangeKey('v');
        actualViewIndex = 0;
        baseMask = mainCamera.cullingMask;
        Debug.Log("CharacterMask: " + excludedLayers[0]);
    }

    void Update()
    {
        if(Input.GetKeyDown(viewChangeKey)){
            vCams[actualViewIndex].Priority = 1;
            actualViewIndex = (actualViewIndex+1 < vCams.Count)? actualViewIndex+1 : 0;
            vCams[actualViewIndex].Priority = 10;
            foreach (var item in excludedLayers)
            {
                if(actualViewIndex == item.vCamIndex){
                    mainCamera.cullingMask = baseMask - item.layers;
                }
                else{
                    mainCamera.cullingMask = baseMask;
                }
            }
        }
    }

    public void SetViewChangeKey(char key){
        viewChangeKey = (KeyCode) System.Enum.Parse(typeof(KeyCode), char.ToUpper(key).ToString());
    }
}

[System.Serializable]
public struct ExcludedLayers
{
    public LayerMask layers;
    public int vCamIndex;
}
