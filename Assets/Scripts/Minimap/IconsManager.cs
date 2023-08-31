using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsManager : MonoBehaviour
{
    [Tooltip("Objeto que contiene el Manager del Minimap")]
    [SerializeField] private MinimapManager minimapManager;

    void Start(){
        if(minimapManager != null){  // Se suscribe a los eventos en los que está interesado
            minimapManager.MinimapChanged += OnMinimapChanged;
        }
        
    }

    private void OnDestroy(){       // Cancela la suscripción a los eventos
        if(minimapManager != null){
            minimapManager.MinimapChanged -= OnMinimapChanged;
        }
    }

    private void OnMinimapChanged(int size){
        gameObject.transform.localScale = new Vector3(size, size, 1);
    }
}
