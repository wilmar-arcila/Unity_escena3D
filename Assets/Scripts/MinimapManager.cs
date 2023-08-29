using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinimapState
{
    OFF,  // Minimap Set Off
    ON,   // Minimap Set On -> 1X zoom
    ON5,  // Minimap Set On -> 5X zoom
    ON10  // Minimap Set On -> 10X zoom
}
public class MinimapManager : MonoBehaviour
{
    [Header("Minimap UI")]
    [Tooltip("Objeto Padre de los elementos UI del Minimap")]
    [SerializeField] private GameObject minimap;

    [Header("Minimap Camera")]
    [Tooltip("Camara con la vista del Minimap")]
    [SerializeField] private Camera minimapCam;

    private MinimapState minimapNextState;

    void Start()
    {
        minimap.SetActive(false);
        minimapNextState = MinimapState.ON;
        minimapCam.enabled = false;
        minimapCam.orthographic = true;
    }

    void Update()
    {
        if(Input.GetKeyDown("m")){
            switch(minimapNextState){
                case MinimapState.OFF:
                    minimap.SetActive(false);
                    minimapCam.enabled = false;
                    minimapNextState = MinimapState.ON;
                    break;
                case MinimapState.ON:
                    minimap.SetActive(true);
                    minimapCam.enabled = true;
                    minimapCam.orthographicSize = 15.0f;
                    minimapNextState = MinimapState.ON5;
                    break;
                case MinimapState.ON5:
                    minimapCam.orthographicSize = 10.0f;
                    minimapNextState = MinimapState.ON10;
                    break;
                case MinimapState.ON10:
                    minimapCam.orthographicSize = 5.0f;
                    minimapNextState = MinimapState.OFF;
                    break;
            }
        }
    }
}
