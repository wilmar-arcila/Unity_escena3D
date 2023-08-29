#define DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    private enum TipoSeguimiento {Fijo, MomentoLineal, Lerp}

    [Header("Objeto")]
    [Tooltip("Objeto a seguir")]
    [SerializeField] private GameObject body;
    private Rigidbody rb;
    private Vector3 projMGlobal;

    [Tooltip("Fijo: La cámara se ubica a una distancia fija del objeto en los 3 ejes.\nMomentoLineal: la cámara se ubica a una distancia fija del objeto en un espacio esférico apuntando en la dirección de movimiento del objeto.\nLerp: Se utiliza un valor de lerp para seguir la posición y la rotación del objeto.")]
    [SerializeField] private TipoSeguimiento tipoSeguimiento;

    [Space(8)]
    [Header("Offset")]

    [Space(15)]
    [Tooltip("Distancia al objeto")]
    [SerializeField] private float cameraDistance = 1f;

    [Tooltip("Angulo de declinación (Pitch) desde el cual se observa el objeto")]
    [Range(-90f,90f)]
    [SerializeField] private float declinationAngle = 0f;

    [Tooltip("Angulo de ascención de la cámara respecto al objeto")]
    [Range(-70f,70f)]
    [SerializeField] private float ascensionAngle = 0f;

    [Tooltip("Angulo de precesión de la cámara (Yaw) respecto al objeto")]
    [Range(-45f,45f)]
    [SerializeField] private float precessionAngle = 0f;

    [Tooltip("Offset vertical para la cámara")]
    [Range(0,5f)]
    [SerializeField] private float verticalOffset = 0f;

    [Space(15)]
    [Tooltip("Valor de Lerp para el seguimiento de la posición")]
    [SerializeField] private float pLerp = 0.2f;
    [Tooltip("Valor de Lerp para el seguimiento de la rotación")]
    [SerializeField] private float rLerp = 0.1f;
    
    private bool tailSet;
    private Vector3 offset;
    private GameObject tail;

    void Start()
    {
        rb = body.GetComponent<Rigidbody>();
        projMGlobal = new Vector3(0,0,0);
        tail = new GameObject("Tail");
        tail.SetActive(false);
        tailSet = false;
    }
    void Update(){
        // Vector de proyección de M (momento lineal) sobre el plano horizontal de la escena
        projMGlobal = Vector3.ProjectOnPlane(rb.velocity, Vector3.up).normalized;

        float  _x = cameraDistance*Mathf.Cos(declinationAngle * Mathf.Deg2Rad)*Mathf.Sin(ascensionAngle * Mathf.Deg2Rad);
        float  _y = cameraDistance*Mathf.Sin(declinationAngle * Mathf.Deg2Rad);
        float  _z = -cameraDistance*Mathf.Cos(declinationAngle * Mathf.Deg2Rad)*Mathf.Cos(ascensionAngle * Mathf.Deg2Rad);
        offset = new Vector3(_x, _y, _z);

        switch (tipoSeguimiento)
        {
            case TipoSeguimiento.Fijo:
                tail.SetActive(false);
                tailSet = false;
                break;
            case TipoSeguimiento.Lerp:
                tail.SetActive(true);
                if(!tailSet){
                    tail.transform.position = body.transform.position + offset;
                    tail.transform.rotation = body.transform.rotation;
                    tail.transform.LookAt(body.transform.position);
                    tail.transform.Rotate(new Vector3(0,precessionAngle,0));
                    tail.transform.position = tail.transform.position + new Vector3(0, verticalOffset, 0);
                    tail.transform.SetParent(body.transform);
                    tailSet = true;
                }                
                break;
            case TipoSeguimiento.MomentoLineal:
                tail.SetActive(false);
                offset = new Vector3(0,cameraDistance*Mathf.Sin(declinationAngle * Mathf.Deg2Rad),0);
                tailSet = false;
                break;
        }
    }

    void LateUpdate()
    {
        #if DEBUG
            Debug.DrawRay(body.transform.position, projMGlobal, Color.blue);
            Debug.DrawRay(transform.position, 5*transform.forward, Color.red);
        #endif

        switch (tipoSeguimiento)
        {
            case TipoSeguimiento.Fijo:
                // Ubica la cámara según el offset
                transform.position = body.transform.position + offset;
                // Giro de la cámara apuntando al objeto
                transform.LookAt(body.transform.position);
                // Desviación de la cámara
                transform.Rotate(new Vector3(0,precessionAngle,0));
                transform.position = transform.position + new Vector3(0, verticalOffset, 0);
                break;
            case TipoSeguimiento.MomentoLineal:
                // Posición de la cámara en línea con el objeto en la dirección de M
                transform.position = body.transform.position - cameraDistance * projMGlobal;
                // Ubica la cámara según el offset
                transform.position = transform.position + offset;
                // Giro de la cámara apuntando al objeto
                transform.LookAt(body.transform.position);
                // Desviación de la cámara
                transform.Rotate(new Vector3(0,precessionAngle,0));
                break;
            case TipoSeguimiento.Lerp:
                transform.position = Vector3.Lerp(transform.position, tail.transform.position, pLerp);
                transform.rotation = Quaternion.Lerp(transform.rotation, tail.transform.rotation, rLerp);
                break;
        }
        
        
    }
}
